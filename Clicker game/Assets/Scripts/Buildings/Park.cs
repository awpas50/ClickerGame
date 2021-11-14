using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Park : MonoBehaviour
{
    
    [Header("Manual collected resources (Reduce pollution)")]
    private float pollutionReduced_initial;
    public float pollutionReduced;
    public float interval;
    public float intervalPassed = 0;

    public float totalProduction;
    public float totalProduction_buff;
    public float extraProduction;

    [Header("Auto generated resources (Reduce pollution)")]
    public float CleanAirProduced_auto;
    public float CleanAirInterval_auto;
    
    public float CleanAirProduced_auto_initial;

    [Header("What to instantiate")]
    public GameObject parkPopUp;
    [Header("Where to instantiate (Do not edit)")]
    public GameObject popupStorageCanvas;
    [Header("Attached pop up (Do not edit)")]
    public GameObject parkPopUpREF;
    [Header("Script reference (Do not edit)")]
    public BuildingBuff buildingBuff;
    public BuildingLevel buildingLevel;
    public BuildingState buildingState;
    [Header("Resources multiplier")]
    public float levelMultipiler = 1f;
    public float efficiency = 1f;

    void Start()
    {
        CleanAirProduced_auto_initial = CleanAirProduced_auto;
        pollutionReduced_initial = pollutionReduced;

        popupStorageCanvas = GameObject.FindGameObjectWithTag("StorageCanvas");
        buildingBuff = GetComponent<BuildingBuff>();
        buildingLevel = GetComponent<BuildingLevel>();
        buildingState = GetComponent<BuildingState>();
        StartCoroutine(ReducePollution_POP_UP(interval - intervalPassed));
        StartCoroutine(ReducePollution_AUTOMATIC(CleanAirInterval_auto));
    }
    private void Update()
    {
        levelMultipiler = 1f + ((buildingLevel.level - 1) * 0.2f);
        // each house increase the efficiency by 25%, whereas each factory nearby decrease the efficiency by 25%. Minimum 20% output.
        float factoryPollutionPunishment = buildingBuff.nearbyFactory * 0.4f;
        if(factoryPollutionPunishment >= 0.8f)
        {
            factoryPollutionPunishment = 0.8f;
        }
        efficiency = 1 + buildingBuff.houseEfficiencyTotal + buildingBuff.nearbyMainBuilding * Objective.townHallEfficiency - factoryPollutionPunishment;

        totalProduction = pollutionReduced * levelMultipiler;
        totalProduction_buff = pollutionReduced * levelMultipiler * efficiency;
        extraProduction = totalProduction_buff - totalProduction;

        //Round off
        totalProduction = (float)Math.Round(totalProduction, 1);
        totalProduction_buff = (float)Math.Round(totalProduction_buff, 1);
        extraProduction = (float)Math.Round(extraProduction, 1);

        CleanAirProduced_auto = CleanAirProduced_auto_initial + (buildingLevel.level - 1) * 0.02f;

        // interval passed (indicates the current process of generating resources, used for the save system)
        intervalPassed += Time.deltaTime;
        if (intervalPassed >= interval)
        {
            intervalPassed = interval;
        }
    }

    public IEnumerator ReducePollution_POP_UP(float interval)
    {
        // Instaniate a pop up every few seconds. Next pop up won't be spawned unless the previous pop up has been collected
        while (parkPopUpREF == null)
        {
            yield return new WaitForSeconds(interval);
            parkPopUpREF = Instantiate(parkPopUp, transform.position, Quaternion.identity);
            parkPopUpREF.transform.SetParent(popupStorageCanvas.transform);
            parkPopUpREF.GetComponent<ParkPopUp>().parkREF = gameObject;
        }
    }
    public void GetResources(float pollutionReduced, float efficiency, float levelMultipiler)
    {
        Pollution.POLLUTION -= pollutionReduced * efficiency * levelMultipiler;
    }
    IEnumerator ReducePollution_AUTOMATIC(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Pollution.POLLUTION -= CleanAirProduced_auto;
        }
    }

    public void ResetInterval()
    {
        intervalPassed = 0;
    }
}
