using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkPopUp : MonoBehaviour
{
    public Image img;
    public Vector3 offset;
    [Header("Assigned in script - do not edit")]
    public GameObject parkREF;
    public Park parkREF_script;
    public GameObject popupStorageCanvas;
    [Header("Assigned manually")]
    public GameObject secondPopUp;

    private void Start()
    {
        parkREF_script = parkREF.GetComponent<Park>();
        popupStorageCanvas = GameObject.FindGameObjectWithTag("StorageCanvas");
    }
    void Update()
    {
        // Screen border
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector3 pos = Camera.main.WorldToScreenPoint(parkREF.transform.position + offset);

        //pos.x = Mathf.Clamp(pos.x, minX, maxX);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
    }

    // When clicked
    public void ButtonEvent()
    {
        // Audio
        AudioManager.instance.Play(SoundList.GetCleanAir);
        parkREF_script.pollutionReduced = (float)Math.Round(parkREF_script.pollutionReduced, 1);
        parkREF_script.GetResources(parkREF_script.pollutionReduced, parkREF_script.efficiency, parkREF_script.levelMultipiler);
        // Instantiate an additional pop up
        GameObject secondPopUpPrefab = Instantiate(secondPopUp, Camera.main.WorldToScreenPoint(parkREF.transform.position + offset), Quaternion.identity);
        secondPopUpPrefab.transform.SetParent(popupStorageCanvas.transform);
        secondPopUpPrefab.GetComponent<BuildingPopUp>().buildingREF = parkREF;
        secondPopUpPrefab.GetComponent<BuildingPopUp>().resourceText.text = "-" + (parkREF_script.pollutionReduced * parkREF_script.efficiency * parkREF_script.levelMultipiler).ToString() + " pollution";
        // Remove reference
        parkREF_script.parkPopUpREF = null;
        // Restart coroutine
        parkREF_script.StartCoroutine(parkREF_script.ReducePollution_POP_UP(parkREF_script.pollutionReduced, parkREF_script.interval));
        // Close this pop up.
        Destroy(gameObject);
    }
}
