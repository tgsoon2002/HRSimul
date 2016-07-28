using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

//Class that creates a new room for the user to design
public class CreateRoom : MonoBehaviour {

    public Canvas createRoomOverlay;
    public GameObject roomObj;

    public InputField lengthField;
    public InputField widthField;
    public InputField heightField;

    private float roomLength;
    private float roomWidth;
    private float roomHeight;

	// Use this for initialization
	void Start ()
    {
        createRoomOverlay = createRoomOverlay.GetComponent<Canvas>();
        lengthField = lengthField.GetComponent<InputField>();
        widthField = widthField.GetComponent<InputField>();
        heightField = heightField.GetComponent<InputField>();
    }

    //Closes the CreateRoomOverlay in the main menu screen
    public void CloseButtonPress()
    {
        createRoomOverlay.enabled = false;
    }

    //function that converts the inputs from the inputfields from strings
    //to integers
    public void ConvertInput()
    {
        roomLength = float.Parse(lengthField.text);
        roomWidth = float.Parse(widthField.text);
        roomHeight = float.Parse(heightField.text);
    }

    //creates a room made up of wall objects in the editor scene with 
    //the dimensions from the input fields
    public void CreateRoomPress()
    {
        PlayerPrefs.SetFloat("Length", roomLength);
        PlayerPrefs.SetFloat("Width", roomWidth);
        PlayerPrefs.SetFloat("Height", roomHeight);
        Application.LoadLevel(1);
    }

    //generates a room of the desired size in the editor scene 
    public void GenerateRoom()
    {
        roomLength = PlayerPrefs.GetFloat("Length");
        roomWidth = PlayerPrefs.GetFloat("Width");
        roomHeight = PlayerPrefs.GetFloat("Height");

        roomObj.transform.localScale += new Vector3(roomLength, roomHeight, roomWidth);
    }

    //Function to check an see if a new level was loaded. Specifically if the 
    //editor scene level(1) was loaded it will call the GenerateRoomSize function
	public void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            GenerateRoom();
        }
    }
}
