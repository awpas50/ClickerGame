using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Canvas of building details (assign manually)")]
    public GameObject buildingDetailsCanvas;
    public TextMeshProUGUI buildingLevel;
    [Header("Indicate what is selected in UI (Do not edit)")]
    public GameObject buildingSelectedInUI;
    public float buildingCost;
    [Header("Indicate what is selected in UI (assign manually)")]
    public GameObject buildingInfoCanvas;
    public TextMeshProUGUI buildingInfo;
    [Header("Indicate what is selected in scene (Do not edit)")]
    public GameObject buildingSelectedInScene;
    [Header("Indicate all the nodes selected in the scene (Do not edit)")]
    public List<GameObject> futureBuildingList;
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
    public bool isGeneratorInPlanning = false;

    [Header("Node animation")]
    public GameObject[] allNodes;

    Touch touch;
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
    private void Start()
    {
        // node animation
        // allNodes = GetAllNodes();
    }
    // Button event
    public void SelectBuilding1()
    {
        buildingSelectedInUI = building1.building;
        buildingCost = building1.cost;
        //buildingName.text = "House";
        buildingInfo.text = "House ($50)- Higher population buffs the efficiency of nearby factorys and parks.";
        if(buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }
        buildingSelectedInScene = null;
    }
    public void SelectBuilding2()
    {
        // Named factory, but this is the dark market where drug dealer do transaction here...........
        buildingSelectedInUI = building2.building;
        buildingCost = building2.cost;
        //buildingName.text = "Factory";
        buildingInfo.text = "Factory ($100) - Generate currency over a period of time. Gains benefit from nearby houses, but can pollution nearby park.";
        if (buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }
        buildingSelectedInScene = null;
    }
    public void SelectBuilding3()
    {
        buildingSelectedInUI = building3.building;
        buildingCost = building3.cost;
        //buildingName.text = "Park";
        buildingInfo.text = "Park ($75) - The clear air generator by plants can temporary keep out the pollution.";
        if (buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }

        buildingSelectedInScene = null;
    }
    public void SelectBuilding4()
    {
        // A cat always lands on her feet whereas a bread with butter always fall buttered side down. 
        buildingSelectedInUI = building4.building;
        buildingCost = building4.cost;
        //buildingName.text = "Perpetual motion machine";
        buildingInfo.text = "Meteor defense ($100) - If the bread was fasten with the cat, Unlimited power source can be generated as the cat will keep rotating.";
        if (buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }
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
        futureBuildingList.Clear();
        // Clear the estimated cost spent list
        estimatedCostList.Clear();
        // leave purchase state
        buildingSelectedInUI = null;
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
        buildingSelectedInUI = null;
        buildingCost = 0;
    }
    public void Upgrade()
    {
        Debug.Log("Upgrade");
        if(Currency.MONEY <= buildingSelectedInScene.GetComponent<BuildingLevel>().cost)
        {
            return;
        }
        else
        {
            buildingSelectedInScene.GetComponent<BuildingLevel>().level += 1;
            Currency.MONEY -= buildingSelectedInScene.GetComponent<BuildingLevel>().cost;
        }
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

        if (Input.GetMouseButtonUp(0))
        {
            // prevent clicking through UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (TouchManager.instance.isDragging)
            {
                return;
            }
            // Mouse click detection
            if (!buildingPurchasingState)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    //if (hit.collider.gameObject.tag == "MainBuilding")
                    //{
                    //    hit.collider.gameObject.GetComponent<MainBuilding>().MainBuildingClickEvent();
                    //}
                    if(hit.collider.gameObject.tag == "Factory")
                    {
                        if (hit.collider.gameObject.GetComponent<Factory>().factoryPopUpREF)
                        {
                            hit.collider.gameObject.GetComponent<Factory>().factoryPopUpREF.
                                GetComponent<FactoryPopUp>().ButtonEvent();
                        }
                        else
                        {
                            buildingSelectedInScene = hit.collider.gameObject;
                            StartCoroutine(buildingSelectedInScene.GetComponent<BuildingSelection>().BuildingPopAnimation());
                            buildingDetailsCanvas.SetActive(true);

                            // Remove reference of builing selected in the UI
                            GameManager.instance.buildingSelectedInUI = null;
                            GameManager.instance.buildingCost = 0;

                            // Set building name in the UI
                            UIManager.instance.buildingName.text = buildingSelectedInScene.GetComponent<BuildingSelection>().buildingName + " (LEVEL " + buildingSelectedInScene.GetComponent<BuildingLevel>().level + ")";
                            UIManager.instance.image.sprite = buildingSelectedInScene.GetComponent<BuildingSelection>().sprite;
                            //UIManager.instance.buildingLevel.text = "LEVEL " + buildingSelectedInScene.GetComponent<BuildingLevel>().level;
                        }
                        
                    }
                    else if(hit.collider.gameObject.tag == "Park")
                    {
                        if (hit.collider.gameObject.GetComponent<Park>().parkPopUpREF)
                        {
                            hit.collider.gameObject.GetComponent<Park>().parkPopUpREF.
                                GetComponent<ParkPopUp>().ButtonEvent();
                        }
                        else
                        {
                            buildingSelectedInScene = hit.collider.gameObject;
                            StartCoroutine(buildingSelectedInScene.GetComponent<BuildingSelection>().BuildingPopAnimation());
                            buildingDetailsCanvas.SetActive(true);

                            // Remove reference of builing selected in the UI
                            GameManager.instance.buildingSelectedInUI = null;
                            GameManager.instance.buildingCost = 0;

                            // Set building name in the UI
                            UIManager.instance.buildingName.text = buildingSelectedInScene.GetComponent<BuildingSelection>().buildingName + " (LEVEL " + buildingSelectedInScene.GetComponent<BuildingLevel>().level + ")";
                            UIManager.instance.image.sprite = buildingSelectedInScene.GetComponent<BuildingSelection>().sprite;
                            //UIManager.instance.buildingLevel.text = "LEVEL " + buildingSelectedInScene.GetComponent<BuildingLevel>().level;
                        }
                    }
                    else if (hit.collider.gameObject.tag == "House" ||
                    hit.collider.gameObject.tag == "Generator")
                    {

                        buildingSelectedInScene = hit.collider.gameObject;
                        StartCoroutine(buildingSelectedInScene.GetComponent<BuildingSelection>().BuildingPopAnimation());
                        buildingDetailsCanvas.SetActive(true);

                        // Remove reference of builing selected in the UI
                        GameManager.instance.buildingSelectedInUI = null;
                        GameManager.instance.buildingCost = 0;

                        // Set building name in the UI
                        UIManager.instance.buildingName.text = buildingSelectedInScene.GetComponent<BuildingSelection>().buildingName + " (LEVEL " + buildingSelectedInScene.GetComponent<BuildingLevel>().level + ")";
                        UIManager.instance.image.sprite = buildingSelectedInScene.GetComponent<BuildingSelection>().sprite;
                        //if(buildingSelectedInScene.GetComponent<BuildingLevel>())
                        //{
                        //    UIManager.instance.buildingLevel.text = "LEVEL " + buildingSelectedInScene.GetComponent<BuildingLevel>().level;
                        //}
                    }
                    else
                    {
                        buildingDetailsCanvas.SetActive(false);
                        //UIManager.instance.buildingName.text = buildingSelectedInScene.GetComponent<BuildingSelection>().buildingName;
                        // Remove indicator surrounded the building
                        if(buildingSelectedInScene)
                        {
                            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
                        }
                        // deselect building in scene
                        GameManager.instance.buildingSelectedInScene = null;

                        //Remove reference of builing selected in the UI
                        GameManager.instance.buildingSelectedInUI = null;
                        GameManager.instance.buildingCost = 0;
                    }
                    Debug.Log(hit.collider);
                }
            }
        }

        // make sure the building detail canvas won't show when nothing is selected
        if (buildingSelectedInScene)
        {
            buildingDetailsCanvas.SetActive(true);
        }
        else if(buildingSelectedInScene == null)
        {
            buildingDetailsCanvas.SetActive(false);
        }

        // Check if the generator exists in the scene
        //if(list_generator.Length >= 1)
        //{
        //    buildingButton4.interactable = false;
        //}
        //else
        //{
        //    buildingButton4.interactable = true;
        //}

        //isGeneratorInPlanning = IsGeneratorInPlanning();
        //if (isGeneratorInPlanning)
        //{
        //    buildingButton4.interactable = false;
        //}
        //else
        //{
        //    buildingButton4.interactable = true;
        //}

        // Check if the player is purchasing building (which means the variable "buildingSelectedInUI" is not null)
        if(buildingSelectedInUI != null)
        {
            buildingPurchasingState = true;
            UIManager.instance.RightUI.SetActive(true);
            UIManager.instance.BuildingInstructionUI.SetActive(true);
        }
        else if (buildingSelectedInUI == null)
        {
            buildingPurchasingState = false;
            UIManager.instance.RightUI.SetActive(false);
            UIManager.instance.BuildingInstructionUI.SetActive(false);
        }

        // base on "buildingPurchasingState" the mesh on each node (the first child, GetChild(0) will be disabled)
        if (buildingPurchasingState)
        {
            for(int i = 0; i < allNodes.Length; i++)
            {
                allNodes[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < allNodes.Length; i++)
            {
                allNodes[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    // Check if the generator is on a plan to be built:
    //private bool IsGeneratorInPlanning()
    //{
    //    if(futureBuildingList.Count == 0)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        for (int i = 0; i < futureBuildingList.Count; i++)
    //        {
    //            if (futureBuildingList[i].gameObject.tag != "Generator")
    //            {
    //                return false;
    //            }
    //        }
    //        return true;
    //    }
    //}

    public GameObject[] GetAllNodes()
    {
        return GameObject.FindGameObjectsWithTag("Node");
    }

}
