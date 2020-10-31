using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

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
    public BuildingBluePrint building5;
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
    public bool isTurretInPlanning = false;
    public bool isPaused = false;

    [Header("Node animation")]
    public GameObject[] allNodes;
    Touch touch;

    void Awake()
    {
        //debug
        if (i != null)
        {
            Debug.LogError("More than one GameManager in scene");
            return;
        }
        i = this;
    }

    // Button event
    public void SelectBuilding1()
    {
        // Audio
        AudioManager.instance.Play(SoundList.ButtonClicked);

        buildingSelectedInUI = building1.building;
        buildingCost = building1.cost;
        buildingInfo.text = "House ($60)- Buffs the efficiency of nearby factorys, whereas the nearby parks produces extra clear air.";
        if(buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }
        buildingSelectedInScene = null;
    }
    public void SelectBuilding2()
    {
        // Audio
        AudioManager.instance.Play(SoundList.ButtonClicked);

        // Named factory, but this is the dark market where drug dealer do transaction here...........
        buildingSelectedInUI = building2.building;
        buildingCost = building2.cost;
        buildingInfo.text = "Factory ($100) - Generate resources every 20 seconds. Gains efficiency buff from nearby houses, but will pollute nearby parks.";
        if (buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }
        buildingSelectedInScene = null;
    }
    public void SelectBuilding3()
    {
        // Audio
        AudioManager.instance.Play(SoundList.ButtonClicked);

        buildingSelectedInUI = building3.building;
        buildingCost = building3.cost;
        buildingInfo.text = "Park ($80) - The clear air generated every 20 seconds by plants can temporary keep out the pollution - But remember not to put it near factories";
        if (buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }

        buildingSelectedInScene = null;
    }
    public void SelectBuilding4()
    {
        // Audio
        AudioManager.instance.Play(SoundList.ButtonClicked);

        // A cat always lands on her feet whereas a bread with butter always fall buttered side down. 
        buildingSelectedInUI = building4.building;
        buildingCost = building4.cost;
        buildingInfo.text = "Meteor defense ($400) - The powerful laser beams protect your base from asteroids.";
        if (buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }
        buildingSelectedInScene = null;
    }
    public void SelectBuilding5()
    {
        // Audio
        AudioManager.instance.Play(SoundList.ButtonClicked);

        // A cat always lands on her feet whereas a bread with butter always fall buttered side down. 
        buildingSelectedInUI = building5.building;
        buildingCost = building5.cost;
        buildingInfo.text = "Airport ($800) - Build airplanes to explore the outside world - numerous treasures are waiting for you!";
        if (buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }
        buildingSelectedInScene = null;
    }
    public void OK_PurchaseBuilding()
    {
        // Audio
        AudioManager.instance.Play(SoundList.ButtonClicked);

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
        // Audio
        AudioManager.instance.Play(SoundList.Cancel);

        // Remove all placeHolder
        for (int i = 0; i < nodeList.Count; i++)
        {
            Node nodeWithPlaceHolder = nodeList[i].GetComponent<Node>();
            nodeWithPlaceHolder.RemovePlaceHolder();
            nodeWithPlaceHolder.RemoveBuildingPlaceHolder();
        }

        // Clear node list reference
        nodeList.Clear();
        futureBuildingList.Clear();

        // Add back the money deducted, than clear the money list
        for (int i = 0; i < estimatedCostList.Count; i++)
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
        BuildingLevel buildingLevel = buildingSelectedInScene.GetComponent<BuildingLevel>();
        if (Currency.MONEY <= buildingLevel.costEachLevel[buildingLevel.level])
        {
            AudioManager.instance.Play(SoundList.Error);
            return;
        }
        else
        {
            // level 1 --> 2
            // buildingLevel.level = 1
            
            // level 2 --> 3
            // buildingLevel.level = 2
            AudioManager.instance.Play(SoundList.ButtonClicked);
            Currency.MONEY -= buildingLevel.costEachLevel[buildingLevel.level];
            //Debug.Log("buildingLevel.costEachLevel[buildingLevel.level] = " + buildingLevel.costEachLevel[buildingLevel.level]);
            buildingLevel.level += 1;
            //Debug.Log("buildingLevel.level = " + buildingLevel.level);
        }
    }

    public void Sell()
    {
        // Remove ref on node properly
        AudioManager.instance.Play(SoundList.Cancel);
        buildingSelectedInScene.GetComponent<BuildingState>().node.GetComponent<Node>().building_REF = null;
        Destroy(buildingSelectedInScene);
        Currency.MONEY += buildingSelectedInScene.GetComponent<BuildingLevel>().sellCost;
    }

    private void Update()
    {
        list_house = GameObject.FindGameObjectsWithTag("House");
        list_factory = GameObject.FindGameObjectsWithTag("Factory");
        list_park = GameObject.FindGameObjectsWithTag("Park");
        list_generator = GameObject.FindGameObjectsWithTag("Generator");

        if(buildingSelectedInScene)
        {
            BuildingLevel buildingLevel = buildingSelectedInScene.GetComponent<BuildingLevel>();
            BuildingSelection buildingSelection = buildingSelectedInScene.GetComponent<BuildingSelection>();
            UIManager.i.sellText.text = "$" + buildingLevel.sellCost;
            UIManager.i.upgradeText.text = "$" + buildingLevel.costEachLevel[buildingLevel.level];
            UIManager.i.image.sprite = buildingSelection.sprite;
            UIManager.i.buildingName.text = buildingSelection.buildingName + " (LEVEL " + buildingLevel.level + ")";
            UIManager.i.Line1Text.color = UIManager.i.textDefaultColor;

            // Disable button when maximum level:
            if (buildingLevel.level >= buildingLevel.maxLevel)
            {
                UIManager.i.upgradeButton.interactable = false;
                UIManager.i.upgradeText.text = "MAX";
            }
            else
            {
                UIManager.i.upgradeButton.interactable = true;
                UIManager.i.upgradeText.text = "$" + buildingLevel.costEachLevel[buildingLevel.level];
            }

            // Building detail text
            if (buildingSelectedInScene.tag == "House")
            {
                // Set building name in the UI
                UIManager.i.Line1Text.text = "Efficiency: " + buildingSelectedInScene.GetComponent<House>().efficiency * 100 + "%";
                UIManager.i.Line2Text.text = "";
                UIManager.i.Line3Text.text = "";
                UIManager.i.Line4Text.text = "";
            }
            if (buildingSelectedInScene.tag == "Factory")
            {
                // Set building name in the UI
                UIManager.i.Line1Text.text = "Production: " + buildingSelectedInScene.GetComponent<Factory>().totalProduction +
                    " + " + buildingSelectedInScene.GetComponent<Factory>().extraProduction;
                UIManager.i.Line2Text.text = "Pollution: " + Math.Round(buildingSelectedInScene.GetComponent<Factory>().totalPollution, 2);
                UIManager.i.Line3Text.text = "Auto Production: " + buildingSelectedInScene.GetComponent<Factory>().moneyProduced_auto + " per second";
                UIManager.i.Line4Text.text = "Auto Pollution: " + 
                    (buildingSelectedInScene.GetComponent<Factory>().pollutionProduced_auto / buildingSelectedInScene.GetComponent<Factory>().pollutionInterval_auto) + " per second";
            }
            if (buildingSelectedInScene.tag == "Park")
            {
                // Set building name in the UI
                if(buildingSelectedInScene.GetComponent<Park>().extraProduction >= 0)
                {
                    UIManager.i.Line1Text.text = "Pollution reduced: " + buildingSelectedInScene.GetComponent<Park>().totalProduction +
                    " + " + buildingSelectedInScene.GetComponent<Park>().extraProduction;
                }
                else
                {
                    UIManager.i.Line1Text.color = UIManager.i.textWarningColor;
                    UIManager.i.Line1Text.text = "Pollution reduced: " + buildingSelectedInScene.GetComponent<Park>().totalProduction +
                    " - " + Mathf.Abs(buildingSelectedInScene.GetComponent<Park>().extraProduction);
                }
                
                UIManager.i.Line2Text.text = "Auto Production: " 
                    + (buildingSelectedInScene.GetComponent<Park>().CleanAirProduced_auto / buildingSelectedInScene.GetComponent<Park>().CleanAirInterval_auto) + " per second";
                UIManager.i.Line3Text.text = "";
                UIManager.i.Line4Text.text = "";
            }
            if (buildingSelectedInScene.tag == "Generator")
            {
                // Set building name in the UI
                UIManager.i.Line1Text.text = "Fire rate: " + buildingSelectedInScene.GetComponent<Turret>().fireRate + " per second";
                UIManager.i.Line2Text.text = "";
                UIManager.i.Line3Text.text = "";
                UIManager.i.Line4Text.text = "";
            }
            if (buildingSelectedInScene.tag == "Airport")
            {
                // Set building name in the UI
                UIManager.i.Line1Text.text = "Copter speed: " + buildingSelectedInScene.GetComponent<Airport>().airplaneScript.relativeSpeed * 100;
                UIManager.i.Line2Text.text = "Time taken to collect resources: " + buildingSelectedInScene.GetComponent<Airport>().airplaneScript.waitTime_des + " seconds";
                UIManager.i.Line3Text.text = "Auto Pollution: " +
                    (buildingSelectedInScene.GetComponent<Airport>().pollutionProduced_auto / buildingSelectedInScene.GetComponent<Airport>().pollutionInterval_auto) + " per second";
                UIManager.i.Line4Text.text = "";
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // prevent clicking through UI
            if (MouseOverUILayerObject.IsPointerOverUIObject())
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
                    if (hit.collider.gameObject.tag == "Factory")
                    {
                        if (hit.collider.gameObject.GetComponent<Factory>().factoryPopUpREF)
                        {
                            hit.collider.gameObject.GetComponent<Factory>().factoryPopUpREF.
                                GetComponent<FactoryPopUp>().ButtonEvent();
                        }
                        else
                        {
                            // Audio
                            AudioManager.instance.Play(SoundList.SelectFactory);

                            buildingSelectedInScene = hit.collider.gameObject;
                            StartCoroutine(buildingSelectedInScene.GetComponent<BuildingSelection>().BuildingPopAnimation());
                            buildingDetailsCanvas.SetActive(true);

                            // Remove reference of builing selected in the UI
                            GameManager.i.buildingSelectedInUI = null;
                            GameManager.i.buildingCost = 0;

                            // Animation
                            BuildingAnimation buildingAnimation = buildingSelectedInScene.GetComponent<BuildingAnimation>();
                            buildingAnimation.StartCoroutine(buildingAnimation.BuildingPopAnimation());
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
                            // Audio
                            AudioManager.instance.Play(SoundList.SelectPark);

                            buildingSelectedInScene = hit.collider.gameObject;
                            StartCoroutine(buildingSelectedInScene.GetComponent<BuildingSelection>().BuildingPopAnimation());
                            buildingDetailsCanvas.SetActive(true);

                            // Remove reference of builing selected in the UI
                            GameManager.i.buildingSelectedInUI = null;
                            GameManager.i.buildingCost = 0;

                            // Animation
                            BuildingAnimation buildingAnimation = buildingSelectedInScene.GetComponent<BuildingAnimation>();
                            buildingAnimation.StartCoroutine(buildingAnimation.BuildingPopAnimation());
                        }
                    }
                    else if (hit.collider.gameObject.tag == "House" ||
                    hit.collider.gameObject.tag == "Generator")
                    {
                        if(hit.collider.gameObject.tag == "House")
                        {
                            // Audio
                            AudioManager.instance.Play(SoundList.SelectHouse);
                        }
                        if (hit.collider.gameObject.tag == "Generator")
                        {
                            AudioManager.instance.Play(SoundList.SelectTurret);
                        }

                        buildingSelectedInScene = hit.collider.gameObject;
                        StartCoroutine(buildingSelectedInScene.GetComponent<BuildingSelection>().BuildingPopAnimation());
                        buildingDetailsCanvas.SetActive(true);

                        // Remove reference of builing selected in the UI
                        GameManager.i.buildingSelectedInUI = null;
                        GameManager.i.buildingCost = 0;

                        // Animation
                        BuildingAnimation buildingAnimation = buildingSelectedInScene.GetComponent<BuildingAnimation>();
                        buildingAnimation.StartCoroutine(buildingAnimation.BuildingPopAnimation());
                    }
                    else if(hit.collider.gameObject.tag == "Airport")
                    {
                        if (hit.collider.gameObject.GetComponent<Airport>().airportPopUpREF)
                        {
                            hit.collider.gameObject.GetComponent<Airport>().airportPopUpREF.
                                GetComponent<AirportPopUp>().ButtonEvent();
                        }
                        else
                        {
                            AudioManager.instance.Play(SoundList.SelectAirport);

                            buildingSelectedInScene = hit.collider.gameObject;
                            StartCoroutine(buildingSelectedInScene.GetComponent<BuildingSelection>().BuildingPopAnimation());
                            buildingDetailsCanvas.SetActive(true);

                            // Remove reference of builing selected in the UI
                            GameManager.i.buildingSelectedInUI = null;
                            GameManager.i.buildingCost = 0;

                            // Animation
                            BuildingAnimation buildingAnimation = buildingSelectedInScene.GetComponent<BuildingAnimation>();
                            buildingAnimation.StartCoroutine(buildingAnimation.BuildingPopAnimation());
                        }
                    }
                    // Click ruin to trigger event on the pop up (UI)
                    else if (hit.collider.gameObject.tag == "Ruin")
                    {
                        hit.collider.gameObject.GetComponent<Ruin>().repairPopUp_REF.
                                GetComponent<RepairPopUp>().ButtonEvent();
                    }
                    //not buildings
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
                        GameManager.i.buildingSelectedInScene = null;

                        //Remove reference of builing selected in the UI
                        GameManager.i.buildingSelectedInUI = null;
                        GameManager.i.buildingCost = 0;
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
            UIManager.i.RightUI.SetActive(true);
            UIManager.i.BuildingInstructionUI.SetActive(true);

            // Right UI state
            if (nodeList.Count == 0)
            {
                UIManager.i.OKButton.interactable = false;
            }
            else
            {
                UIManager.i.OKButton.interactable = true;
            }
        }
        else if (buildingSelectedInUI == null)
        {
            buildingPurchasingState = false;
            UIManager.i.RightUI.SetActive(false);
            UIManager.i.BuildingInstructionUI.SetActive(false);
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
        if(isPaused)
        {
            UIManager.i.MainMenuUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            UIManager.i.MainMenuUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    

    // Check if the turret is on a plan to be built:
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

    public void PauseGameMenu()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);
        buildingSelectedInScene = null;
        buildingSelectedInUI = null;
        DisableGameUI();
        isPaused = true;
    }

    public void Resume()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);
        EnableGameUI();
        isPaused = false;
    }
    public void Save()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);
    }
    public void SaveAndQuit()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);
    }

    void DisableGameUI()
    {
        UIManager.i.TopUI.SetActive(false);
        UIManager.i.BottomUI.SetActive(false);
        UIManager.i.RightUI.SetActive(false);
        buildingDetailsCanvas.SetActive(false);
        buildingInfoCanvas.SetActive(false);
    }
    void EnableGameUI()
    {
        UIManager.i.TopUI.SetActive(true);
        UIManager.i.BottomUI.SetActive(true);
        UIManager.i.RightUI.SetActive(true);
    }
}
