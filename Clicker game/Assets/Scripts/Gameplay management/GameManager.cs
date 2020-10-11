using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Canvas of building details (assign manually)")]
    public GameObject buildingDetailsCanvas;
    
    public TextMeshProUGUI buildingLevel;
    [Header("Indicate what is selected in UI (Do not edit)")]
    public GameObject buildingSelectedInList;
    public float buildingCost;
    [Header("Indicate what is selected in UI (assign manually)")]
    public GameObject buildingInfoCanvas;
    public TextMeshProUGUI buildingInfo;
    [Header("Indicate what is selected in scene (Do not edit)")]
    public GameObject buildingSelectedInScene;
    [Header("Indicate all the nodes selected in the scene (Do not edit)")]
    public List<GameObject> nodeList;
    [Header("Indicate total spent (Do not edit)")]
    public List<float> estimatedCostList;

    public BuildingBluePrint building1;
    public BuildingBluePrint building2;
    public BuildingBluePrint building3;
    public BuildingBluePrint building4;
    [Header("Building Buttons")]
    public Button buildingButton1;
    public Button buildingButton2;
    public Button buildingButton3;
    public Button buildingButton4;
    [Header("buildingStats")]
    public GameObject[] list_house;
    public GameObject[] list_factory;
    public GameObject[] list_park;
    public GameObject[] list_generator;

    public GameObject placeHolder;

    public bool buildingPurchasingState = false;

    public GameObject Upgrade1;
    public GameObject Upgrade2;
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
        //buildingName.text = "House";
        buildingInfo.text = "House ($50)- Higher population buffs the efficiency of nearby factorys and parks.";

        buildingSelectedInScene = null;
    }
    public void SelectBuilding2()
    {
        // Named factory, but this is the dark market where drug dealer do transaction here...........
        buildingSelectedInList = building2.building;
        buildingCost = building2.cost;
        //buildingName.text = "Factory";
        buildingInfo.text = "Factory ($100) - Generate currency over a period of time. Gains benefit from nearby houses, but can pollution nearby park.";
        buildingSelectedInScene = null;
    }
    public void SelectBuilding3()
    {
        buildingSelectedInList = building3.building;
        buildingCost = building3.cost;
        //buildingName.text = "Park";
        buildingInfo.text = "Park ($75) - The clear air generator by plants can temporary keep out the pollution.";

        buildingSelectedInScene = null;
    }
    public void SelectBuilding4()
    {
        // A cat always lands on her feet whereas a bread with butter always fall buttered side down. 
        buildingSelectedInList = building4.building;
        buildingCost = building4.cost;
        //buildingName.text = "Perpetual motion machine";
        buildingInfo.text = "Perpetual motion machine ($100) - If the bread was fasten with the cat, Unlimited power source can be generated as the cat will keep rotating.";

        buildingSelectedInScene = null;
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
                // Remove place holder, and reset the index to all -1
                Node nodeWithPlaceHolder = nodeList[i].GetComponent<Node>();
                nodeWithPlaceHolder.RemovePlaceHolder();
                nodeWithPlaceHolder.ConfirmPlaceBuilding();
                nodeWithPlaceHolder.RemoveBuildingPlaceHolder();
                nodeWithPlaceHolder.nodeIndex = -1;
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
    public void Upgrade()
    {
        Debug.Log("Upgrade");
    }
    public void Sell()
    {
        Debug.Log("Sell");
        Destroy(buildingSelectedInScene);
    }
    private void Update()
    {
        list_house = GameObject.FindGameObjectsWithTag("House");
        list_factory = GameObject.FindGameObjectsWithTag("Factory");
        list_park = GameObject.FindGameObjectsWithTag("Park");
        list_generator = GameObject.FindGameObjectsWithTag("Generator");
        // make sure the building detail canvas won't show when nothing is selected
        if (buildingSelectedInScene)
        {
            buildingDetailsCanvas.SetActive(true);
        }
        else if(!buildingSelectedInScene)
        {
            buildingDetailsCanvas.SetActive(false);
        }

        // make sure the building info canvas won't show when nothing is selected
        if (buildingSelectedInList)
        {
            buildingInfoCanvas.SetActive(true);
        }
        else if (!buildingSelectedInList)
        {
            buildingInfoCanvas.SetActive(false);
        }
        for(int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].GetComponent<Node>().placeHolder_building_REF.gameObject.tag == "Generator")
            {
                buildingButton4.interactable = false;
            }
        }
        // Check if the generator exists in the scene
        if(list_generator.Length >= 1)
        {
            buildingButton4.interactable = false;
        }
        else
        {
            buildingButton4.interactable = true;
        }

        //// Objective
        //if(Currency.MONEY >= 5000)
        //{
        //    for(int i = 0; i < Upgrade1.transform.childCount; i++)
        //    {
        //        Upgrade1.transform.GetChild(i).gameObject.SetActive(true);
        //    }
        //}
        //// Objective
        //if (Currency.MONEY >= 20000)
        //{
        //    for (int i = 0; i < Upgrade2.transform.childCount; i++)
        //    {
        //        Upgrade2.transform.GetChild(i).gameObject.SetActive(true);
        //    }
        //}
    }
}
