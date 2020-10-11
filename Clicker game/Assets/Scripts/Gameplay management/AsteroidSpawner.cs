using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroid;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(10f);
            if (Pollution.POLLUTION >= 0 && Pollution.POLLUTION < 20)
            {
                for (int i = 0; i < 1; i++)
                {
                    Instantiate(asteroid, transform.position, Quaternion.identity);
                }
            }
            if (Pollution.POLLUTION >= 20 && Pollution.POLLUTION < 40)
            {
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(asteroid, transform.position, Quaternion.identity);
                }
            }
            if (Pollution.POLLUTION >= 40 && Pollution.POLLUTION < 60)
            {
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(asteroid, transform.position, Quaternion.identity);
                }
            }
            if (Pollution.POLLUTION >= 60 && Pollution.POLLUTION < 80)
            {
                for (int i = 0; i < 9; i++)
                {
                    Instantiate(asteroid, transform.position, Quaternion.identity);
                }
            }
            if (Pollution.POLLUTION >= 80 && Pollution.POLLUTION < 100)
            {
                for (int i = 0; i < 13; i++)
                {
                    Instantiate(asteroid, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
