using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class  ObjectManager : MonoBehaviour
{

	private static ObjectManager _instance;
	//[SerializeField]
	public List<AssetBase> assetList;
	public List<GameObject> objectNameList;
	public Vector3 roomDimen;
	public GameObject roomObject;
	private GameObject assetPrefab;
	public GameObject objectNamePre;
	private GameObject currentWall;

	public int currentID;

	public GameObject FourArrow;
	public GameObject selectedItem;

	private RaycastHit hit;
	private Ray ray;

	public static ObjectManager Instance {
		get{ return  _instance; }
	}

	#region Unity Built-In

	void Awake ()
	{
		_instance = this;
		assetList = new List<AssetBase> ();
		objectNameList = new List<GameObject> ();
	}



	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 50.0f)) {
				if (hit.collider.gameObject.tag == "Object") {
					DeselectedCurrentObject ();
					objectNameList.Find (o => o.name == hit.collider.gameObject.GetComponent<AssetBase> ().randomID.ToString ()).GetComponent<ObjectItem> ().IsSelected = true;
					FourArrow.GetComponent<FourArrow> ().ObjectBeingFollowing = hit.collider.gameObject;
				}
			}

		}
	}

	#endregion

	public void RemoveItem (AssetBase assBeingRemove)
	{
		objectNameList.RemoveAll (o => o.name == assBeingRemove.randomID.ToString ());

		assetList.Remove (assBeingRemove);
	}


	public void CreateAssets (string assetName, AssetType type)
	{
		Random rand = new Random ();
		int id = -1;
		do {
			id = Random.Range (0, 1000);	
		} while (objectNameList.Find (o => o.name == id.ToString ()));

		// Instantiate asset.
		GameObject tempAsset = InstantiateAsset (assetName, type, id);


		// Instantiate name in objectlist.
		GameObject tempAssetName = InstantiateObjectItem (assetName, id);

		// add asset and assetItem to the list.
		assetList.Add ((AssetBase)tempAsset.GetComponent<AssetBase> ());

		tempAssetName.GetComponent<ObjectItem> ().referenceItem = tempAsset.GetComponent<AssetBase> ();
		objectNameList.Add (tempAssetName);
	}

	public void AddAsset (string name, AssetType type, Transform location, int id)
	{
		GameObject tempAsset = InstantiateAsset (name, type, id);
		tempAsset.transform.position = location.position;
		tempAsset.transform.rotation = location.rotation;
	}

	public void DeselectedCurrentObject ()
	{
		
		if (objectNameList.Find (o => o.GetComponent<ObjectItem> ().IsSelected == true)) {
			objectNameList.Find (o => o.GetComponent<ObjectItem> ().IsSelected == true).GetComponent<ObjectItem> ().IsSelected = false;
		}
			
	}

	public void NorthWallClick()
	{
		Color newColor;

		currentWall = roomObject.transform.GetChild (1).gameObject;
		ColorUtility.TryParseHtmlString (PlayerPrefs.GetString ("HexColor"), out newColor);
		currentWall.GetComponent<Renderer> ().material.color = newColor;
	}
	public void WestWallClick()
	{
		Color newColor;

		currentWall = roomObject.transform.GetChild (2).gameObject;
		ColorUtility.TryParseHtmlString (PlayerPrefs.GetString ("HexColor"), out newColor);
		currentWall.GetComponent<Renderer> ().material.color = newColor;
	}
	public void EastWallClick()
	{
		Color newColor;

		currentWall = roomObject.transform.GetChild (3).gameObject;
		ColorUtility.TryParseHtmlString (PlayerPrefs.GetString ("HexColor"), out newColor);
		currentWall.GetComponent<Renderer> ().material.color = newColor;
	}
	public void SouthWallClick()
	{
		Color newColor;

		currentWall = roomObject.transform.GetChild (4).gameObject;
		ColorUtility.TryParseHtmlString (PlayerPrefs.GetString ("HexColor"), out newColor);
		currentWall.GetComponent<Renderer> ().material.color = newColor;
	}
	public void FloorWallClick()
	{
		Color newColor;

		currentWall = roomObject.transform.GetChild (0).gameObject;
		ColorUtility.TryParseHtmlString (PlayerPrefs.GetString ("HexColor"), out newColor);
		currentWall.GetComponent<Renderer> ().material.color = newColor;
	}

	#region Helper Class

	GameObject InstantiateObjectItem (string itemName, int id)
	{
		GameObject tempAssetName = Instantiate (objectNamePre) as GameObject;
		tempAssetName.transform.parent = transform;
		tempAssetName.name = id.ToString ();
		tempAssetName.GetComponent<Text> ().text = (itemName + " " + id.ToString ());
		return tempAssetName;
	}

	GameObject InstantiateAsset (string name, AssetType type, int id)
	{
		assetPrefab = (GameObject)Resources.Load (type.ToString () + "/" + name);
		GameObject tempAsset = Instantiate (assetPrefab, Vector3.zero, Quaternion.Euler (Vector3.zero)) as GameObject;
		tempAsset.GetComponent<AssetBase> ().randomID = id;
		return tempAsset;
	}

	#endregion

}
