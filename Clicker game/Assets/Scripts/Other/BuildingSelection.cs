using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingSelection : MonoBehaviour
{
    public string buildingName;
    // This script is attached into the buildings
    // when a building is selected...
    private void OnMouseDown()
    {
        // Select building in scene
        GameManager.instance.buildingSelectedInScene = gameObject;

        //Remove reference of builing selected in the UI
        GameManager.instance.buildingSelectedInList = null;
        GameManager.instance.buildingCost = 0;

        UIManager.instance.buildingName.text = buildingName;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            // Select building in scene
            GameManager.instance.buildingSelectedInScene = null;

            //Remove reference of builing selected in the UI
            GameManager.instance.buildingSelectedInList = null;
            GameManager.instance.buildingCost = 0;

            UIManager.instance.buildingName.text = buildingName;
        }
    }
}
