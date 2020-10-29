using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingPopUp : MonoBehaviour
{
    [Header("Props")]
    public Image img;
    public float flyingSpeed;
    public Vector3 offset;
    Vector3 pos;
    [Header("Assigned in script - do not edit")]
    public GameObject buildingREF;
    [Header("Resources")]
    public TextMeshProUGUI resourceText;

    void Start()
    {
        pos = Camera.main.WorldToScreenPoint(buildingREF.transform.position + offset);
        img.transform.position = pos;
    }

    void Update()
    {
        img.transform.position += Vector3.up * flyingSpeed * Time.deltaTime;
        Destroy(gameObject, 1.5f);
    }
}
