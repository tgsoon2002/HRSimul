using UnityEngine;

using System.Collections;

public class AssetBase : MonoBehaviour
{
	protected string nameAssets;
	public  int randomID;
	public bool isSelected = false;



	public bool IsSelected {
		get { return  isSelected; }
		set { isSelected = value; }
	}

	public void BeingRemove ()
	{
		ObjectManager.Instance.RemoveItem (this);
	}

	public virtual void Move ()
	{
		
	}

	public void BeingSelected ()
	{
		
	}



}
