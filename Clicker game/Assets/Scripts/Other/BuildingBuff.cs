using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuff : MonoBehaviour
{
    public List<GameObject> allBuildingList;
    [Header("Check nearby buildings")]
    public int nearbyHouse;
    public int nearbyFactory;
    public int nearbyPark;
    public int nearbyTurret;
    public int nearbyMainBuilding;
    public int nearbyAirport;
    public int nearbyLogistic;
    public int nearbyPerpetual;
    [Header("nearbyHouse properties")]
    public List<float> houseEfficiencyList;
    public float houseEfficiencyTotal;

    public List<float> perpetualEfficiencyList;
    public float perpetualEfficiencyTotal;
}
