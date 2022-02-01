using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;


public static class SaveSystem
{
    public static void Save()
    {
        // create a binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        // declare a path 
        string path;
#if !UNITY_WEBGL
        var pathWithEnv = @"%userprofile%\AppData\Local\Motorland0123\Save\save1.txt";
        path = Environment.ExpandEnvironmentVariables(pathWithEnv);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
#elif UNITY_WEBGL
        path = "/idbfs/Motorland0123" + "/save1.txt";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
#endif

        FileStream stream = new FileStream(path, FileMode.Create);
        AllSaveData allSaveData = new AllSaveData();
        // encrypt the data into binary format
        formatter.Serialize(stream, allSaveData);
        // remember to close the stream
        stream.Close();
    }

    public static AllSaveData Load()
    {
        string path;
#if !UNITY_WEBGL
        var pathWithEnv = @"%userprofile%\AppData\Local\Motorland0123\Save\save1.txt";
        path = Environment.ExpandEnvironmentVariables(pathWithEnv);
#elif UNITY_WEBGL
        path = "/idbfs/Motorland0123" + "/save1.txt";
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
