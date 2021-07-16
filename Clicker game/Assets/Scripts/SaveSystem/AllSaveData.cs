using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllSaveData
{
    [Header("House")]
    public float[,] saveData_housePos;
    public int[] saveData_houseModelIndex; // 0 or 1
    public int[] saveData_houseLevel;
    [Header("Factory")]
    public float[,] saveData_factoryPos;
    public int[] saveData_factoryModelIndex; // 0 or 1
    public int[] saveData_factoryLevel;
    [Header("Park")]
    public float[,] saveData_parkPos;
    public int[] saveData_parkModelIndex; // 0 or 1
    public int[] saveData_parkLevel;
    [Header("Turret")]
    public float[,] saveData_turretPos;
    public int[] saveData_turretLevel;
    [Header("Airport")]
    public float[,] saveData_airportPos;
    public int[] saveData_airportLevel;
    [Header("Platforms")]
    public int saveData_platformsInHand;
    public float[,] saveData_platformPos;
    [Header("Miscellenous")]
    public float saveData_resources;
    public float saveData_pollution;
    public int saveData_townHallLevel;
    public bool[] saveData_townHall_objectiveTriggers;
    [Header("Building properties")]
    public float[] saveData_allFactories_timer;
    public float[] saveData_allParks_timer;
    public float[,] saveData_allTurrets_target;
    public float[,] saveData_allTurrets_missilePos;
    public string[] saveData_allAirports_state;
    public float[,] saveData_allAirports_planePos;

    [Header("Asteroids")]
    public float saveData_timeElapsed;
    public float[][] saveData_allAsteroidPos;
    public float[] saveData_allAsteroidSpeed;

    [Header("Ref")]
    public GameObject[] allHouses;
    public GameObject[] allFactories;
    public GameObject[] allParks;
    public GameObject[] allTurrets;
    public GameObject[] allAirports;
    public GameObject[] allPlatforms;

    public AllSaveData()
    {
        StoreHouseData();
        StoreFactoryData();
        StoreParkData();
        StoreTurretData();
        StoreAirportData();
        StoreResourcesAndPollution();
        StoreTownHallRelated();
    }

    private void StoreHouseData()
    {
        // House data
        allHouses = GameObject.FindGameObjectsWithTag("House");
        saveData_housePos = new float[allHouses.Length, 3];
        saveData_houseModelIndex = new int[allHouses.Length];
        saveData_houseLevel = new int[allHouses.Length];
        for (int i = 0; i < allHouses.Length; i++)
        {
            saveData_housePos[i, 0] = allHouses[i].transform.position.x;
            saveData_housePos[i, 1] = allHouses[i].transform.position.y;
            saveData_housePos[i, 2] = allHouses[i].transform.position.z;
            saveData_houseModelIndex[i] = allHouses[i].GetComponent<BuildingRandomModel>().seed;
            saveData_houseLevel[i] = allHouses[i].GetComponent<BuildingLevel>().level;
        }
    }
    private void StoreFactoryData()
    {
        // Factory data
        allFactories = GameObject.FindGameObjectsWithTag("Factory");
        saveData_factoryPos = new float[allFactories.Length, 3];
        saveData_factoryModelIndex = new int[allFactories.Length];
        saveData_factoryLevel = new int[allFactories.Length];
        for (int i = 0; i < allFactories.Length; i++)
        {
            saveData_factoryPos[i, 0] = allFactories[i].transform.position.x;
            saveData_factoryPos[i, 1] = allFactories[i].transform.position.y;
            saveData_factoryPos[i, 2] = allFactories[i].transform.position.z;
            saveData_factoryModelIndex[i] = allFactories[i].GetComponent<BuildingRandomModel>().seed;
            saveData_factoryLevel[i] = allFactories[i].GetComponent<BuildingLevel>().level;
        }
    }
    private void StoreParkData()
    {
        // Park data
        allParks = GameObject.FindGameObjectsWithTag("Park");
        saveData_parkPos = new float[allParks.Length, 3];
        saveData_parkModelIndex = new int[allParks.Length];
        saveData_parkLevel = new int[allParks.Length];
        for (int i = 0; i < allParks.Length; i++)
        {
            saveData_parkPos[i, 0] = allParks[i].transform.position.x;
            saveData_parkPos[i, 1] = allParks[i].transform.position.y;
            saveData_parkPos[i, 2] = allParks[i].transform.position.z;
            saveData_parkModelIndex[i] = allParks[i].GetComponent<BuildingRandomModel>().seed;
            saveData_parkLevel[i] = allParks[i].GetComponent<BuildingLevel>().level;
        }
    }
    private void StoreTurretData()
    {
        // Turret data
        allTurrets = GameObject.FindGameObjectsWithTag("Generator");
        saveData_turretPos = new float[allTurrets.Length, 3];
        saveData_turretLevel = new int[allTurrets.Length];
        for (int i = 0; i < allTurrets.Length; i++)
        {
            saveData_turretPos[i, 0] = allTurrets[i].transform.position.x;
            saveData_turretPos[i, 1] = allTurrets[i].transform.position.y;
            saveData_turretPos[i, 2] = allTurrets[i].transform.position.z;
            saveData_turretLevel[i] = allTurrets[i].GetComponent<BuildingLevel>().level;
        }
    }
    private void StoreAirportData()
    {
        // Airport data
        allAirports = GameObject.FindGameObjectsWithTag("Airport");
        saveData_airportPos = new float[allAirports.Length, 3];
        saveData_airportLevel = new int[allAirports.Length];
        for (int i = 0; i < allAirports.Length; i++)
        {
            saveData_airportPos[i, 0] = allAirports[i].transform.position.x;
            saveData_airportPos[i, 1] = allAirports[i].transform.position.y;
            saveData_airportPos[i, 2] = allAirports[i].transform.position.z;
            saveData_airportLevel[i] = allAirports[i].GetComponent<BuildingLevel>().level;
        }
    }
    private void StoreResourcesAndPollution()
    {
        saveData_resources = Currency.MONEY;
        saveData_pollution = Pollution.POLLUTION;
    }
    private void StoreTownHallRelated()
    {
        saveData_townHallLevel = Objective.townHallLevel;
        switch (Objective.townHallLevel)
        {
            case 1:
                saveData_townHall_objectiveTriggers = new bool[4] { false, false, false, false };
                break;
            case 2:
                saveData_townHall_objectiveTriggers = new bool[4] { true, false, false, false };
                break;
            case 3:
                saveData_townHall_objectiveTriggers = new bool[4] { true, true, false, false };
                break;
            case 4:
                saveData_townHall_objectiveTriggers = new bool[4] { true, true, true, false };
                break;
            case 5:
                saveData_townHall_objectiveTriggers = new bool[4] { true, true, true, true };
                break;
        }
    }
}
