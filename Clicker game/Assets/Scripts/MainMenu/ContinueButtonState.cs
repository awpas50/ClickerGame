using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ContinueButtonState : MonoBehaviour
{
    string path;
    private void Start()
    {
        path = Application.persistentDataPath + " save1.txt";
        Debug.Log(path);
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
