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
    [Header("Time elapsed")]
    public float timeElapsed;
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

    // Upgrade++
    private int estimateCostToUpgrade = 0;
    private int estimatelevelsToUpgrade = 0;
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

    [Header("Camera Rotator")]
    public GameObject cameraRotator;
    [Header("Main Menu Buttons")]
    public GameObject mainMenuBG;
    public GameObject mainMenuBtn1;
    public GameObject mainMenuBtn2;
    public GameObject mainMenuBtn3;
    public GameObject mainMenuBtn4;
    public GameObject musicText;
    public GameObject sfxText;
    public GameObject musicSlider;
    public GameObject sfxSlider;
    [Header("VFX")]
    public GameObject upgradeVFX;

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
        canInput = true;
        Time.timeScale = 1;
        InvokeRepeating("GetRefInvokeFunction", 0f, 0.05f);
    }
    // Button event
    public void SelectBuilding1()
    {
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

        // A cat always lands on her feet whereas a bread with butter always fall buttered side down. 
        buildingSelectedInUI = building6.building;
        buildingCost = building6.cost;
        buildingObjectType = 0;

        UIManager.i.buildingInfo_name.text = "Logistic center";
        UIManager.i.buildingInfo_price.text = "$1000";
        UIManager.i.buildingInfo.text = "Auto collect nearby resources from buildings. Grant 4x buffs when collecting resources from town hall.";
        if (buildingSelectedInScene)
        {
            buildingSelectedInScene.GetComponent<BuildingSelection>().indicator.SetActive(false);
        }
        buildingSelectedInScene = null;
    }
    public void SelectBuilding7()
    {
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
        for (int i = 0; i < nodeList.Count; i++)
        {
            GameObject tempVFX = Instantiate(upgradeVFX, nodeList[i].transform.position, Quaternion.identity);
            Destroy(tempVFX, 5f);
        }
        
        // Sound
        AudioManager.instance.Play(SoundList.BuildingComplete);
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
        GameObject tempVFX = Instantiate(upgradeVFX, buildingSelectedInScene.transform.position, Quaternion.identity);
        Destroy(tempVFX, 5f);
        BuildingLevel buildingLevel = buildingSelectedInScene.GetComponent<BuildingLevel>();
        if (Currency.MONEY <= buildingLevel.costEachLevel[buildingLevel.level])
        {
            AudioManager.instance.Play(SoundList.Error);
            return;
        }
        else
        {
            // Sound
            AudioManager.instance.Play(SoundList.BuildingUpgrade);
            // level 1 --> 2
            // buildingLevel.level = 1
            // level 2 --> 3
            // buildingLevel.level = 2
            Currency.MONEY -= buildingLevel.costEachLevel[buildingLevel.level];
            //Debug.Log("buildingLevel.costEachLevel[buildingLevel.level] = " + buildingLevel.costEachLevel[buildingLevel.level]);
            buildingLevel.level += 1;
            //Debug.Log("buildingLevel.level = " + buildingLevel.level);
        }
    }
    public void UpgradePlus()
    {
        GameObject tempVFX = Instantiate(upgradeVFX, buildingSelectedInScene.transform.position, Quaternion.identity);
        Destroy(tempVFX, 5f);
        BuildingLevel buildingLevel = buildingSelectedInScene.GetComponent<BuildingLevel>();
        if (Currency.MONEY <= estimateCostToUpgrade)
        {
            AudioManager.instance.Play(SoundList.Error);
            return;
        }
        else
        {
            // Sound
            AudioManager.instance.Play(SoundList.BuildingUpgrade);
            // level 1 --> 2
            // buildingLevel.level = 1
            // level 2 --> 3
            // buildingLevel.level = 2
            Currency.MONEY -= estimateCostToUpgrade;
            //Debug.Log("buildingLevel.costEachLevel[buildingLevel.level] = " + buildingLevel.costEachLevel[buildingLevel.level]);
            buildingLevel.level += estimatelevelsToUpgrade;
            //Debug.Log("buildingLevel.level = " + buildingLevel.level);
        }
    }
    public void Sell()
    {
        // Sound
        AudioManager.instance.Play(SoundList.BuildingSold);
        buildingSelectedInScene.GetComponent<BuildingState>().node.GetComponent<Node>().building_REF = null;
        Destroy(buildingSelectedInScene);
        Currency.MONEY += buildingSelectedInScene.GetComponent<BuildingLevel>().sellCost;
    }
    private void Update()
    {
        // Record elapsed time
        timeElapsed += Time.deltaTime;
        // Get money and pollution
        SaveData.current.money = Currency.MONEY;
        SaveData.current.pollution = Pollution.POLLUTION;

        if (buildingSelectedInScene)
        {
            BuildingLevel buildingLevel = buildingSelectedInScene.GetComponent<BuildingLevel>();
            BuildingSelection buildingSelection = buildingSelectedInScene.GetComponent<BuildingSelection>();
            UIManager.i.sellText.text = "$" + buildingLevel.sellCost;
            UIManager.i.upgradeText.text = "$" + buildingLevel.costEachLevel[buildingLevel.level];
            UIManager.i.upgradePlusText.text = "$" + buildingLevel.costEachLevel[buildingLevel.level];
            UIManager.i.image.sprite = buildingSelection.sprite;
            UIManager.i.buildingName.text = buildingSelection.buildingName + " (LEVEL " + buildingLevel.level + ")";
            UIManager.i.Line1Text.color = UIManager.i.textDefaultColor;

            // Get upgrade++ estimate cost
            estimateCostToUpgrade = buildingLevel.costEachLevel[buildingLevel.level];
            estimatelevelsToUpgrade = 1;
            for (int i = buildingLevel.level + 1; i < buildingLevel.maxLevel; i++)
            {
                if (estimateCostToUpgrade + buildingLevel.costEachLevel[i] <= Currency.MONEY)
                {
                    estimateCostToUpgrade += buildingLevel.costEachLevel[i];
                    estimatelevelsToUpgrade += 1;
                }
                else
                {
                    break;
                }
            }

            // Disable button when maximum level:
            if (buildingLevel.level >= buildingLevel.maxLevel)
            {
                UIManager.i.upgradeButton.interactable = false;
                UIManager.i.upgradePlusButton.interactable = false;
                //UIManager.i.upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Level max";
                UIManager.i.upgradeText.text = "";
                UIManager.i.upgradePlusText.text = "";
            }
            else if(buildingLevel.costEachLevel[buildingLevel.level] > Currency.MONEY)
            {
                UIManager.i.upgradeButton.interactable = false;
                UIManager.i.upgradePlusButton.interactable = false;
                //UIManager.i.upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade";
                UIManager.i.upgradeText.text = "$" + buildingLevel.costEachLevel[buildingLevel.level];
                UIManager.i.upgradePlusText.text = "$" + buildingLevel.costEachLevel[buildingLevel.level];
            }
            else
            {
                UIManager.i.upgradeButton.interactable = true;
                UIManager.i.upgradePlusButton.interactable = true;
                //UIManager.i.upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade";
                UIManager.i.upgradeText.text = "$" + buildingLevel.costEachLevel[buildingLevel.level];
                UIManager.i.upgradePlusText.text = "$" + estimateCostToUpgrade;
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
                UIManager.i.Line1Text.text = "Production: " + 
                    buildingSelectedInScene.GetComponent<Factory>().totalProduction +" + " + 
                    buildingSelectedInScene.GetComponent<Factory>().extraProduction;
                UIManager.i.Line2Text.text = "Pollution: " + Math.Round(buildingSelectedInScene.GetComponent<Factory>().totalPollution, 2);
                UIManager.i.Line3Text.text = "Auto Production: " + buildingSelectedInScene.GetComponent<Factory>().moneyProduced_auto.ToString("F2") + "/s";
                UIManager.i.Line4Text.text = "Environmental pollution: " + 
                    buildingSelectedInScene.GetComponent<Factory>().pollutionProduced_auto_base.ToString("F2") + " + " +
                    buildingSelectedInScene.GetComponent<Factory>().pollutionProduced_auto_extra.ToString("F2") + "/s";
            }
            if (buildingSelectedInScene.tag == "Park")
            {
                // Set building name in the UI
                if(buildingSelectedInScene.GetComponent<Park>().extraProduction > 0)
                {
                    UIManager.i.Line1Text.text = "Pollution reduction: " + 
                        buildingSelectedInScene.GetComponent<Park>().totalProduction.ToString("F2") + " + " + 
                        buildingSelectedInScene.GetComponent<Park>().extraProduction.ToString("F2");
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
                    (buildingSelectedInScene.GetComponent<Turret>().fireRate_additional).ToString("F2") + "/s";
                }
                UIManager.i.Line2Text.text = "Environmental pollution: " + 
                    buildingSelectedInScene.GetComponent<Turret>().pollutionProduced_auto_base.ToString("F2") + " + " +
                    buildingSelectedInScene.GetComponent<Turret>().pollutionProduced_auto_extra.ToString("F2") + "/s";
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
                    buildingSelectedInScene.GetComponent<Airport>().pollutionProduced_base.ToString("F2") + " + " +
                    buildingSelectedInScene.GetComponent<Airport>().pollutionProduced_extra.ToString("F2") + "/s";
                
            }
            if (buildingSelectedInScene.tag == "LogisticCenter")
            {
                // Set building name in the UI
                UIManager.i.Line1Text.text = "Collection interval: " + 
                    buildingSelectedInScene.GetComponent<LogisticCenter>().collectionSpeed_base + " - " +
                    buildingSelectedInScene.GetComponent<LogisticCenter>().collectionSpeed_extra + "s";
                UIManager.i.Line2Text.text = "Environmental pollution: " + 
                    buildingSelectedInScene.GetComponent<LogisticCenter>().pollution_auto_base + " + " +
                    buildingSelectedInScene.GetComponent<LogisticCenter>().pollution_auto_extra + "/s";
                UIManager.i.Line3Text.text = "";
                UIManager.i.Line4Text.text = "";
            }
            if(buildingSelectedInScene.tag == "PerpetualMachine")
            {
                UIManager.i.Line1Text.text = "Nearby factory production +" + buildingSelectedInScene.GetComponent<PerpetualMachine>().baseEfficiency * 100 + "%";
                UIManager.i.Line2Text.text = "";
                UIManager.i.Line3Text.text = "";
                UIManager.i.Line4Text.text = "";
            }
        }
        // Make resources avaliable to collect even in building pruchasing mode.
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            // prevent clicking through UI
            if (MouseOverUILayerObject.IsPointerOverUIObject())
            {
                return;
            }
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
                        return;
                        //hit0.collider.gameObject.GetComponent<MainBuilding>().MainBuildingClickEvent();
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
                                AudioManager.instance.Play(SoundList.SelectLogisticCenter);
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

            UIManager.i.tutorialButton.interactable = false;
            UIManager.i.resetCameraButton.interactable = false;
            UIManager.i.pauseButton.interactable = false;

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

            UIManager.i.tutorialButton.interactable = true;
            UIManager.i.resetCameraButton.interactable = true;
            UIManager.i.pauseButton.interactable = true;
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
        buildingSelectedInScene = null;
        buildingObjectType = -1;
        buildingSelectedInUI = null;
        DisableGameUI();
        isPaused = true;
    }
    public void Resume()
    {
        EnableGameUI();
        isPaused = false;
    }
    public void ScaleDownAllBtns()
    {
        mainMenuBtn1.GetComponent<ScaleTween>().ScaleDown();
        mainMenuBtn2.GetComponent<ScaleTween>().ScaleDown();
        mainMenuBtn3.GetComponent<ScaleTween>().ScaleDown();
        mainMenuBtn4.GetComponent<ScaleTween>().ScaleDown();
        musicText.GetComponent<ScaleTween>().ScaleDown();
        musicSlider.GetComponent<ScaleTween>().ScaleDown();
        sfxText.GetComponent<ScaleTween>().ScaleDown();
        sfxSlider.GetComponent<ScaleTween>().ScaleDown();
    }
    public void SaveAndQuit()
    {
        canInput = false;
        LeanTween.move(cameraRotator, new Vector3(8.36f, 36, -10.96f), 1.5f).setEase(LeanTweenType.easeOutQuad).setIgnoreTimeScale(true);
        LeanTween.rotate(cameraRotator, new Vector3(0, -15f, 0), 1.5f).setEase(LeanTweenType.easeOutQuad).setIgnoreTimeScale(true);
        LeanTween.rotate(cameraRotator.transform.GetChild(0).gameObject, new Vector3(25, 0, 0), 1.5f).setEase(LeanTweenType.easeOutQuad).
            setIgnoreTimeScale(true).setOnComplete(LoadMainMenu);
        mainMenuBG.GetComponent<CanvasGroup>().alpha = 0;
    }
    void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }   
    public void ResetCamera()
    {
        CameraControllerMouse.i.ResetCameraDefaultSettings();
    }
    void DisableGameUI()
    {
        UIManager.i.TopUI.SetActive(false);
        UIManager.i.BottomUI.SetActive(false);
        UIManager.i.RightUI.SetActive(false);
        UIManager.i.MainMenuUI.SetActive(true);
        UIManager.i.popUpStorage.SetActive(false);
        buildingDetailsCanvas.SetActive(false);
        buildingInfoCanvas.SetActive(false);
    }
    void EnableGameUI()
    {
        UIManager.i.TopUI.SetActive(true);
        UIManager.i.BottomUI.SetActive(true);
        UIManager.i.RightUI.SetActive(true);
        UIManager.i.MainMenuUI.SetActive(false);
        UIManager.i.popUpStorage.SetActive(true);
    }
}
