using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public GameObject Platform2;
    public GameObject Platform3;
    public GameObject Platform4;

    void Start()
    {
        
    }
    
    void Update()
    {
        GameManager.instance.allNodes = GameManager.instance.GetAllNodes();
        if (Currency.MONEY > 2500)
        {
            Platform2.SetActive(true);
        }
        if (Currency.MONEY > 10000)
        {
            Platform3.SetActive(true);
        }
        if (Currency.MONEY > 25000)
        {
            Platform4.SetActive(true);
        }
    }
}
