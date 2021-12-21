using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISwitchCloseAndOpen : MonoBehaviour, IPointerClickHandler
{
    public bool objectActiveState;
    public GameObject[] objectsForInteraction;

    public float openDelay = 0;
    public float closeDelay = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(objectActiveState == false)
        {
            StartCoroutine(OpenWithDelay());
            
        }
        if(objectActiveState == true)
        {
            StartCoroutine(CloseWithDelay());
        }

        objectActiveState = !objectActiveState;
    }

    private IEnumerator OpenWithDelay()
    {
        yield return new WaitForSeconds(openDelay);
        if (objectsForInteraction.Length > 0)
        {
            for (int i = 0; i < objectsForInteraction.Length; i++)
            {
                objectsForInteraction[i].SetActive(true);
            }
        }
        yield return null;
    }

    private IEnumerator CloseWithDelay()
    {
        yield return new WaitForSeconds(closeDelay);
        if (objectsForInteraction.Length > 0)
        {
            for (int i = 0; i < objectsForInteraction.Length; i++)
            {
                objectsForInteraction[i].SetActive(false);
            }
        }
    }
}
