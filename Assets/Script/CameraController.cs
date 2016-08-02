using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!Input.GetKey (KeyCode.Space)) {
			// input a and d
			if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.1f) {
				transform.Translate (Vector3.right * Input.GetAxis ("Horizontal") * 0.7f);
			}
			// scroll wheel or w and s
			if (Mathf.Abs (Input.GetAxis ("Vertical")) > 0.1f) {
				transform.Translate (Vector3.forward * Input.GetAxis ("Vertical") * 0.7f);
			}
			//Input left ctrl and left shift
			if (Mathf.Abs (Input.GetAxis ("Height")) > 0.1f) {
				transform.Translate (Vector3.up * Input.GetAxis ("Height") * 0.7f);
			}
			//Input f and r
			if (Mathf.Abs (Input.GetAxis ("Depth")) > 0.1f) {
				transform.Rotate (Vector3.left * Input.GetAxis ("Depth") * 0.4f, Space.Self);
			}
			//Input q and e 
			if (Mathf.Abs (Input.GetAxis ("Side")) > 0.1f) {
				transform.Rotate (Vector3.up * Input.GetAxis ("Side") * 0.4f, Space.World);
			}
				
		}
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0.0f);
	}

}
