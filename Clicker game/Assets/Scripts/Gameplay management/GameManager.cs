using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    [Header("Canvas of building details (assign manually)")]
    public GameObject buildingDetailsCanvas;
    public TextMeshProUGUI buildingLevel;
    //[Header("Indicate what size of platform is selected in UI (Do not edit)")]
    //public GameObject platformSelectedInUI;
    [Header("Indicate what is selected in UI (Do not edit)")]
    public GameObject buildingSelectedInUI;
    public float buildingCost;
    [Header("Indicate what is selected in UI (assign manually)")]
    public GameObject buildingInfoCanvas;
    [Header("Indicate what is selected in scene (Do not edit)")]
    public GameObject buildingSelectedInScene;
    // -1: null, 0: buildings, 1: platforms
    public int buildingObjectType = -1;
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
    public BuildingBluePrint building6;
    public BuildingBluePrint building7;
    public BuildingBluePrint platform1;
    public BuildingBluePrint platform2;
    public GameObject ruin1;
    public GameObject ruin2;
    public GameObject ruin3;
    public GameObject ruin4;
    public GameObject ruin5;
    public GameObject ruin6;
    public GameObject ruin7;
    public GameObject bullet;

    [Header("Building Buttons")]
    public Button buildingButton1;
    public Button buildingButton2;
    public Button buildingButton3;
    public Button buildingButton4;
    //[Header("buildingStats")]
    //public GameObject[] list_house;
    //public GameObject[] list_factory;
    //public GameObject[] list_park;
    //public GameObject[] list_generator;

    public GameObject placeHolder;

    public bool buildingPurchasingState = false;
    public bool isTurretInPlanning = false;
    [Header("State")]
    public bool canInput;
    public bool isPaused = false;

    [Header("Node animation")]
    public GameObject[] allNodes;
    public GameObject[] allPlatformNodes;
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

    private void Start()
    {
        Time.timeScale = 1;
        InvokeRepeating("GetRefInvokeFunction", 0f, 0.05f);
    }
    // Button event
    public void SelectBuilding1()
    {
        // Audio
        AudioManager.instance.Play(SoundList.ButtonClicked);

        buildingSelectedInUI = building1.building;
        buildingCost = building1.cost;
        buildingObjectType = 0;

        UIManager.i.buildingInfo_name.text = "House";
        UIManager.i.buildingInfo_price.text = "$60";
        UIManager.i.buildingInfo.text = "Buffs the efficiency of the closest 4 nearby buildings.";
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
        buildingObjectType = 0;

        UIManager.i.buildingInfo_name.text = "Factory";
        UIManager.i.buildingInfo_price.text = "$100";
        UIManager.i.buildingInfo.text = "Generate resources every 20 seconds. Gains efficiency buff from nearby houses, but will pollute nearby parks.";
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
        buildingObjectType = 0;

        UIManager.i.buildingInfo_name.text = "Park";
        UIManager.i.buildingInfo_price.text = "$80";
        UIManager.i.buildingInfo.text = "The clear air generated every 20 seconds can temporary keep out the pollution. Gains efficiency buff from nearby houses, but remember not to put near factories.";
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
        buildingObjectType = 0;

        UIManager.i.buildingInfo_name.text = "Meteor defense";
        UIManager.i.buildingInfo_price.text = "$600";
        UIManager.i.buildingInfo.text = "Powerful laser beams protect your base from asteroids. Gains shooting speed buff from nearby houses.";
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
        buildingObjectType = 0;

        UIManager.i.buildingInfo_name.text = "Airport";
        UIManager.i.buildingInfo_price.text = "$800";
        UIManager.i.buildingInfo.text = "Build airplanes to explore the outside world, treasures are waiting for you! Gains airplane travel speed buff from nearby houses.";
        if (buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }
        buildingSelectedInScene = null;
    }
    public void SelectBuilding6()
    {
        // Audio
        AudioManager.instance.Play(SoundList.ButtonClicked);

        // A cat always lands on her feet whereas a bread with butter always fall buttered side down. 
        buildingSelectedInUI = building6.building;
        buildingCost = building6.cost;
        buildingObjectType = 0;

        UIManager.i.buildingInfo_name.text = "Logistic center";
        UIManager.i.buildingInfo_price.text = "$1500";
        UIManager.i.buildingInfo.text = "Auto collect nearby resources from buildings.";
        if (buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }
        buildingSelectedInScene = null;
    }
    public void SelectBuilding7()
    {
        // Audio
        AudioManager.instance.Play(SoundList.ButtonClicked);

        // A cat always lands on her feet whereas a bread with butter always fall buttered side down. 
        buildingSelectedInUI = building7.building;
        buildingCost = building7.cost;
        buildingObjectType = 0;

        UIManager.i.buildingInfo_name.text = "Perpetual machine";
        UIManager.i.buildingInfo_price.text = "$3000";
        UIManager.i.buildingInfo.text = "Do you know what will happen if you tie a buttered bread with cat? (Significantly increase the efficiency of factories.)";
        if (buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }
        buildingSelectedInScene = null;
    }
    public void SelectPlatform()
    {
        // Audio
        AudioManager.instance.Play(SoundList.ButtonClicked);

        // A cat always lands on her feet whereas a bread with butter always fall buttered side down. 
        buildingSelectedInUI = platform1.building;
        buildingCost = platform1.cost;
        buildingObjectType = 1;

        UIManager.i.buildingInfo_name.text = "Platform";
        UIManager.i.buildingInfo_price.text = "$150";
        UIManager.i.buildingInfo.text = "Expand your territory! You can obtain additional platforms if you reach certain objectives or after a airplane travel.";
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

        // Add back the estimated number of platforms.
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].tag == "PlatformPlaces")
            {
                SpecialBuildingCount.platform1Count += 1;
            }
        }

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
        SaveData.current.money = Currency.MONEY;
        SaveData.current.pollution = Pollution.POLLUTION;

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
                // Turret shooting speed buff = building production buff * 0.2
                // therefore it needs to be mutiplied by 20 instead of 100.
                // Set building name in the UI
                UIManager.i.Line1Text.text = "Nearby resources production +" + buildingSelectedInScene.GetComponent<House>().baseEfficiency * 100 + "%";
                UIManager.i.Line2Text.text = "Nearby turret shooting speed +" + buildingSelectedInScene.GetComponent<House>().baseEfficiency * 20 + "%";
                UIManager.i.Line3Text.text = "Nearby airplane travel time -" + buildingSelectedInScene.GetComponent<House>().baseEfficiency * 20 + "%";
                UIManager.i.Line4Text.text = "";
            }
            if (buildingSelectedInScene.tag == "Factory")
            {
                // Set building name in the UI
                UIManager.i.Line1Text.text = "Production: " + buildingSelectedInScene.GetComponent<Factory>().totalProduction +
                    " + " + buildingSelectedInScene.GetComponent<Factory>().extraProduction;
                UIManager.i.Line2Text.text = "Pollution: " + Math.Round(buildingSelectedInScene.GetComponent<Factory>().totalPollution, 2);
                UIManager.i.Line3Text.text = "Auto Production: " + buildingSelectedInScene.GetComponent<Factory>().moneyProduced_auto.ToString("F2") + "/s";
                UIManager.i.Line4Text.text = "Environmental pollution: " + 
                    (buildingSelectedInScene.GetComponent<Factory>().pollutionProduced_auto / buildingSelectedInScene.GetComponent<Factory>().pollutionInterval_auto).ToString("F2") + "/s";
            }
            if (buildingSelectedInScene.tag == "Park")
            {
                // Set building name in the UI
                if(buildingSelectedInScene.GetComponent<Park>().extraProduction > 0)
                {
                    UIManager.i.Line1Text.text = "Pollution reduction: " + buildingSelectedInScene.GetComponent<Park>().totalProduction.ToString("F2") +
                    " + " + buildingSelectedInScene.GetComponent<Park>().extraProduction.ToString("F2");
                }
                else if(buildingSelectedInScene.GetComponent<Park>().extraProduction == 0)
                {
                    UIManager.i.Line1Text.text = "Pollution reduction: " + buildingSelectedInScene.GetComponent<Park>().totalProduction.ToString("F2");
                }
                else if(buildingSelectedInScene.GetComponent<Park>().extraProduction < 0)
                {
                    UIManager.i.Line1Text.color = UIManager.i.textWarningColor;
                    UIManager.i.Line1Text.text = "Pollution reduction: " + buildingSelectedInScene.GetComponent<Park>().totalProduction.ToString("F2") +
                    " - " + Mathf.Abs(buildingSelectedInScene.GetComponent<Park>().extraProduction).ToString("F2");
                }
                
                UIManager.i.Line2Text.text = "Environmental pollution: -"
                    + (buildingSelectedInScene.GetComponent<Park>().CleanAirProduced_auto / buildingSelectedInScene.GetComponent<Park>().CleanAirInterval_auto).ToString("F2") + "/s";
                UIManager.i.Line3Text.text = "";
                UIManager.i.Line4Text.text = "";
            }
            if (buildingSelectedInScene.tag == "Generator")
            {
                // Set building name in the UI
                if(buildingSelectedInScene.GetComponent<Turret>().efficiency <= 1)
                {
                    UIManager.i.Line1Text.text = "Fire rate: " + buildingSelectedInScene.GetComponent<Turret>().fireRate_original.ToString("F2") + "/s";
                }
                else if(buildingSelectedInScene.GetComponent<Turret>().efficiency > 1)
                {
                    UIManager.i.Line1Text.text = "Fire rate: " + buildingSelectedInScene.GetComponent<Turret>().fireRate_original.ToString("F2") + " + " +
                    (buildingSelectedInScene.GetComponent<Turret>().fireRate_additional).ToString("F2") + " per second";
                }
                UIManager.i.Line2Text.text = "Environmental pollution: " + (buildingSelectedInScene.GetComponent<Turret>().pollutionProduced_auto.ToString("F2")) + "/s";
                UIManager.i.Line3Text.text = "";
                UIManager.i.Line4Text.text = "";
            }
            if (buildingSelectedInScene.tag == "Airport")
            {
                // Set building name in the UI
                UIManager.i.Line1Text.text = "Airplane speed: " + buildingSelectedInScene.GetComponent<Airport>().airplaneScript.relativeSpeed * 100;
                UIManager.i.Line2Text.text = "Travel time: " + buildingSelectedInScene.GetComponent<Airport>().airplaneScript.waitTime_des + " - " + 
                    (buildingSelectedInScene.GetComponent<Airport>().airplaneScript.waitTime_des - buildingSelectedInScene.GetComponent<Airport>().airplaneScript.waitTime_des_actial).ToString("F2") + "s";
                UIManager.i.Line3Text.text = "Pollution: " + buildingSelectedInScene.GetComponent<Airport>().pollutionProduced.ToString("F2");
                UIManager.i.Line4Text.text = "Environmental pollution: " +
                    (buildingSelectedInScene.GetComponent<Airport>().pollutionProduced_auto / buildingSelectedInScene.GetComponent<Airport>().pollutionInterval_auto).ToString("F2") + "/s";
                
            }
            if (buildingSelectedInScene.tag == "LogisticCenter")
            {
                // Set building name in the UI
                UIManager.i.Line1Text.text = "Collection interval: " + buildingSelectedInScene.GetComponent<LogisticCenter>().collectionSpeed + "s";
                UIManager.i.Line2Text.text = "Environmental pollution: " + buildingSelectedInScene.GetComponent<LogisticCenter>().pollution_auto + "/s";
                UIManager.i.Line3Text.text = "";
                UIManager.i.Line4Text.text = "";
            }
            if(buildingSelectedInScene.tag == "PerpetualMachine")
            {
                UIManager.i.Line1Text.text = "";
                UIManager.i.Line2Text.text = "";
                UIManager.i.Line3Text.text = "";
                UIManager.i.Line4Text.text = "";
            }
        }
        // Make resources avaliable to collect even in building pruchasing mode.
        if (Input.GetMouseButtonUp(0))
        {
            // prevent clicking through UI
            if (MouseOverUILayerObject.IsPointerOverUIObject())
            {
                return;
            }
            //if (TouchManager.instance.isDragging)
            //{
            //    return;
            //}
            if (buildingPurchasingState)
            {
                RaycastHit hit0;
                Ray ray0 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray0, out hit0))
                {
                    if (hit0.collider.gameObject.tag == "Factory")
                    {
                        if (hit0.collider.gameObject.GetComponent<Factory>().factoryPopUpREF)
                        {
                            hit0.collider.gameObject.GetComponent<Factory>().factoryPopUpREF.
                                GetComponent<FactoryPopUp>().ButtonEvent();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (hit0.collider.gameObject.tag == "Park")
                    {
                        if (hit0.collider.gameObject.GetComponent<Park>().parkPopUpREF)
                        {
                            hit0.collider.gameObject.GetComponent<Park>().parkPopUpREF.
                                GetComponent<ParkPopUp>().ButtonEvent();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (hit0.collider.gameObject.tag == "Airport")
                    {
                        if (hit0.collider.gameObject.GetComponent<Airport>().airportPopUpREF)
                        {
                            hit0.collider.gameObject.GetComponent<Airport>().airportPopUpREF.
                                GetComponent<AirportPopUp>().ButtonEvent();
                        }
                        else
                        {
                            return;
                        }
                    }
                    // Click ruin to trigger event on the pop up (UI)
                    else if (hit0.collider.gameObject.tag == "Ruin")
                    {
                        hit0.collider.gameObject.GetComponent<Ruin>().repairPopUp_REF.
                                GetComponent<RepairPopUp>().ButtonEvent();
                    }
                    // Town Hall
                    else if (hit0.collider.gameObject.tag == "MainBuilding")
                    {
                        hit0.collider.gameObject.GetComponent<MainBuilding>().MainBuildingClickEvent();
                    }
                    // not buildings
                    else
                    {
                        return;
                    }
                }
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
                            if(hit.collider.gameObject != buildingSelectedInScene)
                            {
                                // Audio
                                AudioManager.instance.Play(SoundList.SelectFactory);
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
                            if (hit.collider.gameObject != buildingSelectedInScene)
                            {
                                // Audio
                                AudioManager.instance.Play(SoundList.SelectPark);
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
                    }
                    else if (hit.collider.gameObject.tag == "House" || 
                        hit.collider.gameObject.tag == "Generator" || 
                        hit.collider.gameObject.tag == "LogisticCenter" ||
                        hit.collider.gameObject.tag == "PerpetualMachine")
                    {
                        if(hit.collider.gameObject != buildingSelectedInScene)
                        {
                            if (hit.collider.gameObject.tag == "House")
                            {
                                // Audio
                                AudioManager.instance.Play(SoundList.SelectHouse);
                            }
                            if (hit.collider.gameObject.tag == "Generator")
                            {
                                AudioManager.instance.Play(SoundList.SelectTurret);
                            }
                            if(hit.collider.gameObject.tag == "LogisticCenter")
                            {
                                AudioManager.instance.Play(SoundList.SelectHouse);
                            }
                            if(hit.collider.gameObject.tag == "PerpetualMachine")
                            {
                                AudioManager.instance.Play(SoundList.SelectFactory);
                            }
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
        PurchaseBuildingState();
        PauseGameState();
    }

    void PurchaseBuildingState()
    {
        // Check if the player is purchasing building (which means the variable "buildingSelectedInUI" is not null)
        if (buildingSelectedInUI != null)
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
        if (buildingPurchasingState && buildingSelectedInUI.gameObject.name != "Platform Small")
        {
            for (int i = 0; i < allNodes.Length; i++)
            {
                if (allNodes[i])
                    allNodes[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            for (int i = 0; i < allPlatformNodes.Length; i++)
            {
                if (allPlatformNodes[i])
                    allPlatformNodes[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else if (buildingPurchasingState && buildingSelectedInUI.gameObject.name == "Platform Small")
        {
            for (int i = 0; i < allNodes.Length; i++)
            {
                if (allNodes[i])
                    allNodes[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            for (int i = 0; i < allPlatformNodes.Length; i++)
            {
                if (allPlatformNodes[i])
                    allPlatformNodes[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < allNodes.Length; i++)
            {
                if (allNodes[i])
                    allNodes[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            for (int i = 0; i < allPlatformNodes.Length; i++)
            {
                if (allPlatformNodes[i])
                    allPlatformNodes[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    void PauseGameState()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
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

    public void GetRefInvokeFunction()
    {
        allNodes = GetAllNodes();
        allPlatformNodes = GetAllPlatformNodes();
    }
    public GameObject[] GetAllNodes()
    {
        return GameObject.FindGameObjectsWithTag("Node");
    }
    public GameObject[] GetAllPlatformNodes()
    {
        return GameObject.FindGameObjectsWithTag("PlatformPlaces");
    }

    public void PauseGameMenu()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);
        buildingSelectedInScene = null;
        buildingObjectType = -1;
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
    public void SaveAndQuit()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);
        SceneManager.LoadScene(0);
    }

    public void ResetCamera()
    {
        Camera.main.transform.position = new Vector3(4.25f, 6.5f, -5.2f);
    }

    void DisableGameUI()
    {
        UIManager.i.TopUI.SetActive(false);
        UIManager.i.BottomUI.SetActive(false);
        UIManager.i.RightUI.SetActive(false);
        UIManager.i.MainMenuUI.SetActive(true);
        buildingDetailsCanvas.SetActive(false);
        buildingInfoCanvas.SetActive(false);
    }
    void EnableGameUI()
    {
        UIManager.i.TopUI.SetActive(true);
        UIManager.i.BottomUI.SetActive(true);
        UIManager.i.RightUI.SetActive(true);
        UIManager.i.MainMenuUI.SetActive(false);
    }
}
