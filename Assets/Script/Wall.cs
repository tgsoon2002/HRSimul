using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Wall : MonoBehaviour, IPointerClickHandler
{
    public Material currentTexture;
    public bool isWall;
	
	public void OnPointerClick(PointerEventData eventData)
    {

    }
}
