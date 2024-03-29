﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private LineRenderer lr;
    private BuildingLevel buildingLevel;
    private BuildingState buildingState;
    private BuildingBuff buildingBuff;
    [Header("Bullet props")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    
    [Header("Turret props")]
    public GameObject cannonBase;
    public GameObject barrel;
    public float turnSpeed = 10f;
    public string enemyTag = "Asteroid";
    public float range;
    [Header("Target")]
    private GameObject[] asteroids;
    private List<GameObject> asteroids_list;
    [SerializeField] private Transform target;
    [SerializeField] private bool targetLocked = false;
    [Header("Shooting")]
    private float fireRate_initial;
    public float fireRate_real = 0.35f;
    [HideInInspector] public float fireRate_original;
    [HideInInspector] public float fireRate_additional;
    public float fireCountdown = 0f;
    [Header("Efficiency")]
    public float efficiency;
    [Header("Pollution")]
    [SerializeField] private float pollutionProduced_auto;
    [HideInInspector] public float pollutionProduced_auto_base;
    [HideInInspector] public float pollutionProduced_auto_extra;
    public float pollutionInterval_auto;
    public float pollutionProduced_auto_initial;

    [Header("Initial freeze")]
    [SerializeField] private float initialFreeze = 0.3f;
    public bool canFreeze = true;

    public enum State
    {
        Idle,
        Attack,
        ModelFreezed
    }
    public State state;

    [Header("cannonBase animation")]
    private float angleY_random;
    private float angleY_current;

    private void Awake()
    {
        StartCoroutine(FreezeTurret());
    }
    void Start()
    {
        lr = GetComponent<LineRenderer>();

        fireRate_initial = fireRate_real;
        buildingState = GetComponent<BuildingState>();
        buildingLevel = GetComponent<BuildingLevel>();
        buildingBuff = GetComponent<BuildingBuff>();
        //InvokeRepeating("UpdateTarget", 0f, 0.15f);
        StartCoroutine(RandomAngleY());
        angleY_current = angleY_random;
        fireCountdown = 1f / fireRate_real;

        pollutionProduced_auto_initial = pollutionProduced_auto;
        StartCoroutine(Pollution_AUTOMATIC(pollutionInterval_auto));
    }

    void Update()
    {
        // Efficiency
        // Each house nearby grants 5% efficiency (+1% every level)
        // houseEfficiencyTotal will be multiplied by 0.2 as the original efficiency of a level 1 house is 25%. Multiply by 0.2 makes it 5%.
        efficiency = 1 + buildingBuff.houseEfficiencyTotal * 0.2f + buildingBuff.nearbyMainBuilding * Objective.townHallEfficiency * 0.2f;
        // Pollution
        // Auto production & pollution
        // Building base + 0.04 per level
        pollutionProduced_auto_base = pollutionProduced_auto_initial + (buildingLevel.level - 1) * 0.04f;
        // Stack up pollution according to nearby houses
        pollutionProduced_auto_extra = buildingBuff.houseEfficiencyTotal * 0.2f + buildingBuff.nearbyMainBuilding * Objective.townHallEfficiency * 0.2f;
        // Total
        pollutionProduced_auto = pollutionProduced_auto_base + pollutionProduced_auto_extra;
        // Level
        fireRate_real = (fireRate_initial + (buildingLevel.level - 1) * 0.1f ) * efficiency;
        fireRate_original = fireRate_initial + (buildingLevel.level - 1) * 0.1f;
        fireRate_additional = (fireRate_initial + (buildingLevel.level - 1) * 0.1f) * (efficiency - 1);

        if (canFreeze && target != null)
        {
            state = State.Attack;
        }
        if (canFreeze && target == null)
        {
            state = State.ModelFreezed;
        }
        if (!canFreeze && target != null)
        {
            state = State.Attack;
        }
        if (!canFreeze && target == null)
        {
            state = State.Idle;
        }

        fireCountdown -= Time.deltaTime;

        if(state == State.ModelFreezed)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, transform.position);

            targetLocked = false;
        }
        if (state == State.Idle)
        {
            cannonBase.transform.eulerAngles = new Vector3(cannonBase.transform.rotation.x,
                angleY_current,
                cannonBase.transform.rotation.z);

            angleY_current = Mathf.Lerp(angleY_current, angleY_random, Time.deltaTime * turnSpeed);

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, transform.position);

            targetLocked = false;
        }
        if (state == State.Attack)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, target.transform.position);

            // only turn around if it has a target
            //Vector3 dir = target.position - transform.position;
            //Quaternion lookRotation = Quaternion.LookRotation(dir);
            //Vector3 rotation = Quaternion.Lerp(cannonBase.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            //cannonBase.transform.rotation = Quaternion.Euler(0, rotation.y, 0);
            
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate_real;
            }
        }

        // Update asteroid list
        asteroids = GameObject.FindGameObjectsWithTag(enemyTag);
        if (asteroids.Length == 0)
        {
            target = null;
            return;
        }
        if (asteroids.Length > 0)
        {
            asteroids_list = new List<GameObject>();
            // Array --> list
            for (int i = 0; i < asteroids.Length; i++)
            {
                asteroids_list.Add(asteroids[i]);
            }
            // Remove unnecessary
            for (int i = 0; i < asteroids_list.Count; i++)
            {
                if (asteroids_list[i].GetComponent<Asteroid>().isLockedByTurret)
                {
                    asteroids_list[i] = null;
                }
            }
            if (state == State.Idle || state == State.ModelFreezed)
                UpdateTarget();
        }
    }

    public IEnumerator Pollution_AUTOMATIC(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Pollution.POLLUTION += pollutionProduced_auto;
        }
    }

    void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;
        
        // find out which asteroid is the nearest.
        foreach (GameObject asteroid in asteroids_list)
        {
            if(asteroid == null)
            {
                continue;
            }
            Vector3 posTurret = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 posAsteroid = new Vector3(asteroid.transform.position.x, 0, asteroid.transform.position.z);

            float distanceToAsteroid = Vector3.Distance(posTurret, posAsteroid);
            //if (distanceToAsteroid < shortestDistance && distanceToAsteroid < range)
            if (distanceToAsteroid < shortestDistance)
            {
                if(!asteroid.GetComponent<Asteroid>().isLockedByTurret)
                {
                    shortestDistance = distanceToAsteroid;
                    nearestTarget = asteroid;
                    nearestTarget.GetComponent<Asteroid>().isLockedByTurret = true;
                    nearestTarget.GetComponent<Asteroid>().targetTurret = gameObject;
                }
                else if(asteroid.GetComponent<Asteroid>().isLockedByTurret)
                {
                    UpdateTarget();
                }
            }
        }
        if (nearestTarget != null)
        {
            //Lock Target
            if (!targetLocked)
            {
                target = nearestTarget.transform;
                
                targetLocked = true;
            }
        }
        else if(nearestTarget == null)
        {
            target = null;
        }
    }

    void Shoot()
    {
        AudioManager.instance.Play(SoundList.TurretShoot);
        GameObject bulletPrefab = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bulletPrefab.GetComponent<Bullet>();

        if(bullet != null)
        {
            bulletScript.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    IEnumerator RandomAngleY()
    {
        while(true)
        {
            if(state == State.Idle)
            {
                angleY_random = Random.Range(angleY_random - 50, angleY_random + 50);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    public IEnumerator FreezeTurret()
    {
        canFreeze = true;
        yield return new WaitForSeconds(initialFreeze);
        canFreeze = false;
    }
}
