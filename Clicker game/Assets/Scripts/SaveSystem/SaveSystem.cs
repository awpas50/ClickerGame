using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save()
    {
        // create a binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        // declare a path 
        string path = Application.persistentDataPath + " save1.txt";
        FileStream stream = new FileStream(path, FileMode.Create);
        AllSaveData allSaveData = new AllSaveData();
        // encrypt the data into binary format
        formatter.Serialize(stream, allSaveData);
        // remember to close the stream
        stream.Close();
    }

    public static AllSaveData Load()
    {
        string path = Application.persistentDataPath + " save1.txt";
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
