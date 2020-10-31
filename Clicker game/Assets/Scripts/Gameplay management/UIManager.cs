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
    public Button sellButton;
    public Button upgradeButton;
    public Button OKButton;

    [Header("Top UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI pollutionText;
    public Image image;
    public GameObject MainMenuUI;
    public GameObject TopUI;
    public GameObject BottomUI;
    public GameObject RightUI;
    public GameObject BuildingDetailsUI;
    public GameObject BuildingInstructionUI;

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
        moneyText.text = "RESOURCES " + Currency.MONEY;
        pollutionText.text = "POLLUTION " + Math.Round(Pollution.POLLUTION, 2).ToString();
    }
}
