using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
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
    public Transform target;
    [Header("Shooting")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;

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
        buildingState = GetComponent<BuildingState>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        StartCoroutine(RandomAngleY());
        angleY_current = angleY_random;
    }
    
    void Update()
    {
        if(target == null)
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

            angleY_current = Mathf.Lerp(angleY_current, angleY_random, Time.deltaTime * 5);
        }
        if (state == State.Attack)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(cannonBase.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            cannonBase.transform.rotation = Quaternion.Euler(0, rotation.y, 0);

            fireCountdown -= Time.deltaTime;
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            
        }
    }

    void UpdateTarget()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestAsteroid = null;
        foreach(GameObject asteroid in asteroids)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, asteroid.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestAsteroid = asteroid;
            }
        }

        if(nearestAsteroid != null && shortestDistance <= range)
        {
            target = nearestAsteroid.transform;
        }
    }
    void Shoot()
    {
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
            angleY_random = Random.Range(angleY_random - 200, angleY_random + 200);
            yield return new WaitForSeconds(5f);
        }
    }
}
