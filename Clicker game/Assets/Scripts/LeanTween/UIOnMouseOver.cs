using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject whatToShow;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(whatToShow)
        {
            whatToShow.SetActive(true);
        }
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (whatToShow)
        {
            whatToShow.SetActive(false);
        }
    }
    public void OnDisable()
    {
        if (whatToShow)
        {
            whatToShow.SetActive(false);
        }
    }
}
