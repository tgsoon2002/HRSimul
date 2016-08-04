using UnityEngine;
using System.Collections;

public class Furniture : AssetBase
{

	void Update ()
	{
		if (isSelected && Input.GetKey (KeyCode.Space)) {
			Debug.Log (name);
			if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.1f) {
				Debug.Log (name);
				transform.Translate (Vector3.right * Input.GetAxis ("Horizontal") * 0.7f, Space.World);
			}
			// scroll wheel or w and s
			if (Mathf.Abs (Input.GetAxis ("Vertical")) > 0.1f) {
				Debug.Log (name);
				transform.Translate (Vector3.forward * Input.GetAxis ("Vertical") * 0.7f, Space.World);
			}
			//Input q and e 
			if (Mathf.Abs (Input.GetAxis ("Side")) > 0.1f) {
				transform.Rotate (Vector3.up * Input.GetAxis ("Side") * 0.8f, Space.Self);
			}
			transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0.0f);
		}
	}

	public void Rotate ()
	{
		
	}

}
