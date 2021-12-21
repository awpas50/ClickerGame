using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICloseAndOpen : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] whatToOpen;
    public GameObject[] whatToClose;

    public float openDelay = 0;
    public float closeDelay = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(OpenWithDelay());
        StartCoroutine(CloseWithDelay());
    }

    private IEnumerator OpenWithDelay()
    {
        yield return new WaitForSeconds(openDelay);
        if (whatToOpen.Length > 0)
        {
            for (int i = 0; i < whatToOpen.Length; i++)
            {
                whatToOpen[i].SetActive(true);
            }
        }
        yield return null;
    }

    private IEnumerator CloseWithDelay()
    {
        yield return new WaitForSeconds(closeDelay);
        if (whatToClose.Length > 0)
        {
            for (int i = 0; i < whatToClose.Length; i++)
            {
                whatToClose[i].SetActive(false);
            }
        }
    }
}
