using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    private float randomX;
    private float randomY;
    private float randomZ;
    public GameObject asteroid;
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
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(45f);
            if (Pollution.POLLUTION >= 0 && Pollution.POLLUTION < 10)
            {
                yield return null;
            }
            if (Pollution.POLLUTION >= 10 && Pollution.POLLUTION < 20)
            {
                for (int i = 0; i < 1; i++)
                {
                    Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                    yield return new WaitForSeconds(0.4f);
                }
            }
            if (Pollution.POLLUTION >= 20 && Pollution.POLLUTION < 40)
            {
                for (int i = 0; i < 1; i++)
                {
                    Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                    yield return new WaitForSeconds(0.4f);
                }
            }
            if (Pollution.POLLUTION >= 40 && Pollution.POLLUTION < 60)
            {
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                    yield return new WaitForSeconds(0.4f);
                }
            }
            if (Pollution.POLLUTION >= 60 && Pollution.POLLUTION < 80)
            {
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                    yield return new WaitForSeconds(0.4f);
                }
            }
            if (Pollution.POLLUTION >= 80 && Pollution.POLLUTION < 100)
            {
                for (int i = 0; i < 7; i++)
                {
                    Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                    yield return new WaitForSeconds(0.4f);
                }
            }
        }
    }

    IEnumerator Spawn_DEBUG()
    {
        if (Pollution.POLLUTION >= 0 && Pollution.POLLUTION < 10)
        {
            yield return null;
        }
        if (Pollution.POLLUTION >= 10 && Pollution.POLLUTION < 20)
        {
            for (int i = 0; i < 1; i++)
            {
                Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                yield return new WaitForSeconds(0.4f);
            }
        }
        if (Pollution.POLLUTION >= 20 && Pollution.POLLUTION < 40)
        {
            for (int i = 0; i < 1; i++)
            {
                Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                yield return new WaitForSeconds(0.4f);
            }
        }
        if (Pollution.POLLUTION >= 40 && Pollution.POLLUTION < 60)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                yield return new WaitForSeconds(0.4f);
            }
        }
        if (Pollution.POLLUTION >= 60 && Pollution.POLLUTION < 80)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                yield return new WaitForSeconds(0.4f);
            }
        }
        if (Pollution.POLLUTION >= 80 && Pollution.POLLUTION < 100)
        {
            for (int i = 0; i < 7; i++)
            {
                Instantiate(asteroid, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                yield return new WaitForSeconds(0.4f);
            }
        }
        StopCoroutine(Spawn_DEBUG());
    }
}
