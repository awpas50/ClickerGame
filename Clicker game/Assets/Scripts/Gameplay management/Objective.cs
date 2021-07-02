using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [Header("TownHall")]
    public static int townHallLevel = 1;
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

    private bool additionalTrigger1 = false;
    private bool additionalTrigger2 = false;
    private bool additionalTrigger3 = false;
    private bool additionalTrigger4 = false;

    private void Start()
    {
        mainBuildingScript = FindObjectOfType<MainBuilding>();
    }
    void Update()
    {
        //GameManager.i.allNodes = GameManager.i.GetAllNodes();
        if (Currency.MONEY >= objective1)
        {
            //Platform2.SetActive(true);
            objective1Reached = true;
            townHallLevel = 2;
        }
        if (Currency.MONEY >= objective2)
        {
            //Platform3.SetActive(true);
            objective2Reached = true;
            townHallLevel = 3;
        }
        if (Currency.MONEY >= objective3)
        {
            //Platform4.SetActive(true);
            objective3Reached = true;
            townHallLevel = 4;
        }
        if (Currency.MONEY >= objective4)
        {
            objective4Reached = true;
            townHallLevel = 5;
        }

        if (objective4Reached)
        {
            mainBuildingScript.moneyEachClick = 12;
            UIManager.i.objectiveText.text = "";
            if (!additionalTrigger4)
            {
                SpecialBuildingCount.platform1Count += 5;
                additionalTrigger4 = true;
            }
        }
        else if(objective3Reached)
        {
            mainBuildingScript.moneyEachClick = 8;
            UIManager.i.objectiveText.text = "Reach 10000";
            if (!additionalTrigger3)
            {
                SpecialBuildingCount.platform1Count += 5;
                additionalTrigger3 = true;
            }
        }
        else if(objective2Reached)
        {
            mainBuildingScript.moneyEachClick = 6;
            UIManager.i.objectiveText.text = "Reach 6000";
            if (!additionalTrigger2)
            {
                SpecialBuildingCount.platform1Count += 4;
                additionalTrigger2 = true;
            }
        }
        else if (objective1Reached)
        {
            mainBuildingScript.moneyEachClick = 3;
            UIManager.i.objectiveText.text = "Reach 2500";
            if (!additionalTrigger1)
            {
                SpecialBuildingCount.platform1Count += 4;
                additionalTrigger1 = true;
            }
        }
        else if (!objective4Reached && !objective3Reached && !objective2Reached && !objective1Reached)
        {
            mainBuildingScript.moneyEachClick = 1;
            UIManager.i.objectiveText.text = "Reach 1000";
        }
    }
}
