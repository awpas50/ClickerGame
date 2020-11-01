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
    public float objective4;

    public bool objective1Reached;
    public bool objective2Reached;
    public bool objective3Reached;
    public bool objective4Reached;

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
            objective1Reached = true;
        }
        if (Currency.MONEY >= objective2)
        {
            Platform3.SetActive(true);
            objective2Reached = true;
        }
        if (Currency.MONEY >= objective3)
        {
            Platform4.SetActive(true);
            objective3Reached = true;
        }
        if (Currency.MONEY >= objective4)
        {
            objective4Reached = true;
        }

        if (objective4Reached)
        {
            mainBuildingScript.moneyEachClick = 10;
            UIManager.i.objectiveText.text = "";
        }
        else if(objective3Reached)
        {
            mainBuildingScript.moneyEachClick = 7;
            UIManager.i.objectiveText.text = "Reach 10000";
        }
        else if(objective2Reached)
        {
            mainBuildingScript.moneyEachClick = 5;
            UIManager.i.objectiveText.text = "Reach 6000";
        }
        else if (objective1Reached)
        {
            mainBuildingScript.moneyEachClick = 3;
            UIManager.i.objectiveText.text = "Reach 2500";
        }
        else if (!objective4Reached && !objective3Reached && !objective2Reached && !objective1Reached)
        {
            mainBuildingScript.moneyEachClick = 1;
            UIManager.i.objectiveText.text = "Reach 1000";
        }
    }
}
