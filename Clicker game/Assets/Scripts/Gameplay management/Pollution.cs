using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollution : MonoBehaviour
{
    public float pollution;
    public static float POLLUTION;
    void Start()
    {
        POLLUTION = pollution;
    }
    
    void Update()
    {
        pollution = POLLUTION;
    }
}
