using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainBuildingPopUp : MonoBehaviour
{
    public float realMoneyEachClick;
    [Header("Props")]
    public Image img;
    public float flyingSpeed;
    public Vector3 offset;
    Vector3 pos;
    private GameObject mainBuilding;
    private MainBuilding mainBuildingScript;
    public TextMeshProUGUI moneyText;

    void Start()
    {
        mainBuilding = GameObject.FindGameObjectWithTag("MainBuilding");
        pos = Camera.main.WorldToScreenPoint(mainBuilding.transform.position + offset);
        img.transform.position = pos;
        mainBuildingScript = mainBuilding.GetComponent<MainBuilding>();

        moneyText.text = "+" + realMoneyEachClick;
    }
    
    void Update()
    {
        img.transform.position += Vector3.up * flyingSpeed * Time.deltaTime;
        Destroy(gameObject, 1.5f);
    }
}
