using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public static AsteroidSpawner i;
    // This number will increments by 1 each time when an asteroid is spawned.
    public int asteroidID = 0;
    private float randomX;
    private float randomY;
    private float randomZ;
    public GameObject asteroid;

    public int asteroidCount = 0;
    public float val;

    [Header("Time elapsed")]
    public float timeElapsed;

    void Awake()
    {
        //debug
        if (i != null)
        {
            Debug.LogError("More than one AsteroidSpawner in scene");
            return;
        }
        i = this;
    }
    void Start()
    {
        StartCoroutine(Spawn());
        StartCoroutine(Spawn_Auto());
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        randomX = Random.Range(-6f, 6f);
        randomY = Random.Range(15f, 20f);
        randomZ = Random.Range(-6f, 6f);

        if (Input.GetKeyDown(KeyCode.J))
        {
            SpecialBuildingCount.platform1Count += 1;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Currency.MONEY += 1000;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(Spawn_DEBUG());
        }


        val = Pollution.POLLUTION;

        // A formula to control the asteroid number

        if (Pollution.POLLUTION <= 15)
        {
            asteroidCount = 0;
        }
        else if(Pollution.POLLUTION >= 15 && Pollution.POLLUTION <= 500)
        {
            asteroidCount = (int)(val / 50);
        }
        else if (Pollution.POLLUTION > 500 && Pollution.POLLUTION <= 2100)
        {
            asteroidCount = (int)(10 + Mathf.Floor((val - 500) / 80));
        }
        else if (Pollution.POLLUTION > 2100 && Pollution.POLLUTION <= 4500)
        {
            asteroidCount = (int)(30 + Mathf.Floor((val - 2100) / 120));
        }
        else if(Pollution.POLLUTION > 4500)
        {
            asteroidCount = 50;
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(38f);
            if (asteroidCount >= 1)
            {
                AudioManager.instance.Play(SoundList.AsteroidPassBy);
            }
            yield return new WaitForSeconds(2f);

            
            //yield return null;
            
            for (int i = 0; i < asteroidCount; i++)
            {
                GameObject temp = Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                temp.GetComponent<Asteroid>().uniqueID = asteroidID;
                asteroidID++;
                temp.GetComponent<Asteroid>().speed = Random.Range(asteroid.GetComponent<Asteroid>().speedMin, asteroid.GetComponent<Asteroid>().speedMax);
                yield return new WaitForSeconds(Random.Range(0.3f, 1.3f));
            }
        }
    }
    IEnumerator Spawn_Auto()
    {
        yield return new WaitForSeconds(58f);
        AudioManager.instance.Play(SoundList.AsteroidPassBy);
        yield return new WaitForSeconds(2f);
        while (true)
        {
            for (int i = 0; i < Objective.townHallLevel; i++)
            {
                GameObject temp = Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                temp.GetComponent<Asteroid>().uniqueID = asteroidID;
                asteroidID++;
                temp.GetComponent<Asteroid>().speed = 6f;
                yield return new WaitForSeconds(Random.Range(0.3f, 1.3f));
            }
            yield return new WaitForSeconds(40f);
            AudioManager.instance.Play(SoundList.AsteroidPassBy);
        }
    }
    public IEnumerator Spawn_DEBUG()
    {
        // A formula to control the asteroid number
        if (asteroidCount > 40)
        {
            asteroidCount = 40;
        }
        //AudioManager.instance.Play(SoundList.AsteroidPassBy);
        asteroidID++;
        GameObject temp = Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
        temp.GetComponent<Asteroid>().uniqueID = asteroidID;
        temp.GetComponent<Asteroid>().speed = Random.Range(asteroid.GetComponent<Asteroid>().speedMin, asteroid.GetComponent<Asteroid>().speedMax);
        StopCoroutine(Spawn_DEBUG());
        yield return null;
    }
}
