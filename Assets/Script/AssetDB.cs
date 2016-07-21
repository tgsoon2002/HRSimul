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
	public GameObject assetViewPrefab;
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
		GenerateAssetView ();
		_CloseViewer ();
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

	/// <summary>
	/// Generates the asset view.
	/// Will go through every item in the list, 
	/// Instantiate the item and set parent to the rigth container.
	/// </summary>
	void GenerateAssetView ()
	{
		foreach (var item in assetList) {
			GameObject temp = Instantiate (assetViewPrefab) as GameObject;
			temp.GetComponent<AssetView> ().name = item.aName;
			temp.GetComponent<AssetView> ().aTexture = (Texture)Resources.Load (item.aName, typeof(Texture));
			temp.GetComponent<AssetView> ().UsedCount = item.aCount;
			switch (item.aType) {
			case AssetType.Furniture:
				temp.transform.SetParent (furnitureCon.transform);
				break;
			case AssetType.RoomUtility:
				temp.transform.SetParent (roomUtiCon.transform);
				break;
			case AssetType.Texture:
				temp.transform.SetParent (textureCon.transform);
				break;
			default:
				break;
			}
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