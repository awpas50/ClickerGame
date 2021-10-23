using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airport : MonoBehaviour
{
    public GameObject airplane;
    [Header("Do not edit")]
    public Airplane airplaneScript;
    public GameObject airplaneREF;
    public GameObject popupStorageCanvas;
    [Header("Airport landing point")]
    public Transform point1;
    public Transform point2;
    [Header("Auto generated pollution")]
    public float pollutionProduced_auto;
    public float pollutionInterval_auto;
    public float pollutionProduced_auto_initial;

    [Header("What to instainate")]
    public GameObject airportPopUp;
    [Header("pop up (do not edit)")]
    public GameObject airportPopUpREF;
    [HideInInspector] public BuildingBuff buildingBuff;
    [HideInInspector] public BuildingLevel buildingLevel;

    [HideInInspector] public int chanceVariable;

    void Awake()
    {
        pollutionProduced_auto_initial = pollutionProduced_auto;
        // random point 2 (peak) location
        point2.position = new Vector3(Random.Range(0f, 1f), Random.Range(4.5f, 7f), Random.Range(-0.5f, 1f));
        buildingBuff = GetComponent<BuildingBuff>();
        buildingLevel = GetComponent<BuildingLevel>();

        airplaneREF = Instantiate(airplane, point1.position, Quaternion.identity);
        airplaneREF.transform.SetParent(gameObject.transform);
        airplaneScript = airplaneREF.GetComponent<Airplane>();

        // assign ref to airplane
        airplaneScript.airport = gameObject;
        airplaneScript.airportScript = GetComponent<Airport>();
        airplaneScript.airport_point1 = point1;
        airplaneScript.airport_point2 = point2;

        popupStorageCanvas = GameObject.FindGameObjectWithTag("StorageCanvas");

        StartCoroutine(Pollution_AUTOMATIC(pollutionInterval_auto));
    }

    private void Update()
    {
        // DEBUG
        if(airportPopUpREF)
        {
            airportPopUpREF.GetComponent<AirportPopUp>().airportREF = gameObject;
            airportPopUpREF.GetComponent<AirportPopUp>().airportREF_script = GetComponent<Airport>();
        }

        // Auto production & pollution
        pollutionProduced_auto = pollutionProduced_auto_initial + (buildingLevel.level - 1) * 0.02f;

    }
    public IEnumerator Pollution_AUTOMATIC(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Pollution.POLLUTION += pollutionProduced_auto;
        }
    }

    public void SpawnPopUp()
    {
        //airportPopUpREF = Instantiate(airportPopUp, transform.position, Quaternion.identity);
        //airportPopUpREF.GetComponent<AirportPopUp>().airportREF = gameObject;
        //airportPopUpREF.transform.SetParent(popupStorageCanvas.transform);

        GameObject temp = Instantiate(airportPopUp, transform.position, Quaternion.identity);
        temp.GetComponent<AirportPopUp>().airportREF = gameObject;
        temp.transform.SetParent(popupStorageCanvas.transform);
        airportPopUpREF = temp;
    }

    //public void OnMouseOver()
    //{
    //    if(Input.GetMouseButtonDown(0) && airportPopUpREF == null)
    //    {
    //        GameObject temp = Instantiate(airportPopUp, transform.position, Quaternion.identity);
    //        temp.GetComponent<AirportPopUp>().airportREF = gameObject;
    //        temp.transform.SetParent(popupStorageCanvas.transform);
    //        airportPopUpREF = temp;
    //    }
    //}
}
