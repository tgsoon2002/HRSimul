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
	private GameObject assetPrefab;
	public GameObject objectNamePre;
	public Material wallText;
	public Material floorText;
	public int currentID;

	public GameObject FourArrow;
	public GameObject selectedItem;

	private RaycastHit hit;
	private Ray ray;

	public static ObjectManager Instance {
		get{ return  _instance; }
	}

	public GameObject SelectedItem {
		get{ return  selectedItem; }
		set {
			selectedItem = value;
			FourArrow.GetComponent<FourArrow> ().Following = value;
		}
	}


	#region Unity Built-In

	void Awake ()
	{
		_instance = this;
		assetList = new List<AssetBase> ();
		objectNameList = new List<GameObject> ();
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 50.0f)) {
				if (hit.collider.gameObject.tag == "Object") {
					SelectedItem = hit.collider.gameObject;
				} else if (hit.collider.gameObject.tag == "Wall") {
					AssetDB.Instance._OpenWallAssetViewer ();
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
		objectNameList.Add (tempAssetName);
	}

	public void AddAsset (string name, AssetType type, Transform location, int id)
	{
		GameObject tempAsset = InstantiateAsset (name, type, id);
		tempAsset.transform.position = location.position;
		tempAsset.transform.rotation = location.rotation;
	}


	public void _WallColorChange (Color newColor)
	{
		if (wallText != null) {
			wallText.color = newColor;	
		}
	}

	public void DeslectedCurrentObject ()
	{
		if (selectedItem != null) {
			selectedItem.GetComponent<ObjectItem> ().IsSelected = false;	
		}

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
