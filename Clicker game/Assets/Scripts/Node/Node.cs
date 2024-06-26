﻿using System.Linq;
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
    [Header("Node REF (Do not edit)")]
    public int nodeIndex = -1;
    public GameObject placeHolder;
    public GameObject placeHolder_building_REF;
    public GameObject building_REF;
    public GameObject building_ruin;
    public Vector3 offset;

    //private bool isDestroyed = false;
    private void Start()
    {
        StartCoroutine(CheckNearbyBuildingType());
    }
    // Only destroy one node when two nodes collide each other.
    private void OnTriggerEnter(Collider other)
    {
        if ((this.gameObject.tag == "PlatformPlaces" && other.gameObject.tag == "PlatformPlaces"))
        {
            //Debug.Log(this.gameObject + " " + other.gameObject);
            // Destroy one only.
            if(this.gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                Destroy(gameObject);
            }
            if (this.gameObject.GetInstanceID() >= other.gameObject.GetInstanceID())
            {
                Destroy(other.gameObject);
            }
        }
        if ((this.gameObject.tag == "PlatformPlaces" && other.gameObject.tag == "NodeDestroyer"))
        {
             Destroy(gameObject);
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
        if (gameObject.tag == "Node" && GameManager.i.buildingObjectType != 0)
        {
            AudioManager.instance.Play(SoundList.Error);
            return;
        }
        if ((gameObject.tag == "PlatformPlaces" || gameObject.tag == "PlatformPlaces2") && GameManager.i.buildingObjectType != 1)
        {
            AudioManager.instance.Play(SoundList.Error);
            return;
        }
        //if (building_REF == null && GameManager.i.buildingSelectedInUI.name == "Building4_Generator" && GameManager.i.isTurretInPlanning)
        //{
        //    Debug.LogWarning("Generator is already in planning list");
        //    return;
        //}
        // EstimateCost: Precalculate the total cost needed
        if (building_REF == null && GameManager.i.buildingSelectedInUI && Currency.MONEY < GameManager.i.buildingCost)
        {
            AudioManager.instance.Play(SoundList.Error);
        }
        if (building_REF == null && GameManager.i.buildingSelectedInUI && GameManager.i.buildingObjectType == 0 && Currency.MONEY >= GameManager.i.buildingCost && !building_ruin)
        {
            AddPlaceHolder();
            EstimateCost();
        }
        if (building_REF == null && GameManager.i.buildingSelectedInUI && GameManager.i.buildingObjectType == 1 && Currency.MONEY >= GameManager.i.buildingCost)
        {
            if(SpecialBuildingCount.platform1Count > 0)
            {
                SpecialBuildingCount.platform1Count -= 1;
                AddPlaceHolder();
                EstimateCost();
            }
            else
            {
                AudioManager.instance.Play(SoundList.Error);
                return;
            }
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
        // This is the actual process of putting a building
        GameObject buildingPrefab = Instantiate(placeHolder_building_REF, transform.position + placeHolder_building_REF.GetComponent<BuildingState>().offset, Quaternion.identity);
        Debug.Log(buildingPrefab);
        if(gameObject.tag == "Node")
        {
            // Add reference (node recongize the building)
            if (buildingPrefab.tag != "Platform")
                building_REF = buildingPrefab;
            // Add reference (building recongize the node)
            if (buildingPrefab.tag != "Platform")
                building_REF.GetComponent<BuildingState>().node = gameObject;
            // Add glowing effect
            //StartCoroutine(buildingPrefab.GetComponent<BuildingGlowingMat>().SetGlow());
        }
        
        GameManager.i.estimatedCostList.Clear();
    }
    public void RemoveBuildingPlaceHolder()
    {
        // Remove the placeholder
        placeHolder_building_REF = null;
    }
    IEnumerator CheckNearbyBuildingType()
    {
        while(true)
        {
            if (gameObject.tag == "Node" && building_REF)
            {
                // Check nearby building type every frame (using a specified collider on the node)
                building_REF.GetComponent<BuildingBuff>().allBuildingList.Clear();
                building_REF.GetComponent<BuildingBuff>().nearbyHouse = 0;
                building_REF.GetComponent<BuildingBuff>().nearbyFactory = 0;
                building_REF.GetComponent<BuildingBuff>().nearbyPark = 0;
                building_REF.GetComponent<BuildingBuff>().nearbyTurret = 0;
                building_REF.GetComponent<BuildingBuff>().nearbyMainBuilding = 0;
                building_REF.GetComponent<BuildingBuff>().nearbyAirport = 0;
                building_REF.GetComponent<BuildingBuff>().nearbyLogistic = 0;
                building_REF.GetComponent<BuildingBuff>().nearbyPerpetual = 0;
                building_REF.GetComponent<BuildingBuff>().houseEfficiencyList.Clear();
                building_REF.GetComponent<BuildingBuff>().houseEfficiencyTotal = 0;
                building_REF.GetComponent<BuildingBuff>().perpetualEfficiencyList.Clear();
                building_REF.GetComponent<BuildingBuff>().perpetualEfficiencyTotal = 0;

                foreach (GameObject b in nearbyNode_building)
                {
                    if (b == null)
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
                    else if (b.gameObject.tag == "Airport")
                    {
                        building_REF.GetComponent<BuildingBuff>().allBuildingList.Add(b.gameObject);
                        building_REF.GetComponent<BuildingBuff>().nearbyAirport += 1;
                    }
                    else if (b.gameObject.tag == "LogisticCenter")
                    {
                        building_REF.GetComponent<BuildingBuff>().allBuildingList.Add(b.gameObject);
                        building_REF.GetComponent<BuildingBuff>().nearbyLogistic += 1;
                    }
                    else if (b.gameObject.tag == "PerpetualMachine")
                    {
                        building_REF.GetComponent<BuildingBuff>().allBuildingList.Add(b.gameObject);
                        building_REF.GetComponent<BuildingBuff>().nearbyPerpetual += 1;
                        building_REF.GetComponent<BuildingBuff>().perpetualEfficiencyList.Add(b.GetComponent<PerpetualMachine>().efficiency);
                    }
                }
                if (building_REF.GetComponent<BuildingBuff>().houseEfficiencyList.Count > 0)
                {
                    for (int i = 0; i < building_REF.GetComponent<BuildingBuff>().houseEfficiencyList.Count; i++)
                    {
                        building_REF.GetComponent<BuildingBuff>().houseEfficiencyTotal += building_REF.GetComponent<BuildingBuff>().houseEfficiencyList[i];
                    }
                }
                if (building_REF.GetComponent<BuildingBuff>().perpetualEfficiencyList.Count > 0)
                {
                    for (int i = 0; i < building_REF.GetComponent<BuildingBuff>().perpetualEfficiencyList.Count; i++)
                    {
                        building_REF.GetComponent<BuildingBuff>().perpetualEfficiencyTotal += building_REF.GetComponent<BuildingBuff>().perpetualEfficiencyList[i];
                    }
                }
            }
            yield return new WaitForSeconds(0.125f);
        }
    }
}
