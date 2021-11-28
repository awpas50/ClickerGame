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
    public float[] saveData_factoryTimer;
    [Header("Park")]
    public float[,] saveData_parkPos;
    public int[] saveData_parkModelIndex; // 0 or 1
    public int[] saveData_parkLevel;
    public float[] saveData_parkTimer;
    [Header("Turret")]
    public float[,] saveData_turretPos;
    public int[] saveData_turretLevel;
    public float[] saveData_turretFireCountdown;
    [Header("Bullet")]
    public float[,] saveData_bulletPos;
    public int[] saveData_bulletTargetID;
    public float[,] saveData_bulletDir;
    [Header("Airport")]
    public float[,] saveData_airportPos;
    public int[] saveData_airportLevel;
    public float[,] saveData_airplanePos;
    public int[] saveData_airplaneDesIndex;
    public float[,] saveData_airplaneTravelTime;
    public bool[,] saveData_airplaneBoolState;
    public string[] saveData_airplaneEnumState;
    [Header("Logistic Center")]
    public float[,] saveData_logisticPos;
    public int[] saveData_logisticLevel;
    public int[] saveData_findPosIndex; // 1 ~ 25
    [Header("Perpetual machine")]
    public float[,] saveData_perpetualPos;
    public int[] saveData_perpetualLevel;
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
    public int saveData_asteroidSpawnID;
    public float saveData_timeElapsed;
    public float[,] saveData_asteroidProps; // 0 ~ 2: position, 3 ~ 5: target position, 6: speed
    public int[] saveData_asteroidUniqueID;
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
    public int allBullets_int;
    public int allAirports_int;
    public int allLogistics_int;
    public int allPerpetuals_int;
    public int allPlatforms_int;
    public int allAsteroids_int;
    public int allRuins_int;

    public AllSaveData()
    {
        StorePlatformData();
        StoreHouseData();
        StoreFactoryData();
        StoreParkData();
        StoreTurretData();
        StoreAirportData();
        StoreLogisticCenterData();
        StorePerpetualMachineData();
        StoreRuinData();
        StoreResourcesAndPollution();
        StoreTownHallRelated();
        StoreAsteroidsData();
        StoreBulletData();
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
        saveData_factoryTimer = new float[allFactories.Length];
        for (int i = 0; i < allFactories.Length; i++)
        {
            saveData_factoryPos[i, 0] = allFactories[i].transform.position.x;
            saveData_factoryPos[i, 1] = allFactories[i].transform.position.y;
            saveData_factoryPos[i, 2] = allFactories[i].transform.position.z;
            saveData_factoryModelIndex[i] = allFactories[i].GetComponent<BuildingRandomModel>().seed;
            saveData_factoryLevel[i] = allFactories[i].GetComponent<BuildingLevel>().level;
            saveData_factoryTimer[i] = allFactories[i].GetComponent<Factory>().intervalPassed;
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
        saveData_parkTimer = new float[allParks.Length];
        for (int i = 0; i < allParks.Length; i++)
        {
            saveData_parkPos[i, 0] = allParks[i].transform.position.x;
            saveData_parkPos[i, 1] = allParks[i].transform.position.y;
            saveData_parkPos[i, 2] = allParks[i].transform.position.z;
            saveData_parkModelIndex[i] = allParks[i].GetComponent<BuildingRandomModel>().seed;
            saveData_parkLevel[i] = allParks[i].GetComponent<BuildingLevel>().level;
            saveData_parkTimer[i] = allParks[i].GetComponent<Park>().intervalPassed;
        }
    }
    private void StoreTurretData()
    {
        // Turret data
        GameObject[] allTurrets = GameObject.FindGameObjectsWithTag("Generator");
        
        allTurrets_int = allTurrets.Length;
        saveData_turretPos = new float[allTurrets.Length, 3];
        saveData_turretLevel = new int[allTurrets.Length];
        saveData_turretFireCountdown = new float[allTurrets.Length];
        
        for (int i = 0; i < allTurrets.Length; i++)
        {
            saveData_turretPos[i, 0] = allTurrets[i].transform.position.x;
            saveData_turretPos[i, 1] = allTurrets[i].transform.position.y;
            saveData_turretPos[i, 2] = allTurrets[i].transform.position.z;
            saveData_turretLevel[i] = allTurrets[i].GetComponent<BuildingLevel>().level;
            saveData_turretFireCountdown[i] = allTurrets[i].GetComponent<Turret>().fireCountdown;
            
        }
    }
    private void StoreAirportData()
    {
        // Airport data
        GameObject[] allAirports = GameObject.FindGameObjectsWithTag("Airport");
        allAirports_int = allAirports.Length;
        saveData_airportPos = new float[allAirports.Length, 3];
        saveData_airportLevel = new int[allAirports.Length];
        saveData_airplanePos = new float[allAirports.Length, 3];
        saveData_airplaneDesIndex = new int[allAirports.Length];
        saveData_airplaneTravelTime = new float[allAirports.Length, 3];
        saveData_airplaneBoolState = new bool[allAirports.Length, 4];
        saveData_airplaneEnumState = new string[allAirports.Length];

        for (int i = 0; i < allAirports.Length; i++)
        {
            saveData_airportPos[i, 0] = allAirports[i].transform.position.x;
            saveData_airportPos[i, 1] = allAirports[i].transform.position.y;
            saveData_airportPos[i, 2] = allAirports[i].transform.position.z;
            saveData_airportLevel[i] = allAirports[i].GetComponent<BuildingLevel>().level;
            saveData_airplanePos[i, 0] = allAirports[i].GetComponent<Airport>().airplaneREF.transform.position.x;
            saveData_airplanePos[i, 1] = allAirports[i].GetComponent<Airport>().airplaneREF.transform.position.y;
            saveData_airplanePos[i, 2] = allAirports[i].GetComponent<Airport>().airplaneREF.transform.position.z;
            saveData_airplaneDesIndex[i] = allAirports[i].GetComponent<Airport>().airplaneScript.randomDestinationIndex;
            saveData_airplaneTravelTime[i, 0] = allAirports[i].GetComponent<Airport>().airplaneScript.t1;
            saveData_airplaneTravelTime[i, 1] = allAirports[i].GetComponent<Airport>().airplaneScript.t2;
            saveData_airplaneTravelTime[i, 2] = allAirports[i].GetComponent<Airport>().airplaneScript.t3;
            saveData_airplaneBoolState[i, 0] = allAirports[i].GetComponent<Airport>().airplaneScript.reachedHighPoint;
            saveData_airplaneBoolState[i, 1] = allAirports[i].GetComponent<Airport>().airplaneScript.isPopUpSpawned;
            saveData_airplaneBoolState[i, 2] = allAirports[i].GetComponent<Airport>().airplaneScript.redeparture;
            saveData_airplaneBoolState[i, 3] = allAirports[i].GetComponent<Airport>().airplaneScript.isFinishedFirstTimeWaiting;
            saveData_airplaneEnumState[i] = allAirports[i].GetComponent<Airport>().airplaneScript.state.ToString();
        }
    }
    private void StoreLogisticCenterData()
    {
        // Logistic Center data
        GameObject[] allLogistics = GameObject.FindGameObjectsWithTag("LogisticCenter");
        allLogistics_int = allLogistics.Length;
        saveData_logisticPos = new float[allLogistics.Length, 3];
        saveData_logisticLevel = new int[allLogistics.Length];
        saveData_findPosIndex = new int[allLogistics.Length];

        for (int i = 0; i < allLogistics.Length; i++)
        {
            saveData_logisticPos[i, 0] = allLogistics[i].transform.position.x;
            saveData_logisticPos[i, 1] = allLogistics[i].transform.position.y;
            saveData_logisticPos[i, 2] = allLogistics[i].transform.position.z;
            saveData_logisticLevel[i] = allLogistics[i].GetComponent<BuildingLevel>().level;
            saveData_findPosIndex[i] = allLogistics[i].GetComponent<LogisticCenter>().collectPositionIndex;
        }
    }
    private void StorePerpetualMachineData()
    {
        // Perpetual machine data
        GameObject[] allPerpetuals = GameObject.FindGameObjectsWithTag("PerpetualMachine");
        allPerpetuals_int = allPerpetuals.Length;

        saveData_perpetualPos = new float[allPerpetuals.Length, 3];
        saveData_perpetualLevel = new int[allPerpetuals.Length];

        for (int i = 0; i < allPerpetuals.Length; i++)
        {
            saveData_perpetualPos[i, 0] = allPerpetuals[i].transform.position.x;
            saveData_perpetualPos[i, 1] = allPerpetuals[i].transform.position.y;
            saveData_perpetualPos[i, 2] = allPerpetuals[i].transform.position.z;
            saveData_perpetualLevel[i] = allPerpetuals[i].GetComponent<BuildingLevel>().level;
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
                saveData_townHall_objectiveTriggers = new bool[5] { false, false, false, false, false };
                break;
            case 2:
                saveData_townHall_objectiveTriggers = new bool[5] { true, false, false, false, false };
                break;
            case 3:
                saveData_townHall_objectiveTriggers = new bool[5] { true, true, false, false, false };
                break;
            case 4:
                saveData_townHall_objectiveTriggers = new bool[5] { true, true, true, false, false };
                break;
            case 5:
                saveData_townHall_objectiveTriggers = new bool[5] { true, true, true, true, false };
                break;
            case 6:
                saveData_townHall_objectiveTriggers = new bool[5] { true, true, true, true, true };
                break;
        }
    }
    private void StoreAsteroidsData()
    {
        GameObject[] allAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        allAsteroids_int = allAsteroids.Length;
        saveData_asteroidProps = new float[allAsteroids.Length, 7];
        saveData_asteroidUniqueID = new int[allAsteroids.Length];
        for (int i = 0; i < allAsteroids.Length; i++)
        {
            saveData_asteroidProps[i, 0] = allAsteroids[i].transform.position.x;
            saveData_asteroidProps[i, 1] = allAsteroids[i].transform.position.y;
            saveData_asteroidProps[i, 2] = allAsteroids[i].transform.position.z;
            saveData_asteroidProps[i, 3] = allAsteroids[i].GetComponent<Asteroid>().target_vector3.x;
            saveData_asteroidProps[i, 4] = allAsteroids[i].GetComponent<Asteroid>().target_vector3.y;
            saveData_asteroidProps[i, 5] = allAsteroids[i].GetComponent<Asteroid>().target_vector3.z;
            saveData_asteroidProps[i, 6] = allAsteroids[i].GetComponent<Asteroid>().speed;
            saveData_asteroidUniqueID[i] = allAsteroids[i].GetComponent<Asteroid>().uniqueID;
        }
        saveData_asteroidSpawnID = AsteroidSpawner.i.asteroidID;
    }
    private void StoreBulletData()
    {
        GameObject[] allBullets = GameObject.FindGameObjectsWithTag("Bullet");

        allBullets_int = allBullets.Length;
        saveData_bulletPos = new float[allBullets.Length, 3];
        saveData_bulletTargetID = new int[allBullets.Length];
        saveData_bulletDir = new float[allBullets.Length, 3];
        for (int i = 0; i < allBullets.Length; i++)
        {
            saveData_bulletPos[i, 0] = allBullets[i].transform.position.x;
            saveData_bulletPos[i, 1] = allBullets[i].transform.position.y;
            saveData_bulletPos[i, 2] = allBullets[i].transform.position.z;
            if(allBullets[i].GetComponent<Bullet>().target)
            {
                saveData_bulletTargetID[i] = allBullets[i].GetComponent<Bullet>().target.gameObject.GetComponent<Asteroid>().uniqueID;
            }
            else
            {
                saveData_bulletTargetID[i] = -1;
            }
            saveData_bulletDir[i, 0] = allBullets[i].GetComponent<Bullet>().dir.x;
            saveData_bulletDir[i, 1] = allBullets[i].GetComponent<Bullet>().dir.y;
            saveData_bulletDir[i, 2] = allBullets[i].GetComponent<Bullet>().dir.z;
        }
    }
}
