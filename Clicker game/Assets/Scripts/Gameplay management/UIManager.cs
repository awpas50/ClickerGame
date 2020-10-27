using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI buildingLevel;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI pollutionText;
    public Image image;
    public GameObject TopUI;
    public GameObject BottomUI;
    public GameObject RightUI;
    public GameObject BuildingDetailsUI;
    public GameObject BuildingInstructionUI;

    void Awake()
    {
        //debug
        if (instance != null)
        {
            Debug.LogError("More than one UIManager in scene");
            return;
        }
        instance = this;
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        moneyText.text = "MONEY " + Currency.MONEY;
        pollutionText.text = "POLLUTION " + Math.Round(Pollution.POLLUTION, 2).ToString();
        if(GameManager.instance.buildingSelectedInScene)
        {
            if(GameManager.instance.buildingSelectedInScene.GetComponent<BuildingLevel>())
            {
                buildingLevel.text = "LEVEL " + GameManager.instance.buildingSelectedInScene.GetComponent<BuildingLevel>().level;
            }
            else
            {
                buildingLevel.text = "";
            }
        }
    }
}
