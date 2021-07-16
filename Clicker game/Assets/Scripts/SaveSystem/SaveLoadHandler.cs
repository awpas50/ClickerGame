using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadHandler : MonoBehaviour
{
    public void SaveGame()
    {
        SaveSystem.Save();
    }

    public void LoadGame()
    {
        AllSaveData data = AccessSaveFile();
        LoadHouses(data);
        LoadFactories(data);
        LoadParks(data);
        LoadTurrets(data);
        LoadAirports(data);
        LoadPlatforms(data);
        LoadResourcesAndPollution(data);
        LoadTownHallRelated(data);
        LoadAsteroids(data);
    }

    private AllSaveData AccessSaveFile()
    {
        return SaveSystem.Load();
    }
    private void LoadHouses(AllSaveData data)
    {
        for (int i = 0; i < data.allHouses_int; i++)
        {
            GameObject newHouse = Instantiate(GameManager.i.building1.building, 
                new Vector3(data.saveData_housePos[i,0],
                data.saveData_housePos[i,1],
                data.saveData_housePos[i,2]), Quaternion.identity);
            newHouse.GetComponent<BuildingRandomModel>().seed = data.saveData_houseModelIndex[i];
            newHouse.GetComponent<BuildingLevel>().level = data.saveData_houseLevel[i];
        }
    }
    private void LoadFactories(AllSaveData data)
    {
        for (int i = 0; i < data.allFactories_int; i++)
        {
            GameObject newFactory = Instantiate(GameManager.i.building2.building,
                new Vector3(data.saveData_factoryPos[i, 0],
                data.saveData_factoryPos[i, 1],
                data.saveData_factoryPos[i, 2]), Quaternion.identity);
            newFactory.GetComponent<BuildingRandomModel>().seed = data.saveData_factoryModelIndex[i];
            newFactory.GetComponent<BuildingLevel>().level = data.saveData_factoryLevel[i];
        }
    }
    private void LoadParks(AllSaveData data)
    {
        for (int i = 0; i < data.allParks_int; i++)
        {
            GameObject newPark = Instantiate(GameManager.i.building3.building,
                new Vector3(data.saveData_parkPos[i, 0],
                data.saveData_parkPos[i, 1],
                data.saveData_parkPos[i, 2]), Quaternion.identity);
            newPark.GetComponent<BuildingRandomModel>().seed = data.saveData_parkModelIndex[i];
            newPark.GetComponent<BuildingLevel>().level = data.saveData_parkLevel[i];
        }
    }
    private void LoadTurrets(AllSaveData data)
    {
        for (int i = 0; i < data.allTurrets_int; i++)
        {
            GameObject newTurret = Instantiate(GameManager.i.building4.building,
                new Vector3(data.saveData_turretPos[i, 0],
                data.saveData_turretPos[i, 1],
                data.saveData_turretPos[i, 2]), Quaternion.identity);
            newTurret.GetComponent<BuildingLevel>().level = data.saveData_turretLevel[i];
        }
    }
    private void LoadAirports(AllSaveData data)
    {
        for (int i = 0; i < data.allAirports_int; i++)
        {
            GameObject newAirport = Instantiate(GameManager.i.building5.building,
                new Vector3(data.saveData_airportPos[i, 0],
                data.saveData_airportPos[i, 1],
                data.saveData_airportPos[i, 2]), Quaternion.identity);
            newAirport.GetComponent<BuildingLevel>().level = data.saveData_airportLevel[i];
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
}
