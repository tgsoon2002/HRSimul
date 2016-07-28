using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using LitJson;

public class RoomDB : MonoBehaviour {
	private JsonData roomJson;
	public List<RoomItem> roomList;

	// Use this for initialization
	void Start()
	{
		roomList = new List<RoomItem> ();
		ConstructDB ();
	}

	//Construct the database
	void ConstructDB () {

		//Load Json data into roomJson
		roomJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/RoomDB.json"));

		Debug.Log (roomJson);
		Debug.Log(roomJson[0]["ID"]);

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
				roomJson [i] ["RoomName"].ToString (),
				roomJson [i] ["aName"].ToString (),
				tempType,
				tempPos,
				tempRot,
				tempID
			));
		}

		Debug.Log (roomList.Count);
		Debug.Log (roomList);
	}

	void Save()
	{
		StringBuilder sb = new StringBuilder ();
		JsonWriter writer = new JsonWriter (sb);

		writer.WriteArrayStart ();
		for (int i = 0; i < roomList.Count; i++) {

			writer.WriteObjectStart ();
			writer.WritePropertyName ("RoomName");
			writer.Write(roomList[i].roomName);

			writer.WritePropertyName ("aName");
			writer.Write (roomList [i].itemName);

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
		File.WriteAllText (Application.dataPath + "/StreamingAssets/RoomDB.json",sb.ToString());
	}

	// Update is called once per frame
	void Update () {
	
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