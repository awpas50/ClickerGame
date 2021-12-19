using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIOnMouseClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject whatToShow;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (whatToShow)
        {
            whatToShow.SetActive(true);
        }
    }
}
