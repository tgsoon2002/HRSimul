using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Menu class that holds functions for each button option
//shown to the user in the UI
public class MainMenu : MonoBehaviour
{

    public Canvas createRoomOverlay;
    public Canvas quitOverlay;
	public Canvas creditOverlay;

	// Use this for initialization
	void Start ()
    {
        createRoomOverlay.enabled = false;
        quitOverlay.enabled = false;
		creditOverlay.enabled = false;
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

	//Displays creditOverlay when the credit button is pressed in the main menu
	public void CreditPress()
	{
		creditOverlay.enabled = true;
	}

	//closes the creditOverlay when the user clicks the X in the overlay
	public void CloseCreditPress()
	{
		creditOverlay.enabled = false;
	}
	
}
