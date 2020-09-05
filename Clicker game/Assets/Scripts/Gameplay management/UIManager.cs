using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI pollutionText;
    public GameObject TopUI;
    public GameObject BottomUI;
    public GameObject RightUI;

    void Start()
    {
        
    }
    
    void Update()
    {
        moneyText.text = "MONEY " + Currency.MONEY;
        pollutionText.text = "POLLUTION " + Pollution.POLLUTION;
    }
}
