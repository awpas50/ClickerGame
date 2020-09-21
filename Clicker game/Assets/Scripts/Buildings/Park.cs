using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    [Header("Manual collected resources (Reduce pollution)")]
    public float pollutionReduced_popup;
    public float interval_popup;

    //[Header("Auto generated resources (Reduce pollution)")]
    //public float moneyProduced_auto;
    //public float moneyInterval_auto;
    //public float pollutionProduced_auto;
    //public float pollutionInterval_auto;

    [Header("What to instantiate")]
    public GameObject parkPopUp;
    [Header("Where to instantiate (Do not edit)")]
    public GameObject popupStorageCanvas;
    [Header("Attached pop up (Do not edit)")]
    public GameObject parkPopUpREF;
    [Header("Script reference (Do not edit)")]
    public BuildingBuff buildingBuff;
    [Header("Resources multiplier")]
    public float efficiency = 1f;

    void Start()
    {
        popupStorageCanvas = GameObject.FindGameObjectWithTag("StorageCanvas");
        buildingBuff = GetComponent<BuildingBuff>();
        StartCoroutine(ReducePollution_POP_UP(pollutionReduced_popup, interval_popup));
        //StartCoroutine(Pollution_AUTOMATIC(pollutionProduced_auto, pollutionInterval_auto));
    }
    private void Update()
    {
        // each house increase the efficiency by 25%, whereas each factory nearby decrease the efficiency by 25%. Minimum 20% output.
        if(efficiency > 0.2f)
        {
            efficiency = 1 + buildingBuff.nearbyHouse * 0.25f - buildingBuff.nearbyFactory * 0.4f;
        }
        if(efficiency <= 0.2f)
        {
            efficiency = 0.2f;
        }
    }

    public IEnumerator ReducePollution_POP_UP(float pollutionReduced, float interval)
    {
        while (parkPopUpREF == null)
        {
            yield return new WaitForSeconds(interval);
            parkPopUpREF = Instantiate(parkPopUp, transform.position, Quaternion.identity);
            parkPopUpREF.transform.SetParent(popupStorageCanvas.transform);
            parkPopUpREF.GetComponent<ParkPopUp>().parkREF = gameObject;
        }
    }
    public void GetResources(float pollutionReduced, float efficiency)
    {
        Pollution.POLLUTION -= pollutionReduced * efficiency;
    }
    //IEnumerator Pollution_AUTOMATIC(float pollutionProduced, float interval)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(interval);
    //        Pollution.POLLUTION += pollutionProduced;
    //    }
    //}
}
