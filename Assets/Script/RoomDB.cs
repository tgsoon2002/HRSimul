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
	void ConstructDB () {

		roomJson = JsonMapper.ToObject (File.ReadAllText (Application.dataPath + "/StreamingAssets/RoomDB.json"));
		for (int i = 0; i < roomJson.Count; i++) {
			AssetType tempType = (AssetType)Enum.Parse (typeof(AssetType), roomJson [i] ["aType"].ToString ());

			Vector3	tempPos = new Vector3 ((float)roomJson [i] ["LocX"],(float) roomJson [i] ["LocY"], (float)roomJson ["LocZ"]);
			Vector3	tempRot= new Vector3 ((float)roomJson [i] ["RotX"],(float) roomJson [i] ["RotY"],(float) roomJson [i] ["RotZ"]);

			roomList.Add (new RoomItem (
				roomJson [i] ["RoomName"].ToString (),
				roomJson [i] ["aName"].ToString (),
				tempType,
				tempPos,
				tempRot,
				(int)roomJson [i] ["ID"]));
		}
		Debug.Log (roomList.Count);
	}

	void Save()
	{
		StringBuilder sb = new StringBuilder ();
		JsonWriter writer = new JsonWriter (sb);

		writer.WriteObjectStart ();

	}

	// Update is called once per frame
	void Update () {
	
	}
}


public struct RoomItem
{
	public string roomName;
	public string itemName;
	public  AssetType type;
	public Vector3 location;
	public Vector3 rotation;
	public int ID;

	public RoomItem (string room, string item, AssetType aType, Vector3 aLoc,Vector3 aRot, int id)
	{
		roomName = room;
		itemName = item;
		type = aType;
		location = aLoc;
		rotation = aRot;
		ID = id;
	}
}