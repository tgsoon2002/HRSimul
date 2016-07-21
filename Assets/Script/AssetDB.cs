using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System;
using System.Text;
using LitJson;

public class AssetDB : MonoBehaviour
{
	#region Data Members

	private JsonData assetJson;


	public static AssetDB Instance;

	public List <AssetItem> assetList;
	public int totalItem;
	public AssetView assetViewPrefab;
	public GameObject furnitureCon;
	public GameObject furnitureFavCon;
	public GameObject roomUtiCon;
	public GameObject roomUtiFavCon;
	public GameObject textureCon;
	public GameObject textureFavCon;

	public GameObject colorPicker;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods

	void Awake ()
	{
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		assetList = new List<AssetItem> ();
		ConstructDatabase ();
		totalItem = assetList.Count;
		gameObject.SetActive (false);
	}

	#endregion

	#region Public Methods

	public void _OpenColorPicker ()
	{
		colorPicker.gameObject.SetActive (true);
	}

	public void _CloseColorPicker ()
	{
		colorPicker.gameObject.SetActive (false);
	}



	public void _OpenViewer ()
	{
		transform.GetChild (0).gameObject.SetActive (true);
		transform.GetChild (1).gameObject.SetActive (true);
	}

	public void _CloseViewer ()
	{
		transform.GetChild (0).gameObject.SetActive (false);
		transform.GetChild (1).gameObject.SetActive (false);
	}

	#endregion

	#region Private Methods

	void ConstructDatabase ()
	{
		assetJson = JsonMapper.ToObject (File.ReadAllText (Application.dataPath + "/StreamingAssets/AssetDatabase.json"));
		//go through each character
		for (int i = 0; i < assetJson.Count; i++) {
			AssetType temptype = (AssetType)Enum.Parse (typeof(AssetType), assetJson [i] ["aType"].ToString ());

			assetList.Add (new AssetItem (
				assetJson [i] ["aName"].ToString (),
				temptype,
				(int)assetJson [i] ["aCount"]
			));

		}
	}

	void GenerateAssetView ()
	{
		foreach (var item in assetList) {
	
		}
	}

	#endregion




	//	public static AssetDB Instance {
	//		get	{ return _instance; }
	//	}








}

public enum AssetType
{
	Furniture,
	RoomUtility,
	Texture
}

public struct AssetItem
{
	public string aName;
	public AssetType aType;
	public int aCount;

	public AssetItem (string name, AssetType type, int count)
	{ 
		aName = name;
		aType = type;
		aCount = count;
	}
}