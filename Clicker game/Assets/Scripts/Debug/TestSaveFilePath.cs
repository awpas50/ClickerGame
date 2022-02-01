using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestSaveFilePath : MonoBehaviour
{
    void Update()
    {
#if UNITY_WEBGL
        gameObject.GetComponent<TextMeshProUGUI>().text = Application.persistentDataPath;
#endif
    }
}
