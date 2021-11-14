using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerpetualMachine : MonoBehaviour
{
    private BuildingState buildingState;
    private BuildingLevel buildingLevel;
    private BuildingBuff buildingBuff;
    private float efficiency_initial;
    public float efficiency = 0.75f;
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
        // Base efficiency 75%, where each upgrade increase its performance by 50%.
        // 100% + ((n - 1) * 50%))

        //extraEfficiency = buildingBuff.nearbyMainBuilding * (Objective.townHallLevel - 1) * 0.05f;
        baseEfficiency = efficiency_initial + ((buildingLevel.level - 1) * 0.5f);
        efficiency = baseEfficiency;
        //efficiency = baseEfficiency + extraEfficiency;
    }
}
