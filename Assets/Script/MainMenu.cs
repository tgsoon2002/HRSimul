using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Menu class that holds functions for each button option
//shown to the user in the UI
public class MainMenu : MonoBehaviour
{

    public Canvas createRoomOverlay;
    public Canvas quitOverlay;

	// Use this for initialization
	void Start ()
    {
        createRoomOverlay.enabled = false;
        quitOverlay.enabled = false;
	}

    //Displays createRoomOverlay when the Create Room button is pressed
    //in the main menu
    public void CreateRoomPress()
    {
        createRoomOverlay.enabled = true;
    }

    //Displays quitOverlay when quit button is pressed in the Main Menu
    public void QuitPress()
    {
        quitOverlay.enabled = true;
    }
	
}
