using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Indicate what is selected in UI (Do not edit)")]
    public GameObject buildingSelectedInList;
    public float buildingCost;
    [Header("Indicate what is selected in scene (Do not edit)")]
    public GameObject buildingSelectedInScene;
    [Header("Indicate all the nodes selected in the scene (Do not edit)")]
    public List<GameObject> nodeList;
    [Header("Indicate total spent (Do not edit)")]
    public List<float> estimatedCostList;

    public BuildingBluePrint building1;
    public BuildingBluePrint building2;
    public BuildingBluePrint building3;
    public GameObject placeHolder;

    public bool buildingPurchasingState = false;
    void Awake()
    {
        //debug
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene");
            return;
        }
        instance = this;
    }
    // Button event
    public void SelectBuilding1()
    {
        buildingSelectedInList = building1.building;
        buildingCost = building1.cost;
    }
    public void SelectBuilding2()
    {
        buildingSelectedInList = building2.building;
        buildingCost = building2.cost;
    }
    public void SelectBuilding3()
    {
        buildingSelectedInList = building3.building;
        buildingCost = building3.cost;
    }
    public void OK_PurchaseBuilding()
    {
        // Remove placeHolder and building reference
        // Place building according to the building reference on the node
        for (int i = 0; i < nodeList.Count; i++)
        {
            if(nodeList[i] == null)
            {
                return;
            }
            else
            {
                Node nodeWithPlaceHolder = nodeList[i].GetComponent<Node>();
                nodeWithPlaceHolder.RemovePlaceHolder();
                nodeWithPlaceHolder.ConfirmPlaceBuilding();
                nodeWithPlaceHolder.RemoveBuildingPlaceHolder();
            }
        }
        // Clear node list reference
        nodeList.Clear();
        // Clear the estimated cost spent list
        estimatedCostList.Clear();
    }
    public void Cancel_PurchaseBuilding()
    {
        // Remove all placeHolder
        for(int i = 0; i < nodeList.Count; i++)
        {
            Node nodeWithPlaceHolder = nodeList[i].GetComponent<Node>();
            nodeWithPlaceHolder.RemovePlaceHolder();
            nodeWithPlaceHolder.RemoveBuildingPlaceHolder();
        }

        // Clear node list reference
        nodeList.Clear();

        // Add back the money deducted, than clear the money list
        for(int i = 0; i < estimatedCostList.Count; i++)
        {
            Currency.MONEY += estimatedCostList[i];
        }
        estimatedCostList.Clear();

        // Remove reference
        buildingSelectedInList = null;
        buildingCost = 0;
    }
    private void Update()
    {
        //if(buildingSelectedInList != null)
        //{
        //    buildingPurchasingState = true;
        //}
        //else
        //{
        //    buildingPurchasingState = false;
        //}
    }
}
