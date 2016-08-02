using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

//Class to hold what texture/color (material) is assigned to each wall
//also holds a boolean variable to distinguish between a plane being a wall
//or the floor
public class Wall : MonoBehaviour, IPointerClickHandler
{
	public static Wall Instance; //type specifier

	public Material currentTexture;
    public bool isWall;
	
	public void OnPointerClick(PointerEventData eventData)
    {

    }
}
