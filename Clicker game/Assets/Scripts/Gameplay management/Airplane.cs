using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    public float waitTime_base;
    public float waitTime_des;
    private float waitTIme_des_initial;

    public float relativeSpeed;
    public float relativeSpeed_initial;

    public GameObject[] destinations;
    public int randomDestinationIndex;
    // triggers
    [HideInInspector] public bool reachedHighPoint = false;
    private bool isPopUpSpawned = false;
    [HideInInspector] public bool redeparture = false;

    private float t1 = 0;
    private float t2 = 0;
    [Header("Assigned in script")]
    public Transform airport_point1;
    public Transform airport_point2;
    [Header("What to instantiate")]
    public GameObject factoryPopUp;
    // assigned automatically
    [HideInInspector] public GameObject airport;
    [HideInInspector] public Airport airportScript;

    public enum State
    {
        Idle,
        Departure,
        Arrival
    }
    public State state;
    void Start()
    {
        waitTIme_des_initial = waitTime_des;
        relativeSpeed_initial = relativeSpeed;
        destinations = GameObject.FindGameObjectsWithTag("Destinations");

        StartCoroutine(WaitAtApron(waitTime_base));
    }

    // Update is called once per frame
    void Update()
    {
        waitTime_des = waitTIme_des_initial + (airportScript.buildingLevel.level - 1) * -10f;
        if(GameManager.i.isPaused)
        {
            relativeSpeed = 0;
        }
        else
        {
            relativeSpeed = relativeSpeed_initial + (airportScript.buildingLevel.level - 1) * 0.015f;
        }
        

        if(redeparture)
        {
            ReachedBaseEvent();
        }
        if (state == State.Idle)
        {
            return;
        }
        else if(state == State.Departure)
        {
            // high point = point 2
            if(!reachedHighPoint)
            {
                transform.LookAt(airport_point2);
                transform.position = Vector3.MoveTowards(transform.position, airport_point2.position, relativeSpeed);
                randomDestinationIndex = Random.Range(0, destinations.Length);
            }
            if(reachedHighPoint)
            {
                transform.LookAt(destinations[randomDestinationIndex].transform);
                transform.position = Vector3.MoveTowards(transform.position, destinations[randomDestinationIndex].transform.position, relativeSpeed);
            }
            if (Vector3.Distance(transform.position, airport_point2.position) <= 0.1f)
            {
                reachedHighPoint = true;
                isPopUpSpawned = false;
            }
            if (Vector3.Distance(transform.position, destinations[randomDestinationIndex].transform.position) <= 0.1f) {
                ReachedDestinationEvent();
            }
        }
        else if (state == State.Arrival)
        {
            if(!reachedHighPoint)
            {
                transform.LookAt(airport_point2);
                transform.position = Vector3.MoveTowards(transform.position, airport_point2.transform.position, relativeSpeed);
            }
            if (reachedHighPoint)
            {
                transform.LookAt(airport_point1);
                transform.position = Vector3.MoveTowards(transform.position, airport_point1.transform.position, relativeSpeed);
            }
            if (Vector3.Distance(transform.position, airport_point2.transform.position) <= 0.1f)
            {
                reachedHighPoint = true;
            }
            if (Vector3.Distance(transform.position, airport_point1.transform.position) <= 0.1f)
            {
                transform.eulerAngles = new Vector3(0, transform.rotation.y, transform.rotation.z);
                if(!isPopUpSpawned)
                {
                    airportScript.SpawnPopUp();
                    isPopUpSpawned = true;
                }
            }
        }
    }

    IEnumerator WaitAtApron(float time)
    {
        state = State.Idle;
        yield return new WaitForSeconds(time);
        state = State.Departure;
    }

    void ReachedDestinationEvent()
    {
        // arrived
        t1 += Time.deltaTime;
        if (t1 >= waitTime_des)
        {
            t1 = 0;
            reachedHighPoint = false;
            // go back to the base.
            state = State.Arrival;
        }
    }

    void ReachedBaseEvent()
    {
        // arrived back the base
        t2 += Time.deltaTime;
        if (t2 >= waitTime_base)
        {
            t2 = 0;
            reachedHighPoint = false;
            // leave the base.
            state = State.Departure;
            redeparture = false;
        }
    }
}
