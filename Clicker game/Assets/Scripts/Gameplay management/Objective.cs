using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private MainBuilding mainBuildingScript;
    public GameObject Platform2;
    public GameObject Platform3;
    public GameObject Platform4;

    public float objective1;
    public float objective2;
    public float objective3;

    private void Start()
    {
        mainBuildingScript = FindObjectOfType<MainBuilding>();
    }
    void Update()
    {
        GameManager.i.allNodes = GameManager.i.GetAllNodes();
        if (Currency.MONEY >= objective1)
        {
            Platform2.SetActive(true);
            mainBuildingScript.moneyEachClick = 3;
        }
        if (Currency.MONEY >= objective2)
        {
            Platform3.SetActive(true);
            mainBuildingScript.moneyEachClick = 5;
        }
        if (Currency.MONEY >= objective3)
        {
            Platform4.SetActive(true);
            mainBuildingScript.moneyEachClick = 7;
        }
    }
}
