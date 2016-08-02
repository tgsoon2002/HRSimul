using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using LitJson;

public class RoomDB : MonoBehaviour {
	public GameObject roomButton;
	public Room roomSize;
	public static RoomDB _instance;
	private JsonData roomJson;
	private JsonData roomItemJson;
	public static List<Room> roomList;
	public static List<RoomItem> roomItemList;
	public static bool isLoadedLevel{ get; set;}
	public static string loadNumber { get; set;}
	public Canvas LoadRoomOverlay;
	void Start()
	{
		isLoadedLevel = false;
//		if (SceneManager.GetActiveScene ().name == "EditorScene")
//			gameObject.GetComponent<Canvas> ().enabled = false;
//		}
		LoadRoomOverlay.enabled = false;
		ConstructDB ();
	}
	// Use this for initialization
	void Awake()
	{
		if (_instance == null) {
			DontDestroyOnLoad (this.gameObject);
			_instance = this;
		} else if (_instance != this) {
			Destroy (this.gameObject);
		}
		roomItemList = new List<RoomItem> ();
		roomList = new List<Room> ();
		_CloseViewer ();
	}

	public void createButton()
	{
		roomJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/RoomDB.json"));
	}

	public static RoomDB Instance()
	{
		return _instance;
	}

	void OnLevelWasLoaded(int level){
		if( this != _instance ) return;
		if (roomList.Count == 0) {
			ConstructDB ();
		}
		if (level == 1 && isLoadedLevel == true) {
			_CloseViewer ();
		
			LoadFromDB ();
		}
		if (LoadRoomOverlay != null) {
			LoadRoomOverlay.enabled = false;
		}

	}

	void Update()
	{Debug.Log (roomItemList.Count);
	}


	public void Load(string roomNumber)
	{
		Room tempRoom =  roomList.Find (o => o.roomName == roomNumber);


		PlayerPrefs.SetFloat("Length",(float)tempRoom.length);
		PlayerPrefs.SetFloat("Width",(float)tempRoom.width);
		PlayerPrefs.SetFloat("Height",(float)tempRoom.height);

		// keeptrack which room being loaded
		loadNumber = roomNumber;
		// flag if room was load from db, not by createroom btn
		isLoadedLevel = true;

		SceneManager.LoadScene (1);
	}


	//Construct the database
	void ConstructDB () {

		//Load Json data into roomJson
		roomJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/RoomDB.json"));
		for (int i = 0; i < roomJson.Count; i++) {
			Debug.Log (roomJson.Count);
			string rname = roomJson [i] ["Roomname"].ToString ();
			double len = (double)roomJson [i] ["length"];
			double hei = (double)roomJson [i] ["height"];
			double dep = (double)roomJson [i] ["depth"];
			roomList.Add(new Room(rname,len,hei,dep));
		}


		//For each Json object add to the roomList
		roomItemJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Room1.json"));
		for (int i = 0; i < roomItemJson.Count; i++) {
			Debug.Log ((double)roomItemJson [i] ["LocX"]);
			//Grab the AssetType
			AssetType tempType = (AssetType)Enum.Parse (typeof(AssetType), roomItemJson [i] ["aType"].ToString ());

			//Grab location and rotation info for vectors
			double locX = (double)roomItemJson [i] ["LocX"];  
			double locY = (double)roomItemJson [i] ["LocY"];
			double locZ = (double)roomItemJson [i] ["LocZ"];
			double rotX = (double)roomItemJson [i] ["RotX"];
			double rotY = (double)roomItemJson [i] ["RotY"];
			double rotZ = (double)roomItemJson [i] ["RotZ"];

			//Position and rotation vectors
			Vector3	tempPos = new Vector3 ((float)locX, (float)locY, (float)locZ);
			Vector3	tempRot = new Vector3 ((float)rotX, (float)rotX, (float)rotX);
			int tempID = (int)roomItemJson [i] ["ID"]; //The item ID

			//Add to the roomList
			roomItemList.Add (new RoomItem (
				roomItemJson [i]["RoomName"].ToString (),
				roomItemJson [i] ["aName"].ToString (),
				tempType,
				tempPos,
				tempRot,
				tempID
			));
		}

	}


	public void Save(string roomNumber)
	{
		string roomInt = roomNumber.Replace ("Room", "");
		int roomIndex = Int32.Parse(roomInt) - 1;

		double roomHeight =(double)PlayerPrefs.GetFloat ("Height");
		double roomLength =(double)PlayerPrefs.GetFloat ("Length");
		double roomWidth =(double)PlayerPrefs.GetFloat ("Width");

		roomList [roomIndex] = new Room(roomNumber, roomLength,roomWidth,roomHeight);

		roomItemList.Clear ();
		List<AssetBase> objManList = ObjectManager.Instance.assetList;
		for (int i = 0; i < objManList.Count; i++) {
			roomItemList.Add(new RoomItem ("Room" + roomList.Count, objManList [i].name,  objManList [i].type, objManList [i].gameObject.transform.position,
				new Vector3(objManList[i].gameObject.transform.rotation.x,objManList[i].gameObject.transform.rotation.y,objManList[i].gameObject.transform.rotation.z), objManList [i].randomID));
		}
		saveToDB (roomNumber);
		Debug.Log ("I saved");
		_CloseViewer ();
	}

	public void saveToDB(string roomNumber)
	{
		StringBuilder sb = new StringBuilder ();
		JsonWriter writer = new JsonWriter (sb);

		writer.WriteArrayStart ();
		writer.WriteArrayStart ();
		for (int i = 0; i < roomItemList.Count; i++) {

			writer.WriteObjectStart ();

			writer.WritePropertyName ("RoomName");
			writer.Write (roomNumber);
			string name = roomItemList [i].itemName;
			name = name.Replace ("(Clone)", "");

			writer.WritePropertyName ("aName");
			writer.Write (name);

			writer.WritePropertyName ("aType");
			writer.Write (roomItemList [i].type.ToString());

			writer.WritePropertyName ("LocX");
			writer.Write (roomItemList [i].location.x);
			writer.WritePropertyName ("LocY");
			writer.Write (roomItemList [i].location.y);
			writer.WritePropertyName ("LocZ");
			writer.Write (roomItemList [i].location.z);

			writer.WritePropertyName ("RotX");
			writer.Write (roomItemList [i].rotation.x);
			writer.WritePropertyName ("RotY");
			writer.Write (roomItemList [i].rotation.y);
			writer.WritePropertyName ("RotZ");
			writer.Write (roomItemList [i].rotation.z);

			writer.WritePropertyName ("ID");
			writer.Write (roomItemList [i].ID);


			writer.WriteObjectEnd ();
		}
		writer.WriteArrayEnd ();
		writer.WriteArrayEnd ();
		File.WriteAllText (Application.dataPath + "/StreamingAssets/Room1.json",sb.ToString());

		writer.Reset();
		writer.WriteArrayStart ();
		writer.WriteArrayStart ();
		for (int i = 0; i < roomList.Count; i++) 
		{
			writer.WriteObjectStart ();
			writer.WritePropertyName ("Roomname");
			writer.Write (roomList[i].roomName);
			writer.WritePropertyName ("length");
			writer.Write (roomList [i].length);
			writer.WritePropertyName ("height");
			writer.Write (roomList [i].height);
			writer.WritePropertyName ("depth");
			writer.Write (roomList [i].width);
			writer.WriteObjectEnd ();
		}
		writer.WriteArrayEnd ();
		File.WriteAllText(Application.dataPath + "/StreamingAssets/RoomDB.json",sb.ToString());
	}



	// Instantiate objects from JSON data
	public void LoadFromDB () {
		Debug.Log ("how many time this is loaded");
		foreach (var item in roomItemList) {
			if (item.roomName == loadNumber) {
				ObjectManager.Instance.AddAsset (item.itemName, item.type, item.location,item.rotation , item.ID);	
			}
		}
	}

	public void _SaveMenu()
	{
//		transform.gameObject.SetActive (true);
//		transform.GetChild (0).gameObject.SetActive (true);
//		transform.GetChild (1).gameObject.SetActive (true);
//		transform.GetChild (2).gameObject.SetActive (true);
//		transform.GetChild (3).gameObject.SetActive (true);
//		transform.GetChild (4).gameObject.SetActive (true);
	}


	public void _CloseViewer()
	{
//		transform.GetChild(0).gameObject.SetActive (false);
//		transform.GetChild(1).gameObject.SetActive (false);
//		transform.GetChild(2).gameObject.SetActive (false);
//		transform.GetChild(3).gameObject.SetActive (false);
//		transform.GetChild(4).gameObject.SetActive (false);

	}

}


public struct Room
{
	public string roomName { get; set;}
	public double length { get; set;}
	public double width { get; set;}
	public double height { get; set;}
					public Room ( string rn, double l, double w, double h) : this()
					{
						roomName = rn;
		length = l;
		width = w;
		height = h;

					}
}

public struct RoomItem
{
	public string roomName { get; set;}
	public string itemName { get; set;}
	public  AssetType type { get; set;}
	public Vector3 location { get; set;}
	public Vector3 rotation { get; set;}
	public int ID { get; set;}


	public RoomItem (string room, string item, AssetType aType, Vector3 aLoc,Vector3 aRot, int id) : this()
	{
		this.roomName = room;
		this.itemName = item;
		this.type = aType;
		this.location = aLoc;
		this.rotation = aRot;
		this.ID = id;
	}
}