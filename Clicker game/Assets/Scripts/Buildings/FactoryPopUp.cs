using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryPopUp : MonoBehaviour
{
    public Image img;
    public Vector3 offset;
    [Header("Assigned in script - do not edit")]
    public GameObject factoryREF;
    public Factory factoryREF_script;
    public GameObject popupStorageCanvas;
    [Header("Assigned manually")]
    public GameObject secondPopUp;

    private void Start()
    {
        factoryREF_script = factoryREF.GetComponent<Factory>();
        popupStorageCanvas = GameObject.FindGameObjectWithTag("StorageCanvas");
    }
    void Update()
    {
        // Screen border
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector3 pos = Camera.main.WorldToScreenPoint(factoryREF.transform.position + offset);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
    }

    // When clicked
    public void ButtonEvent()
    {
        factoryREF_script.GetResources(factoryREF_script.moneyProduced_popup, factoryREF_script.efficiency, factoryREF_script.pollutionProduced_popup);
        // Instantiate an additional pop up
        GameObject secondPopUpPrefab = Instantiate(secondPopUp, Camera.main.WorldToScreenPoint(factoryREF.transform.position + offset), Quaternion.identity);
        secondPopUpPrefab.transform.SetParent(popupStorageCanvas.transform);
        secondPopUpPrefab.GetComponent<BuildingPopUp>().buildingREF = factoryREF;
        secondPopUpPrefab.GetComponent<BuildingPopUp>().resourceText.text = "+" + (factoryREF_script.moneyProduced_popup * factoryREF_script.efficiency).ToString();
        // Remove reference
        factoryREF_script.factoryPopUpREF = null;
        // Restart coroutine
        factoryREF_script.StartCoroutine(factoryREF_script.Production_POP_UP(factoryREF_script.interval_popup));
        // Close this pop up.
        Destroy(gameObject);
    }
}
