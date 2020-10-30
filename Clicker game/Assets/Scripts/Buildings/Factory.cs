using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Factory : MonoBehaviour
{
    BuildingState buildingState;
    [Header("Manual collected resources (money)")]
    [HideInInspector] private float moneyProduced_initial;
    public float moneyProduced;
    public float pollutionProduced;
    public float interval;

    public float totalProduction;
    public float totalProduction_buff;
    public float extraProduction;
    public float totalPollution;

    [Header("Auto generated resources (money)")]
    public float moneyProduced_auto;
    public float interval_auto;
    public float pollutionProduced_auto;
    public float pollutionInterval_auto;

    [Header("What to instantiate")]
    public GameObject factoryPopUp;
    [Header("Where to instantiate (Do not edit)")]
    public GameObject popupStorageCanvas;
    [Header("Attached pop up (Do not edit)")]
    public GameObject factoryPopUpREF;
    [Header("Script reference (Do not edit)")]
    public BuildingBuff buildingBuff;
    public BuildingLevel buildingLevel;
    [Header("Resources multiplier")]
    public float levelMultipiler = 1f;
    public float efficiency = 1f;

    void Start()
    {
        moneyProduced_initial = moneyProduced;
        popupStorageCanvas = GameObject.FindGameObjectWithTag("StorageCanvas");
        buildingBuff = GetComponent<BuildingBuff>();
        buildingLevel = GetComponent<BuildingLevel>();
        buildingState = GetComponent<BuildingState>();
        StartCoroutine(Production_POP_UP(interval));
        StartCoroutine(Production_AUTOMATIC(moneyProduced_auto, interval_auto));
        StartCoroutine(Pollution_AUTOMATIC(pollutionProduced_auto, pollutionInterval_auto));
    }

    private void Update()
    {
        levelMultipiler = 1f + ((buildingLevel.level - 1) * 0.35f);
        // each house & main building nearby increase the efficiency by 25%.
        efficiency = 1 + buildingBuff.houseEfficiencyTotal + buildingBuff.nearbyMainBuilding * 0.25f;

        totalProduction = moneyProduced * levelMultipiler;
        totalProduction_buff = moneyProduced * levelMultipiler * efficiency;
        extraProduction = totalProduction_buff - totalProduction;

        //Round off
        totalProduction = (float)Math.Round(totalProduction, 1);
        totalProduction_buff = (float)Math.Round(totalProduction_buff, 1);
        extraProduction = (float)Math.Round(extraProduction, 1);

        //Pollution
        totalPollution = pollutionProduced * levelMultipiler;
    }

    public IEnumerator Production_POP_UP(float interval)
    {
        // Instaniate a pop up every few seconds. Next pop up won't be spawned unless the previous pop up has been collected
        while (factoryPopUpREF == null && buildingState.isWorking)
        {
            yield return new WaitForSeconds(interval);
            factoryPopUpREF = Instantiate(factoryPopUp, transform.position, Quaternion.identity);
            factoryPopUpREF.transform.SetParent(popupStorageCanvas.transform);
            factoryPopUpREF.GetComponent<FactoryPopUp>().factoryREF = gameObject;
        }
    }
    public void GetResources(float moneyProduced, float efficiency, float pollutionProduced, float levelMultipiler)
    {
        Currency.MONEY += moneyProduced * efficiency * levelMultipiler;
        Pollution.POLLUTION += pollutionProduced;
    }

    public IEnumerator Production_AUTOMATIC(float moneyProduced, float interval)
    {
        while(true)
        {
            yield return new WaitForSeconds(interval);
            Currency.MONEY += moneyProduced;
        }
    }
    public IEnumerator Pollution_AUTOMATIC(float pollutionProduced, float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Pollution.POLLUTION += pollutionProduced;
        }
    }
}
