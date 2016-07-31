using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using LitJson;

public class RoomDB : MonoBehaviour {
	Button[] buttons;
	private JsonData roomJson;
	public List<RoomItem> roomList;

	// Use this for initialization
	void Start()
	{
		_CloseViewer ();
		roomList = new List<RoomItem> ();
	}

	//Construct the database
	void ConstructDB (string roomNumber) {

		//Load Json data into roomJson
		roomJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/"+roomNumber+".json"));

		//For each Json object add to the roomList
		for (int i = 0; i < roomJson.Count; i++) {
			//Grab the AssetType
			AssetType tempType = (AssetType)Enum.Parse (typeof(AssetType), roomJson [i] ["aType"].ToString ());

			//Grab location and rotation info for vectors
			double locX = (double)roomJson [i] ["LocX"];  
			double locY = (double)roomJson [i] ["LocY"];
			double locZ = (double)roomJson [i] ["LocZ"];
			double rotX = (double)roomJson [i] ["RotX"];
			double rotY = (double)roomJson [i] ["RotY"];
			double rotZ = (double)roomJson [i] ["RotZ"];

			//Position and rotation vectors
			Vector3	tempPos = new Vector3 ((float)locX, (float)locY, (float)locZ);
			Vector3	tempRot = new Vector3 ((float)rotX, (float)rotX, (float)rotX);
			int tempID = (int)roomJson [i] ["ID"]; //The item ID

			//Add to the roomList
			roomList.Add (new RoomItem (
				roomJson [i]["RoomName"].ToString (),
				roomJson [i] ["aName"].ToString (),
				tempType,
				tempPos,
				tempRot,
				tempID
			));
		}
	}


	public void Save(string roomNumber)
	{
		roomList.Clear ();
		List<AssetBase> objManList = ObjectManager.Instance.assetList;
		for (int i = 0; i < objManList.Count; i++) {
			roomList.Add(new RoomItem ("Room1", objManList [i].name,  objManList [i].type, objManList [i].gameObject.transform.position,
				gameObject.transform.rotation.eulerAngles, objManList [i].randomID));
		}
			
		StringBuilder sb = new StringBuilder ();
		JsonWriter writer = new JsonWriter (sb);

		writer.WriteArrayStart ();

		for (int i = 0; i < roomList.Count; i++) {

			writer.WriteObjectStart ();

			writer.WritePropertyName ("RoomName");
			writer.Write (roomNumber);
			string name = roomList [i].itemName;
			name = name.Replace ("(Clone)", "");

			writer.WritePropertyName ("aName");
			writer.Write (name);

			writer.WritePropertyName ("aType");
			writer.Write (roomList [i].type.ToString());

			writer.WritePropertyName ("LocX");
			writer.Write (roomList [i].location.x);
			writer.WritePropertyName ("LocY");
			writer.Write (roomList [i].location.y);
			writer.WritePropertyName ("LocZ");
			writer.Write (roomList [i].location.z);

			writer.WritePropertyName ("RotX");
			writer.Write (roomList [i].rotation.x);
			writer.WritePropertyName ("RotY");
			writer.Write (roomList [i].rotation.y);
			writer.WritePropertyName ("RotZ");
			writer.Write (roomList [i].rotation.z);

			writer.WritePropertyName ("ID");
			writer.Write (roomList [i].ID);


			writer.WriteObjectEnd ();
		}
		writer.WriteArrayEnd ();
		File.WriteAllText (Application.dataPath + "/StreamingAssets/"+roomNumber+".json",sb.ToString());
	}


	// Instantiate objects from JSON data
	public void LoadFromDB (string roomNumber) {
		
		ConstructDB (roomNumber);
		for (int i = 0; i < roomList.Count; i++) {
			//ObjectManager.Instance.CreateAssets (roomList [i].itemName, roomList [i].type);
			GameObject tempObject = new GameObject();
			tempObject.transform.position = roomList [i].location;
			tempObject.transform.eulerAngles  = roomList [i].rotation;
			ObjectManager.Instance.AddAsset (roomList [i].itemName, roomList [i].type, tempObject.transform, roomList [i].ID);
		}
	}

	public void _SaveMenu()
	{
		//transform.GetChild (0).gameObject.SetActive (true);
		Debug.Log ("I'm getting called");
	}
	public void _LoadMenu()
	{
		transform.GetChild (1).gameObject.SetActive (true);
	}

	public void _CloseViewer()
	{
		transform.GetChild (1).gameObject.SetActive (false);
		transform.GetChild (0).gameObject.SetActive (false);
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