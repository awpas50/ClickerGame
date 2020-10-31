using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    private float randomX;
    private float randomY;
    private float randomZ;
    public GameObject asteroid;

    public int asteroidCount = 0;
    float val;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        randomX = Random.Range(-6f, 6f);
        randomY = Random.Range(15f, 20f);
        randomZ = Random.Range(-6f, 6f);

        if(Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(Spawn_DEBUG());
        }

        
        if(Pollution.POLLUTION == 0)
        {
            asteroidCount = 0;
            val = 1;
        }
        else
        {
           val = Pollution.POLLUTION;
        }
        if(val - 15 >= 0)
        {
            asteroidCount = (int)Mathf.Sqrt(val - 15);
        }
        else
        {
            asteroidCount = 0;
        }
        
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(40f);
            AudioManager.instance.Play(SoundList.AsteroidPassBy);
            // A formula to control the asteroid number
            if (Pollution.POLLUTION <= 15)
            {
                asteroidCount = 0;
            }
            if (asteroidCount > 16)
            {
                asteroidCount = 16;
            }
            //yield return null;
            for (int i = 0; i < asteroidCount; i++)
            {
                Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(0.3f, 2f));
            }
        }
    }

    IEnumerator Spawn_DEBUG()
    {
        // A formula to control the asteroid number
        if (asteroidCount > 16)
        {
            asteroidCount = 16;
        }
        AudioManager.instance.Play(SoundList.AsteroidPassBy);
        //yield return null;
        for (int i = 0; i < asteroidCount; i++)
        {
            Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.3f, 2f));
        }
        StopCoroutine(Spawn_DEBUG());
    }
}
