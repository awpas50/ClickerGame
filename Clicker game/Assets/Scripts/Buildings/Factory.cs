using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [Header("Manual collected resources (money)")]
    public float moneyProduced_popup;
    public float pollutionProduced_popup;
    public float interval_popup;

    [Header("Auto generated resources (money)")]
    public float moneyProduced_auto;
    public float moneyInterval_auto;
    public float pollutionProduced_auto;
    public float pollutionInterval_auto;

    [Header("What to instantiate")]
    public GameObject factoryPopUp;
    [Header("Where to instantiate (Do not edit)")]
    public GameObject popupStorageCanvas;
    [Header("Attached pop up (Do not edit)")]
    public GameObject factoryPopUpREF;

    void Start()
    {
        popupStorageCanvas = GameObject.FindGameObjectWithTag("StorageCanvas");
        StartCoroutine(Production_POP_UP(interval_popup));
        StartCoroutine(Production_AUTOMATIC(moneyProduced_auto, moneyInterval_auto));
        StartCoroutine(Pollution_AUTOMATIC(pollutionProduced_auto, pollutionInterval_auto));
    }

    IEnumerator Production_POP_UP(float interval)
    {
        while (factoryPopUpREF == null)
        {
            yield return new WaitForSeconds(interval);
            factoryPopUpREF = Instantiate(factoryPopUp, transform.position, Quaternion.identity);
            factoryPopUpREF.transform.SetParent(popupStorageCanvas.transform);
            factoryPopUpREF.GetComponent<FactoryPopUp>().factoryREF = gameObject;
        }
    }
    public void GetResources(float moneyProduced, float pollutionProduced)
    {
        Currency.MONEY += moneyProduced;
        Pollution.POLLUTION += pollutionProduced;
    }

    IEnumerator Production_AUTOMATIC(float moneyProduced, float interval)
    {
        while(true)
        {
            yield return new WaitForSeconds(interval);
            Currency.MONEY += moneyProduced;
        }
    }
    IEnumerator Pollution_AUTOMATIC(float pollutionProduced, float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Pollution.POLLUTION += pollutionProduced;
        }
    }
    
}
