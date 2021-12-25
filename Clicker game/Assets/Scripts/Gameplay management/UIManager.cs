using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager i;

    public Color textDefaultColor;
    public Color textWarningColor;
    [Header("Building details")]
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI Line1Text;
    public TextMeshProUGUI Line2Text;
    public TextMeshProUGUI Line3Text;
    public TextMeshProUGUI Line4Text;
    public TextMeshProUGUI sellText;
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI upgradePlusText;
    public Button sellButton;
    public Button upgradeButton;
    public Button upgradePlusButton;
    public Button OKButton;

    [Header("Building Info")]
    public TextMeshProUGUI buildingInfo_name;
    public TextMeshProUGUI buildingInfo_price;
    public TextMeshProUGUI buildingInfo;

    [Header("Top UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI pollutionText;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI missionObjectiveText;
    public Button tutorialButton;
    public Button resetCameraButton;
    public Button pauseButton;
    public Image image;
    public GameObject MainMenuUI;
    public GameObject TopUI;
    public GameObject BottomUI;
    public GameObject RightUI;
    public GameObject BuildingDetailsUI;
    public GameObject BuildingInstructionUI;

    [Header("Bottom UI")]
    public GameObject buildingPage1;
    public GameObject buildingPage2;
    public TextMeshProUGUI platform1Text;

    public Button building1_button;
    public Button building2_button;
    public Button building3_button;
    public Button building4_button;
    public Button building5_button;
    public Button building6_button;

    [Header("Popup storage UI")]
    public GameObject popUpStorage;

    void Awake()
    {
        //debug
        if (i != null)
        {
            Debug.LogError("More than one UIManager in scene");
            return;
        }
        i = this;
    }
    
    void Update()
    {
        moneyText.text = Currency.MONEY.ToString();

        if (Currency.MONEY >= 1000)
        {
            float temp = Mathf.RoundToInt(Currency.MONEY);
            moneyText.text = temp.ToString();
        }
        else if (Currency.MONEY < 1000)
        {
            float temp = (float)Math.Round(Currency.MONEY, 1);
            moneyText.text = temp.ToString();
        }
        pollutionText.text = Math.Round(Pollution.POLLUTION, 2).ToString();

        platform1Text.text = "x" + SpecialBuildingCount.platform1Count;

        if (Objective.townHallLevel == 1)
        {
            building4_button.interactable = false;
            building5_button.interactable = false;
            building6_button.interactable = false;
        }
        else if (Objective.townHallLevel == 2)
        {
            building4_button.interactable = true;
            building5_button.interactable = true;
            building6_button.interactable = false;
        }
        else if (Objective.townHallLevel >= 3)
        {
            building4_button.interactable = true;
            building5_button.interactable = true;
            building6_button.interactable = true;
        }
    }

    public void SelectBuildingList1()
    {
        buildingPage1.SetActive(true);
        buildingPage2.SetActive(false);
    }
    public void SelectBuildingList2()
    {
        buildingPage1.SetActive(false);
        buildingPage2.SetActive(true);
    }
}
