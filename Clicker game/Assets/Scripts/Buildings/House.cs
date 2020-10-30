using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    BuildingState buildingState;
    BuildingLevel buildingLevel;
    private float efficiency_initial;
    public float efficiency = 0.25f;   
    void Start()
    {
        // efficiency_initial = 25%
        efficiency_initial = efficiency;
        buildingLevel = GetComponent<BuildingLevel>();
        buildingState = GetComponent<BuildingState>();
    }
    
    void Update()
    {
        // Base efficiency 25%, where each upgrade increase its performance by 5%.
        // 25% + ((n - 1) * 5%)
        efficiency = efficiency_initial + ((buildingLevel.level - 1) * 0.05f);
    }
}
