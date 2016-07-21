using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class AssetView : MonoBehaviour, IPointerClickHandler
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
		ObjectManager.Instance.CreateAssets (name);

		AssetDB.Instance._CloseViewer ();
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (Input.GetMouseButtonUp (0)) {

		}



	}
}
