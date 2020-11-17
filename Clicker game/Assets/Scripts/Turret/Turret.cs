using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
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
    public bool isLocked = false;
    [Header("Shooting")]
    private float fireRate_initial;
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
        fireRate_initial = fireRate;
        buildingState = GetComponent<BuildingState>();
        buildingLevel = GetComponent<BuildingLevel>();
        InvokeRepeating("UpdateTarget", 0f, 0.15f);
        StartCoroutine(RandomAngleY());
        angleY_current = angleY_random;
    }
    
    void Update()
    {
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

            angleY_current = Mathf.Lerp(angleY_current, angleY_random, Time.deltaTime * 5);
        }
        if (state == State.Attack)
        {
            // only turn around if it has a target
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
        isLocked = false;
        asteroids = GameObject.FindGameObjectsWithTag(enemyTag);
        if(asteroids.Length == 0)
        {
            return;
        }
        targetID = Random.Range(0, asteroids.Length);
        if(!isLocked)
        {
            target = asteroids[targetID].transform;
            if (asteroids[targetID].GetComponent<Asteroid>().isLockedByTurret)
            {
                targetID = Random.Range(0, asteroids.Length);
            }
            else
            {
                asteroids[targetID].GetComponent<Asteroid>().isLockedByTurret = true;
                isLocked = true;
            }
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
            angleY_random = Random.Range(angleY_random - 200, angleY_random + 200);
            yield return new WaitForSeconds(3f);
        }
    }
}
