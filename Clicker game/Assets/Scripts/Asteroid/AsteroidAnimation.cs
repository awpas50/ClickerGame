using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidAnimation : MonoBehaviour
{
    Asteroid asteroidScript;
    [HideInInspector] public float initialSpeed;
    public float speed = 5;
    private int seed = 0;

    private void Start()
    {
        initialSpeed = speed;
    }
    void Update()
    {
        float rndX = Random.Range(1f, 3f);
        float rndY = Random.Range(1f, 3f);
        float rndZ = Random.Range(1f, 3f);

        transform.Rotate(speed * rndX * Time.deltaTime, speed * rndY * Time.deltaTime, speed * rndZ * Time.deltaTime);
    }
}
