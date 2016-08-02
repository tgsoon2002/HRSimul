using UnityEngine;
using System.Collections;

public class FourArrow : MonoBehaviour
{

	private GameObject referenceItem;
	private bool following;

	public GameObject ObjectBeingFollowing {
		get{ return  referenceItem; }
		set {
			gameObject.SetActive (true);
			referenceItem = value;
			following = true; 
		}
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (following) {
			transform.position = referenceItem.transform.position;	
		} 

	}

	public void StopFollow ()
	{
		following = false;
		gameObject.SetActive (false);
	}
}
