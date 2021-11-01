using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    [SerializeField] private float waitTime_base;
    public float waitTime_des;
    public float waitTime_des_actial;
    [SerializeField] private float waitTime_des_initial;

    public float relativeSpeed;
    [SerializeField] private float relativeSpeed_initial;

    public GameObject[] destinations;
    public int randomDestinationIndex;
    // triggers
    public bool reachedHighPoint = false;
    public bool isPopUpSpawned = false;
    public bool redeparture = false;
    public bool isFinishedFirstTimeWaiting = false;

    public float t1 = 0;
    public float t2 = 0;
    public float t3 = 0;
    [Header("Assigned in script")]
    public Transform airport_point1;
    public Transform airport_point2;
    [Header("What to instantiate")]
    public GameObject factoryPopUp;
    // assigned automatically
    [HideInInspector] public GameObject airport;
    [HideInInspector] public Airport airportScript;

    [Header("Resources multiplier")]
    public float efficiency = 1f;

    public enum State
    {
        Idle,
        Departure,
        Arrival
    }
    public State state;
    void Start()
    {
        waitTime_des = waitTime_des_actial;
        waitTime_des_initial = waitTime_des_actial;
        relativeSpeed_initial = relativeSpeed;
        destinations = GameObject.FindGameObjectsWithTag("Destinations");
    }

    // Update is called once per frame
    void Update()
    {
        efficiency = 1 - ((airportScript.buildingBuff.houseEfficiencyTotal + airportScript.buildingBuff.nearbyMainBuilding * Objective.townHallEfficiency) * 0.2f);
        waitTime_des = (waitTime_des_initial + (airportScript.buildingLevel.level - 1) * -10f);
        waitTime_des_actial = (waitTime_des_initial + (airportScript.buildingLevel.level - 1) * -10f) * efficiency;
        // I don't know why the airplanes are still moving even if the game is paused, so I manually stop their movement by writing this.
        if(GameManager.i.isPaused || !GameManager.i.canInput)
        {
            relativeSpeed = 0;
        }
        else
        {
            relativeSpeed = relativeSpeed_initial + (airportScript.buildingLevel.level - 1) * 0.015f;
        }
        
        
        if (redeparture)
        {
            ReachedBaseEvent();
        }

        if (state == State.Idle)
        {
            if (!isFinishedFirstTimeWaiting)
            {
                t3 += Time.deltaTime;
            }
            if (t3 >= waitTime_base && !isFinishedFirstTimeWaiting)
            {
                t3 = 0;
                state = State.Departure;
                isFinishedFirstTimeWaiting = true;
            }
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
                // For save system Debug:
                if (isPopUpSpawned && !redeparture && airportScript.airportPopUpREF == null)
                {
                    airportScript.SpawnPopUp();
                }
            }
            
        }
    }

    //void WaitAtApron()
    //{
    //    t3 += Time.deltaTime;
    //    if (t3 >= waitTime_base && !isFinishedFirstTimeWaiting)
    //    {
    //        t3 = 0;
    //        state = State.Departure;
    //        isFinishedFirstTimeWaiting = true;
    //    }
    //}

    void ReachedDestinationEvent()
    {
        // arrived
        t1 += Time.deltaTime;
        if (t1 >= waitTime_des_actial)
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
