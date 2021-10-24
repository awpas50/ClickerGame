using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [Header("TownHall")]
    public static int townHallLevel = 1;
    public static float townHallEfficiency = 0.25f;
    private MainBuilding mainBuildingScript;

    public float objective1;
    public float objective2;
    public float objective3;
    public float objective4;

    public bool additionalTrigger1 = false;
    public bool additionalTrigger2 = false;
    public bool additionalTrigger3 = false;
    public bool additionalTrigger4 = false;

    private void Start()
    {
        mainBuildingScript = FindObjectOfType<MainBuilding>();
    }
    void Update()
    {
        //GameManager.i.allNodes = GameManager.i.GetAllNodes();
        if (Currency.MONEY >= objective1 && !additionalTrigger1)
        {
            townHallLevel = 2;
            
        }
        if (Currency.MONEY >= objective2 && !additionalTrigger2)
        {
            townHallLevel = 3;
            
        }
        if (Currency.MONEY >= objective3 && !additionalTrigger3)
        {
            townHallLevel = 4;
            
        }
        if (Currency.MONEY >= objective4 && !additionalTrigger4)
        {
            townHallLevel = 5;
        }

        if (townHallLevel == 5)
        {
            mainBuildingScript.moneyEachClick = 10;
            townHallEfficiency = 0.6f;
            UIManager.i.objectiveText.text = "";

            if (!additionalTrigger4)
            {
                SpecialBuildingCount.platform1Count += 5;
                
                additionalTrigger4 = true;
            }
        }
        else if(townHallLevel == 4)
        {
            mainBuildingScript.moneyEachClick = 6;
            townHallEfficiency = 0.45f;
            UIManager.i.objectiveText.text = "Reach 10000";
            if (!additionalTrigger3)
            {
                SpecialBuildingCount.platform1Count += 5;
                additionalTrigger3 = true;
            }
        }
        else if(townHallLevel == 3)
        {
            mainBuildingScript.moneyEachClick = 4;
            townHallEfficiency = 0.35f;
            UIManager.i.objectiveText.text = "Reach 6000";
            if (!additionalTrigger2)
            {
                SpecialBuildingCount.platform1Count += 4;
                additionalTrigger2 = true;
            }
        }
        else if (townHallLevel == 2)
        {
            mainBuildingScript.moneyEachClick = 2.5f;
            townHallEfficiency = 0.3f;
            UIManager.i.objectiveText.text = "Reach 2500";
            if (!additionalTrigger1)
            {
                SpecialBuildingCount.platform1Count += 4;
                additionalTrigger1 = true;
            }
        }
        else if (townHallLevel == 1)
        {
            mainBuildingScript.moneyEachClick = 1;
            townHallEfficiency = 0.25f;
            UIManager.i.objectiveText.text = "Reach 1000";
        }
    }
}
