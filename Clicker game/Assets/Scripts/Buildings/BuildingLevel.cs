using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLevel : MonoBehaviour
{
    public int buildingBaseCost;
    public int level = 1;
    public int maxLevel = 10;
    [Header("First element & last element always = 0")]
    public int[] costEachLevel;

    [HideInInspector] public int sellCost;
    private void Update()
    {
        int everyLevelCost = 0;
        for(int i = 0; i < level - 1; i++)
        {
            everyLevelCost += costEachLevel[i];
        }
        sellCost = Mathf.RoundToInt((buildingBaseCost + everyLevelCost) / 2);
    }
}
