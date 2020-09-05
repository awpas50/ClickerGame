using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelection : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Select building in scene
        GameManager.instance.buildingSelectedInScene = gameObject;

        //Remove reference of builing selected in the UI
        GameManager.instance.buildingSelectedInList = null;
        GameManager.instance.buildingCost = 0;
    }
}
