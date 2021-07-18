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
    [Header("Ruin")]
    public int[] saveData_ruinType; // 1 ~ 5
    public float[,] saveData_ruinPos;
    public int[] saveData_ruinBuildingLevel;
    public float[] saveData_ruinRepairCost;
    [Header("Miscellenous")]
    public float saveData_resources;
    public float saveData_pollution;
    public int saveData_townHallLevel;
    public bool[] saveData_townHall_objectiveTriggers;
    [Header("Asteroids")]
    public float saveData_timeElapsed;
    public float[,] saveData_asteroidProps; // 0 ~ 2: position, 3 ~ 5: target position, 6: speed
    [Header("Building properties")]
    public float[] saveData_allFactories_timer;
    public float[] saveData_allParks_timer;
    public float[,] saveData_allTurrets_target;
    public float[,] saveData_allTurrets_missilePos;
    public string[] saveData_allAirports_state;
    public float[,] saveData_allAirports_planePos;
    [Header("Ref")]
    public int allHouses_int;
    public int allFactories_int;
    public int allParks_int;
    public int allTurrets_int;
    public int allAirports_int;
    public int allPlatforms_int;
    public int allAsteroids_int;
    public int allRuins_int;

    public AllSaveData()
    {
        StoreHouseData();
        StoreFactoryData();
        StoreParkData();
        StoreTurretData();
        StoreAirportData();
        StorePlatformData();
        StoreRuinData();
        StoreResourcesAndPollution();
        StoreTownHallRelated();
        StoreAsteroids();
    }

    private void StoreHouseData()
    {
        // House data
        GameObject[] allHouses = GameObject.FindGameObjectsWithTag("House");
        allHouses_int = allHouses.Length;
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
        GameObject[] allFactories = GameObject.FindGameObjectsWithTag("Factory");
        allFactories_int = allFactories.Length;
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
        GameObject[] allParks = GameObject.FindGameObjectsWithTag("Park");
        allParks_int = allParks.Length;
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
        GameObject[] allTurrets = GameObject.FindGameObjectsWithTag("Generator");
        allTurrets_int = allTurrets.Length;
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
        GameObject[] allAirports = GameObject.FindGameObjectsWithTag("Airport");
        allAirports_int = allAirports.Length;
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
    private void StorePlatformData()
    {
        saveData_platformsInHand = SpecialBuildingCount.platform1Count;

        GameObject[] allPlatforms = GameObject.FindGameObjectsWithTag("Platform");
        allPlatforms_int = allPlatforms.Length;
        saveData_platformPos = new float[allPlatforms.Length, 3];
        for (int i = 0; i < allPlatforms.Length; i++)
        {
            saveData_platformPos[i, 0] = allPlatforms[i].transform.position.x;
            saveData_platformPos[i, 1] = allPlatforms[i].transform.position.y;
            saveData_platformPos[i, 2] = allPlatforms[i].transform.position.z;
        }
    }
    private void StoreRuinData()
    {
        GameObject[] allRuins = GameObject.FindGameObjectsWithTag("Ruin");

        Debug.Log(allRuins.Length);
        allRuins_int = allRuins.Length;
        saveData_ruinPos = new float[allRuins.Length, 3];
        saveData_ruinBuildingLevel = new int[allRuins.Length];
        saveData_ruinRepairCost = new float[allRuins.Length];
        saveData_ruinType = new int[allRuins.Length];

        for (int i = 0; i < allRuins.Length; i++)
        {
            saveData_ruinPos[i, 0] = allRuins[i].transform.position.x;
            saveData_ruinPos[i, 1] = allRuins[i].transform.position.y;
            saveData_ruinPos[i, 2] = allRuins[i].transform.position.z;

            saveData_ruinBuildingLevel[i] = allRuins[i].GetComponent<Ruin>().buildingLevel;
            saveData_ruinRepairCost[i] = allRuins[i].GetComponent<Ruin>().repairCost;

            switch(allRuins[i].GetComponent<Ruin>().buildingType)
            {
                case 1:
                    saveData_ruinType[i] = 1;
                    break;
                case 2:
                    saveData_ruinType[i] = 2;
                    break;
                case 3:
                    saveData_ruinType[i] = 3;
                    break;
                case 4:
                    saveData_ruinType[i] = 4;
                    break;
                case 5:
                    saveData_ruinType[i] = 5;
                    break;
            }
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
    private void StoreAsteroids()
    {
        GameObject[] allAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        allAsteroids_int = allAsteroids.Length;
        saveData_asteroidProps = new float[allAsteroids.Length, 7];
        for (int i = 0; i < allAsteroids.Length; i++)
        {
            saveData_asteroidProps[i, 0] = allAsteroids[i].transform.position.x;
            saveData_asteroidProps[i, 1] = allAsteroids[i].transform.position.y;
            saveData_asteroidProps[i, 2] = allAsteroids[i].transform.position.z;
            saveData_asteroidProps[i, 3] = allAsteroids[i].GetComponent<Asteroid>().target_vector3.x;
            saveData_asteroidProps[i, 4] = allAsteroids[i].GetComponent<Asteroid>().target_vector3.y;
            saveData_asteroidProps[i, 5] = allAsteroids[i].GetComponent<Asteroid>().target_vector3.z;
            saveData_asteroidProps[i, 6] = allAsteroids[i].GetComponent<Asteroid>().speed;
        }
    }
}
