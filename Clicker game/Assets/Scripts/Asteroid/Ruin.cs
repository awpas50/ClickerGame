using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruin : MonoBehaviour
{
    [Header("Node ref (Assign automatically)")]
    public GameObject node;
    [Header("Building ref (Assign automatically)")]
    public GameObject buildingData;
    public int buildingLevel;
    public float repairCost;
    public int buildingType; // 1 ~ 5
    public Vector3 offset;
    [Header("What to Instantiate (assign maunally)")]
    public GameObject repairPopUp;
    [Header("repairPopUp")]
    public GameObject popupStorageCanvas;
    [Header("Indicator")]
    public GameObject repairPopUp_REF;
    [Header("Assigned manually")]
    public GameObject secondPopUp;
    
    void Start()
    {
        popupStorageCanvas = GameObject.FindGameObjectWithTag("StorageCanvas");
        repairPopUp_REF = Instantiate(repairPopUp, transform.position + offset, Quaternion.identity);
        // Assign ruin reference to pop up
        repairPopUp_REF.GetComponent<RepairPopUp>().ruinREF = gameObject;
        repairPopUp_REF.transform.SetParent(popupStorageCanvas.transform);
    }
}
