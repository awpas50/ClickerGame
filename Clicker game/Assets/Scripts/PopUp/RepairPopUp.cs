using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RepairPopUp : MonoBehaviour
{
    public Image img;
    public Vector3 offset;
    [Header("Assigned in script - do not edit")]
    public GameObject ruinREF;
    public TextMeshProUGUI repairText;
    public Ruin ruinREF_script;
    public GameObject popupStorageCanvas;
    [Header("Assigned manually")]
    public GameObject secondPopUp;

    private void Start()
    {
        popupStorageCanvas = GameObject.FindGameObjectWithTag("StorageCanvas");
        ruinREF_script = ruinREF.GetComponent<Ruin>();
        repairText.text = "$" + ruinREF_script.repairCost;
    }
    void Update()
    {
        // Screen border
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector3 pos = Camera.main.WorldToScreenPoint(ruinREF.transform.position + offset);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
    }

    // When clicked
    public void ButtonEvent()
    {
        // if enough repair cost
        if(Currency.MONEY >= ruinREF_script.repairCost)
        {
            Currency.MONEY -= ruinREF_script.repairCost;
            // Audio
            AudioManager.instance.Play(SoundList.Repair);

            // Restore building (type, level)
            GameObject restoredBuilding = Instantiate(ruinREF_script.buildingData, ruinREF_script.node.transform.position, Quaternion.identity);
            restoredBuilding.GetComponent<BuildingLevel>().level = ruinREF_script.buildingLevel;
            restoredBuilding.GetComponent<BuildingState>().node = ruinREF_script.node;
            ruinREF_script.node.GetComponent<Node>().building_REF = restoredBuilding;
            // Instantiate an additional pop up
            GameObject secondPopUpPrefab = Instantiate(secondPopUp, Camera.main.WorldToScreenPoint(ruinREF.transform.position + offset), Quaternion.identity);
            secondPopUpPrefab.transform.SetParent(popupStorageCanvas.transform);
            secondPopUpPrefab.GetComponent<BuildingPopUp>().buildingREF = restoredBuilding;
            secondPopUpPrefab.GetComponent<BuildingPopUp>().resourceText.text = "-" + ruinREF_script.repairCost;
            // Remove the ruin.
            Destroy(ruinREF);
            // Close this pop up.
            Destroy(gameObject);
        }
        
    }
}
