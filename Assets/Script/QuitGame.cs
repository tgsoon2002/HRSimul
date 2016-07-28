using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Class to quit the game when the user selects the quit button
//in either the Main Menu Scene or the Editor Scene
public class QuitGame : MonoBehaviour
{

    public Canvas quitOverlay;

	// Use this for initialization
	void Start ()
    {
        quitOverlay = quitOverlay.GetComponent<Canvas>();
	}

    //Function that calls the application class to quit the game
    public void YesPress ()
    {
        Application.Quit();
    }

    //Disables the QuitOverlay Canvas so it is no longer showing
    public void NoPress ()
    {
        quitOverlay.enabled = false;
    }
}
