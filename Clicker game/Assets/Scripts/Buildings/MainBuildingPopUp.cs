using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBuildingPopUp : MonoBehaviour
{
    [Header("Props")]
    public Image img;
    public float flyingSpeed;
    public Vector3 offset;
    Vector3 pos;
    private GameObject mainBuilding;

    void Start()
    {
        mainBuilding = GameObject.FindGameObjectWithTag("MainBuilding");
        pos = Camera.main.WorldToScreenPoint(mainBuilding.transform.position + offset);
    }
    
    void Update()
    {
        img.transform.position += Vector3.up * flyingSpeed * Time.deltaTime;
        Destroy(gameObject, 1f);
    }
}
