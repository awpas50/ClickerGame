using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    LineRenderer lr;
    BuildingLevel buildingLevel;
    BuildingState buildingState;
    [Header("Bullet props")]
    public GameObject bullet;
    public Transform firePoint;

    [Header("Turret props")]
    public GameObject cannonBase;
    public GameObject barrel;
    public float turnSpeed = 10f;
    public string enemyTag = "Asteroid";
    public float range;
    [Header("Target")]
    public GameObject[] asteroids;
    public int targetID;
    public Transform target;
    public bool targetLocked = false;
    [Header("Shooting")]
    private float fireRate_initial;
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    [Header("Pollution")]
    public float pollutionProduced_auto;
    public float pollutionInterval_auto;
    public float pollutionProduced_auto_initial;

    
    public enum State
    {
        Idle,
        Attack
    }
    public State state;

    [Header("cannonBase animation")]
    private float angleY_random;
    private float angleY_current;

    void Start()
    {
        lr = GetComponent<LineRenderer>();

        fireRate_initial = fireRate;
        buildingState = GetComponent<BuildingState>();
        buildingLevel = GetComponent<BuildingLevel>();
        InvokeRepeating("UpdateTarget", 0f, 0.15f);
        StartCoroutine(RandomAngleY());
        angleY_current = angleY_random;

        pollutionProduced_auto_initial = pollutionProduced_auto;
        StartCoroutine(Pollution_AUTOMATIC(pollutionInterval_auto));
    }
    
    void Update()
    {
        // Pollution
        // Auto production & pollution
        pollutionProduced_auto = pollutionProduced_auto_initial + (buildingLevel.level - 1) * 0.04f;

        // Level
        fireRate = fireRate_initial + ((buildingLevel.level - 1) * 0.15f);
        if (target == null)
        {
            state = State.Idle;
        }
        if (target != null)
        {
            state = State.Attack;
        }
        
        if(state == State.Idle)
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

            fireCountdown -= Time.deltaTime;
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            
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
        asteroids = GameObject.FindGameObjectsWithTag(enemyTag);
        // find out which enemy is the nearest.
        foreach (GameObject asteroid in asteroids)
        {
            Vector3 posTurret = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 posAsteroid = new Vector3(asteroid.transform.position.x, 0, asteroid.transform.position.z);

            float distanceToAsteroid = Vector3.Distance(posTurret, posAsteroid);
            Debug.Log(distanceToAsteroid);
            //if (distanceToAsteroid < shortestDistance && distanceToAsteroid < range)
            if (distanceToAsteroid < shortestDistance)
            {
                shortestDistance = distanceToAsteroid;
                nearestTarget = asteroid;
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
        else
        {
            target = null;
        }
    }

    //void ConfirmTarget()
    //{
    //    Debug.Log("ConfirmTarget");
    //    target = asteroids[targetID].transform;
    //    asteroids[targetID].GetComponent<Asteroid>().isLockedByTurret = true;
    //    hasTarget = true;
    //}

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
            angleY_random = Random.Range(angleY_random - 50, angleY_random + 50);
            yield return new WaitForSeconds(3f);
        }
    }
}
