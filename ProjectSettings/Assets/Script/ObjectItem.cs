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
				GetComponent<Text> ().fontStyle = FontStyle.Bold;
				referenceItem.IsSelected = true;
			} else {
				GetComponent<Text> ().fontStyle = FontStyle.Normal;
				referenceItem.IsSelected = false;
			}
		}
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (!selected) {
			ObjectManager.Instance.DeselectedCurrentObject ();
			IsSelected = true;	

		}

	}

}
