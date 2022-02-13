using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class ContinueButtonState : MonoBehaviour
{
    string path;
    private void Start()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        path = Application.persistentDataPath + "/save1.txt";
#elif UNITY_STANDALONE_OSX
        path = Application.persistentDataPath + "/save1.txt";
#elif UNITY_STANDALONE_LINUX
        path = Application.persistentDataPath + "/motorlandSave1.txt";
#elif UNITY_WEBGL
        path = "/idbfs/motorland0212" + "/save1.dat";
#endif
    }
    void Update()
    {
        if (File.Exists(path))
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
