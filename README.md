# Summary

## Creating a save system (works on PC/Mac/Linux/WebGL) using binary formatter
The example showcased how to save GameObject's position. Building positions are stored as `Vector3`, but binary formatter cannot store types only avaliable in Unity such as `GameObject`, `Transform` or `Vector3`, we need to convert it to basic C# type (`int`, `float`, etc.).

The scene will be reloaded to wipe any old data before new save data is read and loaded.

4 scripts are needed:

`SaveSystem.cs`: Static class that handles the binary formatter 

`SaveLoadHandler.cs`: A script that has to be placed in the Unity hierarchy (you might just create an empty GameObject and put the SaveLoadHandler.cs script in) in order to work. Has functions `SaveGame()` and `CreateSceneLoader()` to trigger saving and loading

`AllSaveData.cs`: Stores the game data you want to save (must be a string, int, float, bool)

`SceneLoader.cs`: A script used to properly load the game using `LoadSceneAsync()` (instead of `LoadScene()`). Otherwise, the save data loading will happened before the scene reloads, and thus loading will fail.

![Screenshot 2022-08-05 17 03 24](https://user-images.githubusercontent.com/41810433/183021373-e0d75004-574c-40b4-9a72-4eb53f84c810.png)

###### SaveSystem.cs
``` C#
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Runtime.InteropServices;

public static class SaveSystem
{
    [DllImport("__Internal")]
    private static extern void JS_FileSystem_Sync();
    
    // Will be called in the script "SaveLoadHandler"
    public static void Save()
    {
        // create a binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        // declare a path 
        string path;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        path = Application.persistentDataPath + "/save1.txt";
#elif UNITY_STANDALONE_OSX
        path = Application.persistentDataPath + "/save1.txt";
#elif UNITY_STANDALONE_LINUX
        path = Application.persistentDataPath + "/save1.txt";
        // Important: This path must have a custom name or the data will be wiped every time a new build was uploaded
#elif UNITY_WEBGL
        path = "/idbfs/anyNameYouWant" + "/save1.dat";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory("/idbfs/anyNameYouWant");
        }
#endif
        using (FileStream stream = new FileStream(path, FileMode.Create)) {
            AllSaveData allSaveData = new AllSaveData();
            // encrypt the data into binary format
            formatter.Serialize(stream, allSaveData);
            // remember to close the stream
            stream.Close();
            // Sync
            JS_FileSystem_Sync();
        }
    }
    
    // Will be called in the script "SaveLoadHandler"
    public static AllSaveData Load()
    {
        string path;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        path = Application.persistentDataPath + "/save1.txt";
#elif UNITY_STANDALONE_OSX
        path = Application.persistentDataPath + "/save1.txt";
#elif UNITY_STANDALONE_LINUX
        path = Application.persistentDataPath + "/save1.txt";
#elif UNITY_WEBGL
        // Important: This path must have a custom name or the data will be wiped every time a new build was uploaded
        path = "/idbfs/anyNameYouWant" + "/save1.dat";
#endif
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            // decrypt the data from binary to readable format
            AllSaveData data = formatter.Deserialize(stream) as AllSaveData;
            // remember to close the stream
            stream.Close();
            return data;
        }
        else
        {
            //DEBUG
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
```

###### SaveLoadHandler.cs 
``` C#
using System;
using UnityEngine;

public class SaveLoadHandler : MonoBehaviour
{
    // THe building you want to be loaded
    public GameObject buildingA;
    // Will be called in runtime (usually with a button)
    public void SaveGame()
    {
        SaveSystem.Save();
        Debug.Log("Save successful");
    }
    // Will be called in runtime (usually with a button)
    public void CreateSceneLoader()
    {
        GameObject sceneLoader = new GameObject("SceneLoader");
        DontDestroyOnLoad(sceneLoader);
        sceneLoader.AddComponent<SceneLoader>();
    }
    // *** Will be called when SceneLoader.cs is initialized.
    public void LoadGame()
    {
        AllSaveData data = SaveSystem.Load();
        LoadHouses(data);
        Debug.Log("Load successful");
    }
    private void LoadHouses(AllSaveData data)
    {
        for (int i = 0; i < data.saveData_allHousesCount; i++)
        {
            // Instantiate a new house with the position previously saved.
            GameObject newHouse = Instantiate(buildingA, 
                new Vector3(data.saveData_housePos[i,0],
                data.saveData_housePos[i,1],
                data.saveData_housePos[i,2]), Quaternion.identity);
        }
    }
}
```

###### AllSaveData.cs
``` C#
using UnityEngine;

[System.Serializable]
public class AllSaveData
{
    [Header("House")]
    public float[,] saveData_housePos;
    public int saveData_allHousesCount;
    // Will be called in SaveLoadHandler.cs
    public AllSaveData()
    {
        StoreHouseData();
    } 
    private void StoreHouseData()
    {
        // Obtain all the houses with the tag 'House' in the scene
        GameObject[] allHouses = GameObject.FindGameObjectsWithTag("House");
        // The number of houses has to be stored. This will be used when loading.
        saveData_allHousesCount = allHouses.Length;
        // Initializing the array
        saveData_housePos = new float[allHouses.Length, 3];
        for (int i = 0; i < allHouses.Length; i++)
        {
            // Storing a Vector3 variable into 3 float variables.
            saveData_housePos[i, 0] = allHouses[i].transform.position.x;
            saveData_housePos[i, 1] = allHouses[i].transform.position.y;
            saveData_housePos[i, 2] = allHouses[i].transform.position.z;
        }
    }
}
```
###### SceneLoader.cs
``` C#
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadScene());
    }
    public IEnumerator LoadScene()
    {
        // Access SaveLoadHandler.cs
        SaveLoadHandler slh = FindObjectOfType<SaveLoadHandler>();
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake() and Start() method is called
        yield return new WaitForEndOfFrame();
        // Load save data in the next frame
        slh.LoadGame();
        // Destroy itself after everything has loaded, so that this function will only be called once.
        Destroy(gameObject);
    }
}
```

This is everything needed for the save system. To test saving or loading in the game, create 2 buttons and simply assign `SaveLoadHandler.SaveGame()` and `SaveLoadHandler.CreateSceneLoader()` to the OnClick() function respectively.

![Screenshot 2022-08-05 17 11 50](https://user-images.githubusercontent.com/41810433/183022755-d1697b2d-d14c-45d1-9118-8b4bdf36af1b.png)

## Tips
If you want to save multiple types of buildings, simply put all the data you need to save in `SaveData.SaveData()` and everything you need to load in the script `SaveLoadHandler.LoadGame()`. Below is a example storing different entities in the game:
###### SaveData.cs
``` C#
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
    StoreCurrentGameTime();
    StoreTownHallRelated();
    StoreAsteroidsData();
    StoreBulletData();
}
```

###### SaveLoadHandler.cs
``` C#
public void LoadGame()
{
    AllSaveData data = SaveSystem.Load();
    LoadPlatforms(data);
    LoadHouses(data);
    LoadFactories(data);
    LoadParks(data);
    LoadTurrets(data);
    LoadAirports(data);
    LoadLogisticCenters(data);
    LoadPerpetualMachines(data);
    LoadRuins(data);
    LoadResourcesAndPollution(data);
    LoadCurrentGameTime(data);
    LoadTownHallRelated(data);
    LoadAsteroids(data);
    LoadBullets(data);
}
```
