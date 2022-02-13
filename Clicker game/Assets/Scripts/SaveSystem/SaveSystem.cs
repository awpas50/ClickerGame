using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public static class SaveSystem
{
    [DllImport("__Internal")]
    private static extern void JS_FileSystem_Sync();

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
        path = Application.persistentDataPath + "/motorlandSave1.txt";
#elif UNITY_WEBGL
        path = "/idbfs/motorland0212" + "/save1.dat";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory("/idbfs/motorland0212");
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

    public static AllSaveData Load()
    {
        string path;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        path = Application.persistentDataPath + "/save1.txt";
#elif UNITY_STANDALONE_OSX
        path = Application.persistentDataPath + "/save1.txt";
#elif UNITY_STANDALONE_LINUX
        path = Application.persistentDataPath + "/motorlandSave1.txt";
#elif UNITY_WEBGL
        path = "/idbfs/motorland0212" + "/save1.dat";
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
