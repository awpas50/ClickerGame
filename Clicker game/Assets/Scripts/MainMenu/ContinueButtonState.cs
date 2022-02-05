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
        var pathWithEnv = @"%userprofile%\AppData\Local\Motorland0123\Save\save1.txt";
        path = Environment.ExpandEnvironmentVariables(pathWithEnv);
#elif UNITY_STANDALONE_OSX
        path = Application.persistentDataPath + "/save1.txt";
#elif UNITY_STANDALONE_LINUX
        path = Application.persistentDataPath + "/Motorland/Save/save1.txt";
#elif UNITY_WEBGL
        path = "/idbfs/Motorland0123" + "/save1.txt";
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
