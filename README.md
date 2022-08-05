# ClickerGame
1-year long C# Unity project.
Technique used: Binary Formatter, LeanTween API, URP Shader Graph.
Learnt: Writing post-release devlogs for users (players).

# Step-to-step guide on how to save data (on PC/Mac/Linux/WebGL) using binary formatter
Today's example will be saving multiple building positions in a city builder game. As building positions are stored as Vector3, but binary formatter cannot store Unity variables such as GameObject, Transform, Vector3, we need to convert it to non Unity specified variables (int, float, etc.).

The scene will be reloaded to wipe any old data before new save data is read and loaded.

4 scripts are needed:

SaveSystem.cs: Static class that handles the binary formatter 

SaveLoadHandler.cs: A script that has to be placed in the Unity hierarchy (using a empty GameObject) in order to work. Has function called SaveGame() and CreateSceneLoader() to be triggered in the game (usually with a button)

AllSaveData.cs: Non-Monobehaviour script which stores the game data you want to save (must be a string, int, float, bool)

SceneLoader.cs: A script used to properly load the game using LoadSceneAsync() (instead of LoadScene()). Otherwise, the save data loading will happened before the scene reloads, and thus loading will fail.

![Screenshot 2022-08-05 17 03 24](https://user-images.githubusercontent.com/41810433/183021373-e0d75004-574c-40b4-9a72-4eb53f84c810.png)

SaveSystem.cs
```
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

SaveLoadHandler.cs
```
using System;
using UnityEngine;

public class SaveLoadHandler : MonoBehaviour
{
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
    // *** This will be called when the script "SceneLoader" is initialized.
    public void LoadGame()
    {
        AllSaveData data = SaveSystem.Load();
        LoadHouses(data);
        Debug.Log("Load successful");
    }
    private void LoadHouses(AllSaveData data)
    {
        for (int i = 0; i < data.allHouses_int; i++)
        {
            GameObject newHouse = Instantiate(GameManager.i.building1.building, 
                new Vector3(data.saveData_housePos[i,0],
                data.saveData_housePos[i,1],
                data.saveData_housePos[i,2]), Quaternion.identity);
        }
    }
}
```

AllSaveData.cs
```
using UnityEngine;

[System.Serializable]
public class AllSaveData
{
    [Header("House")]
    public float[,] saveData_housePos;
    
    // Will be called in the script "SaveLoadHandler"
    public AllSaveData()
    {
        StoreHouseData();
    }
    
    private void StoreHouseData()
    {
        // House data
        GameObject[] allHouses = GameObject.FindGameObjectsWithTag("House");
        allHouses_int = allHouses.Length;
        saveData_housePos = new float[allHouses.Length, 3];
        for (int i = 0; i < allHouses.Length; i++)
        {
            saveData_housePos[i, 0] = allHouses[i].transform.position.x;
            saveData_housePos[i, 1] = allHouses[i].transform.position.y;
            saveData_housePos[i, 2] = allHouses[i].transform.position.z;
        }
    }
}
```
SceneLoader.cs
```
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
        SaveLoadHandler slh = FindObjectOfType<SaveLoadHandler>();
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called
        yield return new WaitForEndOfFrame();
        // Load save data
        slh.LoadGame();
        // Destroy itself after everything has loaded
        Destroy(gameObject);
    }
}
```

This is everything needed for the save system. To save or load in the game, create 2 buttons and simply assign SaveLoadHandler.SaveGame() or SaveL
oadHandler.CreateSceneLoader() to the OnClick() function respectively.

![Screenshot 2022-08-05 17 11 50](https://user-images.githubusercontent.com/41810433/183022755-d1697b2d-d14c-45d1-9118-8b4bdf36af1b.png)
