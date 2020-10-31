using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Colliders to detect neraby node")]
    public BoxCollider boxCollider;
    [Header("Neraby node list")]
    public List<GameObject> nearbyNode;
    [Header("Check neraby node with building (Don not edit)")]
    public List<GameObject> nearbyNode_building;
    public GameObject[] nearbyNode_building_house;
    [Header("Node REF (Do not edit)")]
    public int nodeIndex = -1;
    public GameObject placeHolder;
    public GameObject placeHolder_building_REF;
    public GameObject building_REF;
    public GameObject building_ruin;
    public Vector3 offset;

    private void Start()
    {
        nearbyNode_building_house = GameObject.FindGameObjectsWithTag("House");
    }
    private void Update()
    {
        if(building_REF)
        {
            CheckNearbyBuildingType();
        }
    }
    private void OnMouseDown()
    {
        // prevent clicking through UI
        if (MouseOverUILayerObject.IsPointerOverUIObject())
        {
            return;
        }
        // DEBUG
        if (GameManager.i.buildingSelectedInUI == null)
        {
            return;
        }
        if (building_REF == null && GameManager.i.buildingSelectedInUI.name == "Building4_Generator" && GameManager.i.isTurretInPlanning)
        {
            Debug.LogWarning("Generator is already in planning list");
            return;
        }
        // EstimateCost: Precalculate the total cost needed
        if (building_REF == null && GameManager.i.buildingSelectedInUI && Currency.MONEY >= GameManager.i.buildingCost && !building_ruin)
        {
            AddPlaceHolder();
            EstimateCost();
        }
    }

    public void AddPlaceHolder()
    {
        // Audio
        AudioManager.instance.Play(SoundList.BuildingPlaced);
        // Add the node into the list **(GameManager - nodeList)
        GameManager.i.nodeList.Add(gameObject);
        // **(GameManager - futureBuildingList)
        GameManager.i.futureBuildingList.Add(GameManager.i.buildingSelectedInUI);
        // Using a index to recongnize the order (from 0)
        nodeIndex = GameManager.i.nodeList.IndexOf(gameObject);
        // indicator only, make the node recongnize the placeHolder
        placeHolder = Instantiate(GameManager.i.placeHolder, transform.position, Quaternion.identity);
        // Oppositely, the placeHolder needs to recongnize the node.
        PlaceHolder placeHolder_script = placeHolder.GetComponent<PlaceHolder>();
        placeHolder_script.attachedNode = gameObject;
        // Store what building will be placed in the particular node
        placeHolder_building_REF = GameManager.i.buildingSelectedInUI;
    }
    public void EstimateCost()
    {
        // Add the esitmated cost into a list **(GameManager - estimatedCostList)
        GameManager.i.estimatedCostList.Add(GameManager.i.buildingCost);
        // Deduct the money needed
        Currency.MONEY -= GameManager.i.buildingCost;
    }
    // Used in game manager
    public void RemovePlaceHolder()
    {
        Destroy(placeHolder);
        placeHolder = null;
    }
    public void ConfirmPlaceBuilding()
    {
        // This is the actual process of generate a building
        GameObject buildingPrefab = Instantiate(placeHolder_building_REF, transform.position, Quaternion.identity);
        // Add reference (node recongize the building)
        building_REF = buildingPrefab;
        // Add reference (building recongize the node)
        building_REF.GetComponent<BuildingState>().node = gameObject;
        GameManager.i.estimatedCostList.Clear();
    }
    public void RemoveBuildingPlaceHolder()
    {
        // Remove the placeholder
        placeHolder_building_REF = null;
    }
    public void CheckNearbyBuildingType()
    {
        // Check nearby building type every frame (using a specified collider on the node)
        building_REF.GetComponent<BuildingBuff>().allBuildingList.Clear();
        building_REF.GetComponent<BuildingBuff>().nearbyHouse = 0;
        building_REF.GetComponent<BuildingBuff>().nearbyFactory = 0;
        building_REF.GetComponent<BuildingBuff>().nearbyPark = 0;
        building_REF.GetComponent<BuildingBuff>().nearbyTurret = 0;
        building_REF.GetComponent<BuildingBuff>().nearbyMainBuilding = 0;
        building_REF.GetComponent<BuildingBuff>().houseEfficiencyList.Clear();
        building_REF.GetComponent<BuildingBuff>().houseEfficiencyTotal = 0;

        foreach (GameObject b in nearbyNode_building)
        {
            if(b == null)
            {
                building_REF.GetComponent<BuildingBuff>().nearbyHouse += 0;
            }
            else if (b.gameObject.tag == "House")
            {
                building_REF.GetComponent<BuildingBuff>().allBuildingList.Add(b.gameObject);
                building_REF.GetComponent<BuildingBuff>().nearbyHouse += 1;
                building_REF.GetComponent<BuildingBuff>().houseEfficiencyList.Add(b.GetComponent<House>().efficiency);
            }
            else if (b.gameObject.tag == "Factory")
            {
                building_REF.GetComponent<BuildingBuff>().allBuildingList.Add(b.gameObject);
                building_REF.GetComponent<BuildingBuff>().nearbyFactory += 1;
            }
            else if (b.gameObject.tag == "Park")
            {
                building_REF.GetComponent<BuildingBuff>().allBuildingList.Add(b.gameObject);
                building_REF.GetComponent<BuildingBuff>().nearbyPark += 1;
            }
            else if (b.gameObject.tag == "Generator")
            {
                building_REF.GetComponent<BuildingBuff>().allBuildingList.Add(b.gameObject);
                building_REF.GetComponent<BuildingBuff>().nearbyTurret += 1;
            }
            else if (b.gameObject.tag == "MainBuilding")
            {
                building_REF.GetComponent<BuildingBuff>().allBuildingList.Add(b.gameObject);
                building_REF.GetComponent<BuildingBuff>().nearbyMainBuilding += 1;
            }
        }
        for (int i = 0; i < building_REF.GetComponent<BuildingBuff>().houseEfficiencyList.Count; i++)
        {
            building_REF.GetComponent<BuildingBuff>().houseEfficiencyTotal += building_REF.GetComponent<BuildingBuff>().houseEfficiencyList[i];
        }
    }
}
