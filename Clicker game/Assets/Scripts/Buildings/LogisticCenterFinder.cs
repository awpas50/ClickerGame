using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogisticCenterFinder : MonoBehaviour
{
    [HideInInspector] public LogisticCenter logisticCenter;


    private void Update()
    {
        if(!logisticCenter)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Factory"))
        {
            if (other.gameObject.GetComponent<Factory>().factoryPopUpREF)
                other.gameObject.GetComponent<Factory>().factoryPopUpREF.
                    GetComponent<FactoryPopUp>().ButtonEvent();
            else
                return;
        }
        else if (other.gameObject.tag == "Park")
        {
            if (other.gameObject.GetComponent<Park>().parkPopUpREF)
                other.gameObject.GetComponent<Park>().parkPopUpREF.
                    GetComponent<ParkPopUp>().ButtonEvent();
            else
                return;
        }
        else if (other.gameObject.tag == "Airport")
        {
            try
            {
                if (other.gameObject.GetComponent<Airport>().airportPopUpREF)
                    other.gameObject.GetComponent<Airport>().airportPopUpREF.
                        GetComponent<AirportPopUp>().ButtonEvent();
            }
            catch
            {
                throw new MissingReferenceException();
            }
        }
        else if (other.gameObject.tag == "MainBuilding")
        {
            // Logistic center x4 buff
            other.gameObject.GetComponent<MainBuilding>().MainBuildingClickEvent_LogisticCenterBuff();
        }
        // not buildings
        else
        {
            return;
        }
    }
}
