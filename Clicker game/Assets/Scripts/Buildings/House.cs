using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private BuildingState buildingState;
    private BuildingLevel buildingLevel;
    private BuildingBuff buildingBuff;
    private float efficiency_initial;
    public float efficiency = 0.25f;
    public float baseEfficiency;
    //public float extraEfficiency;
    
    void Start()
    {
        // efficiency_initial = 25%
        efficiency_initial = efficiency;
        buildingLevel = GetComponent<BuildingLevel>();
        buildingState = GetComponent<BuildingState>();
        buildingBuff = GetComponent<BuildingBuff>();
    }
    
    void Update()
    {
        // Base efficiency 25%, where each upgrade increase its performance by 5%.
        // each town hall level grants 5% efficiency started from level 2
        // 25% + ((n - 1) * 5%) + ((townHall level - 1) * 5%)

        //extraEfficiency = buildingBuff.nearbyMainBuilding * (Objective.townHallLevel - 1) * 0.05f;
        baseEfficiency = efficiency_initial + ((buildingLevel.level - 1) * 0.05f);
        efficiency = baseEfficiency;
        //efficiency = baseEfficiency + extraEfficiency;
    }
}
