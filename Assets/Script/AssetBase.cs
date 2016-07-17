using UnityEngine;
using System.Collections;

public class AssetBase : MonoBehaviour
{
	protected string nameAssets;
	public  int randomID;

	public void BeingRemove ()
	{
		ObjectManager.Instance.RemoveItem (this);
	}

	public virtual void Move ()
	{
		
	}
}
