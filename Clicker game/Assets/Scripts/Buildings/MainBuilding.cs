using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : MonoBehaviour
{
    [Header("What to instantiate")]
    public GameObject mainBuildingPopUp;
    [Header("Where to instantiate (Do not edit)")]
    public GameObject popupStorageCanvas;
    [Header("Attached pop up (Do not edit)")]
    public GameObject mainBuildingPopUpREF;

    void Start()
    {
        popupStorageCanvas = GameObject.FindGameObjectWithTag("StorageCanvas");
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Currency.MONEY += 1;
            mainBuildingPopUpREF = Instantiate(mainBuildingPopUp, transform.position, Quaternion.identity);
            mainBuildingPopUpREF.transform.SetParent(popupStorageCanvas.transform);
        }
    }
}
