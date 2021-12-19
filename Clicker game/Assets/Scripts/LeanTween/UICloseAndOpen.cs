using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICloseAndOpen : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] whatToOpen;
    public GameObject[] whatToClose;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (whatToOpen.Length > 0)
        {
            for (int i = 0; i < whatToOpen.Length; i++)
            {
                whatToOpen[i].SetActive(true);
            }
        }
        if (whatToClose.Length > 0)
        {
            for (int i = 0; i < whatToClose.Length; i++)
            {
                whatToClose[i].SetActive(false);
            }
        }
    }
}
