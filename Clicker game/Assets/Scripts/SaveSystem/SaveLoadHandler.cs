using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadHandler : MonoBehaviour
{
    public void SaveGame()
    {
        SaveSystem.Save();
        Debug.Log("Save successful");
    }

    public void CreateSceneLoader()
    {
        GameObject sceneLoader = new GameObject("SceneLoader");
        DontDestroyOnLoad(sceneLoader);
        sceneLoader.AddComponent<SceneLoader>();
    }
    public void LoadGame()
    {
        AllSaveData data = AccessSaveFile();
        GameObject[] allNodes = GetNodeRef();
        LoadHouses(data, allNodes);
        LoadFactories(data, allNodes);
        LoadParks(data, allNodes);
        LoadTurrets(data, allNodes);
        LoadAirports(data, allNodes);
        LoadPlatforms(data);
        LoadRuins(data);
        LoadResourcesAndPollution(data);
        LoadTownHallRelated(data);
        LoadAsteroids(data);

        Debug.Log("Load successful");
    }

    private AllSaveData AccessSaveFile()
    {
        return SaveSystem.Load();
    }
    private void LoadHouses(AllSaveData data, GameObject[] allNodes)
    {
        for (int i = 0; i < data.allHouses_int; i++)
        {
            GameObject newHouse = Instantiate(GameManager.i.building1.building, 
                new Vector3(data.saveData_housePos[i,0],
                data.saveData_housePos[i,1],
                data.saveData_housePos[i,2]), Quaternion.identity);
            newHouse.GetComponent<BuildingRandomModel>().seed = data.saveData_houseModelIndex[i];
            newHouse.GetComponent<BuildingLevel>().level = data.saveData_houseLevel[i];
            // Node reference
            GameObject closestNode = GetClosestNode(newHouse, allNodes);
            SetNodeReference(newHouse, closestNode);
        }
    }
    private void LoadFactories(AllSaveData data, GameObject[] allNodes)
    {
        for (int i = 0; i < data.allFactories_int; i++)
        {
            GameObject newFactory = Instantiate(GameManager.i.building2.building,
                new Vector3(data.saveData_factoryPos[i, 0],
                data.saveData_factoryPos[i, 1],
                data.saveData_factoryPos[i, 2]), Quaternion.identity);
            newFactory.GetComponent<BuildingRandomModel>().seed = data.saveData_factoryModelIndex[i];
            newFactory.GetComponent<BuildingLevel>().level = data.saveData_factoryLevel[i];
            newFactory.GetComponent<Factory>().intervalPassed = data.saveData_factoryTimer[i];
            // Node reference
            GameObject closestNode = GetClosestNode(newFactory, allNodes);
            SetNodeReference(newFactory, closestNode);
        }
    }
    private void LoadParks(AllSaveData data, GameObject[] allNodes)
    {
        for (int i = 0; i < data.allParks_int; i++)
        {
            GameObject newPark = Instantiate(GameManager.i.building3.building,
                new Vector3(data.saveData_parkPos[i, 0],
                data.saveData_parkPos[i, 1],
                data.saveData_parkPos[i, 2]), Quaternion.identity);
            newPark.GetComponent<BuildingRandomModel>().seed = data.saveData_parkModelIndex[i];
            newPark.GetComponent<BuildingLevel>().level = data.saveData_parkLevel[i];
            newPark.GetComponent<Park>().intervalPassed = data.saveData_parkTimer[i];
            // Node reference
            GameObject closestNode = GetClosestNode(newPark, allNodes);
            SetNodeReference(newPark, closestNode);
        }
    }
    private void LoadTurrets(AllSaveData data, GameObject[] allNodes)
    {
        for (int i = 0; i < data.allTurrets_int; i++)
        {
            GameObject newTurret = Instantiate(GameManager.i.building4.building,
                new Vector3(data.saveData_turretPos[i, 0],
                data.saveData_turretPos[i, 1],
                data.saveData_turretPos[i, 2]), Quaternion.identity);
            newTurret.GetComponent<BuildingLevel>().level = data.saveData_turretLevel[i];
            // Node reference
            GameObject closestNode = GetClosestNode(newTurret, allNodes);
            SetNodeReference(newTurret, closestNode);
        }
    }
    private void LoadAirports(AllSaveData data, GameObject[] allNodes)
    {
        for (int i = 0; i < data.allAirports_int; i++)
        {
            GameObject newAirport = Instantiate(GameManager.i.building5.building,
                new Vector3(data.saveData_airportPos[i, 0],
                data.saveData_airportPos[i, 1],
                data.saveData_airportPos[i, 2]), Quaternion.identity);
            newAirport.GetComponent<BuildingLevel>().level = data.saveData_airportLevel[i];
            // Node reference
            GameObject closestNode = GetClosestNode(newAirport, allNodes);
            SetNodeReference(newAirport, closestNode);
        }
    }
    private void LoadPlatforms(AllSaveData data)
    {
        SpecialBuildingCount.platform1Count = data.saveData_platformsInHand;
        for (int i = 0; i < data.allPlatforms_int; i++)
        {
            GameObject newPlatform = Instantiate(GameManager.i.platform1.building,
                new Vector3(data.saveData_platformPos[i, 0],
                data.saveData_platformPos[i, 1],
                data.saveData_platformPos[i, 2]), Quaternion.identity);
        }
    }
    private void LoadRuins(AllSaveData data)
    {
        for (int i = 0; i < data.allRuins_int; i++)
        {
            GameObject ruinToSpawn = GameManager.i.ruin1;

            if(data.saveData_ruinType[i] == 1)
            {
                ruinToSpawn = GameManager.i.ruin1;
            }
            else if (data.saveData_ruinType[i] == 2)
            {
                ruinToSpawn = GameManager.i.ruin2;
            }
            else if(data.saveData_ruinType[i] == 3)
            {
                ruinToSpawn = GameManager.i.ruin3;
            }
            else if(data.saveData_ruinType[i] == 4)
            {
                ruinToSpawn = GameManager.i.ruin4;
            }
            else if(data.saveData_ruinType[i] == 5)
            {
                ruinToSpawn = GameManager.i.ruin5;
            }
            //newRuin.transform.position = new Vector3(data.saveData_ruinPos[i, 0], data.saveData_ruinPos[i, 1], data.saveData_ruinPos[i, 2]);
            //newRuin.transform.rotation = Quaternion.identity;
            GameObject newRuin = Instantiate(ruinToSpawn,
                new Vector3(data.saveData_ruinPos[i, 0],
                data.saveData_ruinPos[i, 1],
                data.saveData_ruinPos[i, 2]), Quaternion.identity);
            newRuin.GetComponent<Ruin>().buildingLevel = data.saveData_ruinBuildingLevel[i];
            newRuin.GetComponent<Ruin>().buildingType = data.saveData_ruinType[i];
            newRuin.GetComponent<Ruin>().repairCost = data.saveData_ruinRepairCost[i];
        }
    }
    private void LoadResourcesAndPollution(AllSaveData data)
    {
        Currency.MONEY = data.saveData_resources;
        Pollution.POLLUTION = data.saveData_pollution;
    }
    private void LoadTownHallRelated(AllSaveData data)
    {
        Objective.townHallLevel = data.saveData_townHallLevel;
        Objective objective = FindObjectOfType<Objective>();
        objective.additionalTrigger1 = data.saveData_townHall_objectiveTriggers[0];
        objective.additionalTrigger2 = data.saveData_townHall_objectiveTriggers[1];
        objective.additionalTrigger3 = data.saveData_townHall_objectiveTriggers[2];
        objective.additionalTrigger4 = data.saveData_townHall_objectiveTriggers[3];
    }
    private void LoadAsteroids(AllSaveData data)
    {
        for (int i = 0; i < data.allAsteroids_int; i++)
        {
            GameObject newAsteroid = Instantiate(AsteroidSpawner.i.asteroid, new Vector3(data.saveData_asteroidProps[i, 0], 
                data.saveData_asteroidProps[i, 1], 
                data.saveData_asteroidProps[i, 2]), Quaternion.identity);
            newAsteroid.GetComponent<Asteroid>().target_vector3 = new Vector3(data.saveData_asteroidProps[i, 3], 
                data.saveData_asteroidProps[i, 4], 
                data.saveData_asteroidProps[i, 5]);
            newAsteroid.GetComponent<Asteroid>().speed = data.saveData_asteroidProps[i, 6];
        }
    }
    GameObject[] GetNodeRef()
    {
        return GameObject.FindGameObjectsWithTag("Node");
    }
    GameObject GetClosestNode(GameObject building, GameObject[] nodes)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = building.transform.position;
        foreach (GameObject n in nodes)
        {
            float dist = Vector3.Distance(n.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = n;
                minDist = dist;
            }
        }
        return tMin;
    }
    // A special function used to reassign node (gameObject) reference of any building
    void SetNodeReference(GameObject building, GameObject closestNode)
    {
        // assign node --> building
        building.GetComponent<BuildingState>().node = closestNode;
        // assign building --> node
        closestNode.GetComponent<Node>().building_REF = building;
    }
}
