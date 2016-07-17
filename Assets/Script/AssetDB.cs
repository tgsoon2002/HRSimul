using UnityEngine;
using System.Collections;

public class AssetDB : MonoBehaviour
{

	public static AssetDB Instance;

	//	public static AssetDB Instance {
	//		get	{ return _instance; }
	//	}

	void Awake ()
	{
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
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
}
