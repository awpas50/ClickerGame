using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private MainBuilding mainBuildingScript;
    public GameObject Platform2;
    public GameObject Platform3;
    public GameObject Platform4;

    private void Start()
    {
        mainBuildingScript = FindObjectOfType<MainBuilding>();
    }
    void Update()
    {
        GameManager.i.allNodes = GameManager.i.GetAllNodes();
        if (Currency.MONEY >= 1500)
        {
            Platform2.SetActive(true);
            mainBuildingScript.moneyEachClick = 3;
        }
        if (Currency.MONEY >= 5000)
        {
            Platform3.SetActive(true);
            mainBuildingScript.moneyEachClick = 5;
        }
        if (Currency.MONEY >= 10000)
        {
            Platform4.SetActive(true);
            mainBuildingScript.moneyEachClick = 7;
        }
    }
}
