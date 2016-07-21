using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class  ObjectManager : MonoBehaviour
{

	private static ObjectManager _instance;
	//[SerializeField]
	private List<AssetBase> assetList;
	private List<GameObject> objectNameList;
	public Vector3 roomDimen;
	public GameObject assetPrefab;
	public GameObject objectNamePre;
	public Texture wallText;
	public Texture floorText;


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

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	#endregion

	public void RemoveItem (AssetBase assBeingRemove)
	{
		objectNameList.RemoveAll (o => o.name == assBeingRemove.randomID.ToString ());

		assetList.Remove (assBeingRemove);
	}

	public void CreateAssets ()
	{
		Random rand = new Random ();
		int id = -1;
		do {
			id = Random.Range (0, 1000);	
		} while (objectNameList.Find (o => o.name == id.ToString ()));

		GameObject tempAsset = Instantiate (assetPrefab, Vector3.zero, Quaternion.Euler (Vector3.zero)) as GameObject;
		tempAsset.GetComponent<AssetBase> ().randomID = id;
		GameObject tempAssetName = Instantiate (objectNamePre) as GameObject;
		tempAssetName.transform.parent = transform;
		tempAssetName.name = id.ToString ();
		assetList.Add ((AssetBase)tempAsset.GetComponent<AssetBase> ());
		objectNameList.Add (tempAssetName);
	}

	public void CreateAssets (Vector3 position)
	{


	}



}
