using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EditorScene : MonoBehaviour {

    public Canvas quitOverlay;
	public Canvas wallOverlay;

	// Use this for initialization
	void Start ()
    {
        quitOverlay.enabled = false;
		wallOverlay.enabled = false;
	}

    //Opens the Quit Overlay UI to ask the user if they 
    //really want to quit
    public void QuitPress()
    {
        quitOverlay.enabled = true;
    }

	//opens the wall selection overlay where the user can select which wall they want to edit
	public void WallChangePress()
	{
		wallOverlay.enabled = true;
	}

	//closes the wall selection overlay when the user has selected a wall to edit or they
	//press the close button
	public void SelectionPress()
	{
		wallOverlay.enabled = false;
	}
}
