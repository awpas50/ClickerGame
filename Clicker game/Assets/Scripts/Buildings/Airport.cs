using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airport : MonoBehaviour
{
    [Header("Airplane")]
    public GameObject airplane;
    [Header("Do not edit")]
    public Airplane airplaneScript;
    public GameObject airplaneREF;
    [Header("Airport landing point")]
    public Transform point1;
    public Transform point2;
    [Header("Auto generated pollution")]
    public float pollutionProduced_auto;
    public float pollutionInterval_auto;

    [HideInInspector] public BuildingLevel buildingLevel;

    void Start()
    {
        // random point 2 (high point) location
        point2.position = new Vector3(point2.position.x, Random.Range(4.5f, 7f), point2.position.z);
        buildingLevel = GetComponent<BuildingLevel>();

        airplaneREF = Instantiate(airplane, point1.position, Quaternion.identity);
        airplaneREF.transform.SetParent(gameObject.transform);
        airplaneScript = airplaneREF.GetComponent<Airplane>();
        airplaneScript.airportScriptREF = GetComponent<Airport>();
        airplaneScript.airport_point1 = point1;
        airplaneScript.airport_point2 = point2;
        StartCoroutine(Pollution_AUTOMATIC(pollutionProduced_auto, pollutionInterval_auto));
    }

    public IEnumerator Pollution_AUTOMATIC(float pollutionProduced, float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Pollution.POLLUTION += pollutionProduced;
        }
    }
}
