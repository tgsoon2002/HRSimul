using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ObjectItem : MonoBehaviour, IPointerClickHandler
{
	public AssetBase referenceItem;


	private bool selected = false;

	public bool IsSelected {
		get{ return  selected; }
		set {
			
			selected = value; 
			if (value) {
				ObjectManager.Instance.DeslectedCurrentObject ();
				GetComponent<Text> ().fontStyle = FontStyle.Bold;
				ObjectManager.Instance.selectedItem = gameObject;

				Debug.Log ("reference Item will highlight.");
			} else {
				GetComponent<Text> ().fontStyle = FontStyle.Normal;
				Debug.Log ("reference Item will de-highlight.");
			}
		}
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (selected) {
			Debug.Log ("moving Mode");
		} else {
			IsSelected = true;	
		}

	}

}
