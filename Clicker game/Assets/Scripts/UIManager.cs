using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI pollutionText;
    void Start()
    {
        
    }
    
    void Update()
    {
        moneyText.text = "Money " + Currency.MONEY;
        pollutionText.text = "Pollution " + Pollution.POLLUTION;
    }
}
