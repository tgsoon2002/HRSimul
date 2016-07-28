using UnityEngine;
using UnityEngine.UI;
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

	public GameObject TotalPanel;

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
		UpdateFav (); 	
		_CloseViewer ();
	}

	#endregion

	#region Public Methods

	public void _ViewWallAsset ()
	{
		TotalPanel.transform.GetChild (2).gameObject.SetActive (true);
		TotalPanel.transform.GetChild (3).gameObject.SetActive (true);
		TotalPanel.transform.GetChild (0).gameObject.SetActive (false);
		TotalPanel.transform.GetChild (1).gameObject.SetActive (false);
	}

	public void _SwitchAsset ()
	{
		if (TotalPanel.transform.GetChild (0).gameObject.activeSelf) {
			_ViewWallAsset ();
		} else {
			_ViewObjectAsset ();
		}
	}

	public void _ViewObjectAsset ()
	{
		TotalPanel.transform.GetChild (0).gameObject.SetActive (true);
		TotalPanel.transform.GetChild (1).gameObject.SetActive (true);
		TotalPanel.transform.GetChild (2).gameObject.SetActive (false);
		TotalPanel.transform.GetChild (3).gameObject.SetActive (false);

	}

	public void _OpenObjectViewer ()
	{
		transform.GetChild (0).gameObject.SetActive (true);
		transform.GetChild (1).gameObject.SetActive (true);
		transform.GetChild (2).gameObject.SetActive (true);
		_ViewObjectAsset ();
	}

	public void _OpenWallAssetViewer ()
	{
		transform.GetChild (0).gameObject.SetActive (true);
		transform.GetChild (1).gameObject.SetActive (true);
		transform.GetChild (2).gameObject.SetActive (true);
		_ViewWallAsset ();
	}

	public void _CloseViewer ()
	{
		transform.GetChild (0).gameObject.SetActive (false);
		transform.GetChild (1).gameObject.SetActive (false);
		transform.GetChild (2).gameObject.SetActive (false);
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
			temp.GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Graphic/" + item.aName, typeof(Sprite));
			temp.GetComponent<AssetView> ().name = item.aName;
			temp.GetComponent<AssetView> ().assetType = item.aType;
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

	void UpdateFav ()
	{
		int prevMax = 500;
		int max = 0;
		string topFav = "";
		bool change = false;
		#region Generate TopFav Furniture
		for (int i = 0; i < 5; i++) {
			foreach (var item in assetList) {
				if (item.aCount > max && item.aType == AssetType.Furniture && item.aCount < prevMax) {
					change = true;
					max = item.aCount;
					topFav = item.aName;
				}
			}

			if (change == true) {
				prevMax = max;
				max = 0;
				GameObject temp = Instantiate (assetViewPrefab) as GameObject;
				AssetItem itemTemp = assetList.Find (o => o.aName == topFav);
				temp.GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Graphic/" + itemTemp.aName, typeof(Sprite));
				temp.GetComponent<AssetView> ().name = itemTemp.aName;
				temp.GetComponent<AssetView> ().assetType = itemTemp.aType;
				temp.GetComponent<AssetView> ().aTexture = (Texture)Resources.Load (itemTemp.aName, typeof(Texture));
				temp.GetComponent<AssetView> ().UsedCount = itemTemp.aCount;

				temp.transform.SetParent (furnitureFavCon.transform);
			}
			change = false;
		}
		#endregion
		#region Generate TopFav RoomUtility
		max = -1;
		prevMax = 500;
		for (int i = 0; i < 5; i++) {
			foreach (var item in assetList) {
				if (item.aCount > max && item.aType == AssetType.RoomUtility && item.aCount < prevMax) {
					change = true;
					max = item.aCount;
					topFav = item.aName;
				}
			}
			if (change == true) {
				prevMax = max;
				max = 0;
				GameObject temp = Instantiate (assetViewPrefab) as GameObject;
				AssetItem itemTemp = assetList.Find (o => o.aName == topFav);
				temp.GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Graphic/" + itemTemp.aName, typeof(Sprite));
				temp.GetComponent<AssetView> ().name = itemTemp.aName;
				temp.GetComponent<AssetView> ().assetType = itemTemp.aType;
				temp.GetComponent<AssetView> ().aTexture = (Texture)Resources.Load (itemTemp.aName, typeof(Texture));
				temp.GetComponent<AssetView> ().UsedCount = itemTemp.aCount;

				temp.transform.SetParent (roomUtiFavCon.transform);
			}
			change = false;
		}
		#endregion
		#region Generate TopFav Texture
		prevMax = 500;
		max = -1;
		for (int i = 0; i < 5; i++) {
			foreach (var item in assetList) {
				if (item.aCount > max && item.aType == AssetType.Texture && item.aCount < prevMax) {
					change = true;
					max = item.aCount;
					topFav = item.aName;
				}
			}
			if (change == true) {
				prevMax = max;
				max = 0;
				GameObject temp = Instantiate (assetViewPrefab) as GameObject;
				AssetItem itemTemp = assetList.Find (o => o.aName == topFav);
				temp.GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Graphic/" + itemTemp.aName, typeof(Sprite));
				temp.GetComponent<AssetView> ().name = itemTemp.aName;
				temp.GetComponent<AssetView> ().assetType = itemTemp.aType;
				temp.GetComponent<AssetView> ().aTexture = (Texture)Resources.Load (itemTemp.aName, typeof(Texture));
				temp.GetComponent<AssetView> ().UsedCount = itemTemp.aCount;

				temp.transform.SetParent (textureFavCon.transform);
			}
			change = false;
		}
		#endregion

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