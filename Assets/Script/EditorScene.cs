using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EditorScene : MonoBehaviour {

    public Canvas quitOverlay;
    public Canvas assetViewOverlay;

	// Use this for initialization
	void Start ()
    {
        quitOverlay.enabled = false;
		if (assetViewOverlay != null) {
			assetViewOverlay.enabled = false;
		}
       
	}

    //Opens the Quit Overlay UI to ask the user if they 
    //really want to quit
    public void QuitPress()
    {
        quitOverlay.enabled = true;
    }

    //Opens the AssetView Overlay UI to allow the user to add 
    //different furniture and room utility objects
    public void AssetViewPress()
    {
        assetViewOverlay.enabled = true;
    }
}
