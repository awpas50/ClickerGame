using System.Collections;
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
        efficiency = 1 + buildingBuff.houseEfficiencyTotal * 0.2f;
        // Pollution
        // Auto production & pollution
        pollutionProduced_auto = pollutionProduced_auto_initial + (buildingLevel.level - 1) * 0.04f;

        // Level
        fireRate_real = (fireRate_initial + (buildingLevel.level - 1) * 0.15f ) * efficiency;
        fireRate_original = fireRate_initial + (buildingLevel.level - 1) * 0.15f;
        fireRate_additional = (fireRate_initial + (buildingLevel.level - 1) * 0.15f) * (efficiency - 1);
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
            UpdateTarget();

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
                fireCountdown = 1f / fireRate_real;
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
        if(asteroids.Length == 0)
        {
            target = null;
            return;
        }
        List<GameObject> asteroids_list = new List<GameObject>();
        // Array --> list
        for (int i = 0; i < asteroids.Length; i++)
        {
            asteroids_list.Add(asteroids[i]);
        }
        // Remove unnecessary
        for (int i = 0; i < asteroids_list.Count; i++)
        {
            if(asteroids_list[i].GetComponent<Asteroid>().isLockedByTurret)
            {
                asteroids_list[i] = null;
            }
        }
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
            //Debug.Log(distanceToAsteroid);
            //if (distanceToAsteroid < shortestDistance && distanceToAsteroid < range)
            if (distanceToAsteroid < shortestDistance)
            {
                if(!asteroid.GetComponent<Asteroid>().isLockedByTurret)
                {
                    shortestDistance = distanceToAsteroid;
                    nearestTarget = asteroid;
                    nearestTarget.GetComponent<Asteroid>().isLockedByTurret = true;
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
            angleY_random = Random.Range(angleY_random - 50, angleY_random + 50);
            yield return new WaitForSeconds(3f);
        }
    }
}
