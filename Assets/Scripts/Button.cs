using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
      
        GameManager.instance.gameStart();
        gameObject.SetActive(false);
        FindAnyObjectByType<Door>().setBool(true);
    }
}
