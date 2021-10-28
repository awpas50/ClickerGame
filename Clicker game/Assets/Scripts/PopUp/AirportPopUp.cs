using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirportPopUp : MonoBehaviour
{
    public Image img;
    public Vector3 offset;
    [Header("Assigned in script - do not edit")]
    public GameObject airportREF;
    public Airport airportREF_script;
    public GameObject popupStorageCanvas;
    [Header("Assigned manually")]
    public GameObject secondPopUp;

    private void Start()
    {
        //Invoke = delay Start()
        //Invoke("AirportREFAssign", 0.01f);
        
        popupStorageCanvas = GameObject.FindGameObjectWithTag("StorageCanvas");
        
    }
    void Update()
    {
        //DEBUG: destroy this pop up if airport is destroyed.
        //DEBUG, and I don't know why this bug happened (NullReference)
        if(!airportREF)
        {
            Destroy(gameObject);
            return;
        }
        airportREF_script = airportREF.GetComponent<Airport>();

        // Screen border
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector3 pos = Camera.main.WorldToScreenPoint(airportREF.transform.position + offset);

        //pos.x = Mathf.Clamp(pos.x, minX, maxX);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
    }

    // When clicked
    public void ButtonEvent()
    {
        // Audio
        AudioManager.instance.Play(SoundList.GetMoney);
        int seed = Random.Range(0, 100);
        if(seed >= 24)
        {
            float randomVal = Random.Range(100, 500);
            Currency.MONEY += randomVal;

            // Instantiate an additional pop up
            GameObject secondPopUpPrefab = Instantiate(secondPopUp, Camera.main.WorldToScreenPoint(airportREF.transform.position + offset), Quaternion.identity);
            secondPopUpPrefab.transform.SetParent(popupStorageCanvas.transform);
            secondPopUpPrefab.GetComponent<BuildingPopUp>().buildingREF = airportREF;
            secondPopUpPrefab.GetComponent<BuildingPopUp>().resourceText.text = "+" + randomVal;
        }
        else if(seed < 24)
        {
            SpecialBuildingCount.platform1Count += 1;
            // Instantiate an additional pop up
            GameObject secondPopUpPrefab = Instantiate(secondPopUp, Camera.main.WorldToScreenPoint(airportREF.transform.position + offset), Quaternion.identity);
            secondPopUpPrefab.transform.SetParent(popupStorageCanvas.transform);
            secondPopUpPrefab.GetComponent<BuildingPopUp>().buildingREF = airportREF;
            secondPopUpPrefab.GetComponent<BuildingPopUp>().resourceText.text = "+1 platform";
        }
        
        // ** tell the airplane to leave the base.
        //airportREF_script.airplaneScript.state = Airplane.State.Departure;
        airportREF_script.airplaneScript.redeparture = true;
        // Remove reference
        airportREF_script.airportPopUpREF = null;
        // Close this pop up.
        Destroy(gameObject);
    }

    void AirportREFAssign()
    {
        airportREF_script = airportREF.GetComponent<Airport>();
    }
}
