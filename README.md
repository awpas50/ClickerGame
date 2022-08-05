# ClickerGame
1-year long C# Unity project.
Technique used: Binary Formatter, LeanTween API, URP Shader Graph.
Learnt: Writing post-release devlogs for users (players).

# Step-tostep guide on how to save data (on PC/Mac/Linux/WebGL) using binary formatter
Today's example will be saving building positions in a city builder game. As binary formatter cannot store Unity variables such as GameObject, Transform, Vector3, we need to convert it to non Unity specified variables (int, float, etc.).

3 scripts are needed:

SaveSystem.cs: Static class that handles the binary formatter 

SaveLoadHandler.cs: A script that has to be placed in the Unity hierarchy (using a empty GameObject) in order to work. Has function called SaveGame() and LoadGame() to be triggered in the game (usually with a button)

AllSaveData.cs: Non-Monobehaviour script which stores the game data you want to save (must be a string, int, float, bool)

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
