using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AssetView : MonoBehaviour
{
	public AssetType assetType;
	public Texture aTexture;
	public Text countText;
	public int aCount = 0;
	//Destroy(ObjectManager.Instance.assetList.Find(0->0.randomID == value))

	public int UsedCount {
		get{ return  aCount; }
		set {
			aCount = value; 
			countText.text = aCount.ToString ();
		}
	}

	public void _CreateObject ()
	{
		Debug.Log (name);
		if (assetType != AssetType.Texture) {
			ObjectManager.Instance.CreateAssets (name, assetType);
		} else {

		}

		AssetDB.Instance._CloseViewer ();
	}


}
