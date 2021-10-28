using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpinner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidObject;
    [SerializeField] private GameObject asteroidVFX;
    void Start()
    {
        float randomScaleVal = Random.Range(0.2f, 0.3f);
        asteroidObject.transform.localScale = new Vector3(randomScaleVal, randomScaleVal, randomScaleVal);
        asteroidVFX.transform.localScale = new Vector3(randomScaleVal * 4f * 1.2f, randomScaleVal * 4f * 1.2f, randomScaleVal * 4f * 1.2f);
    }
    
    void Update()
    {
        asteroidObject.transform.Rotate(new Vector3(Random.Range(0.25f, 0.75f), Random.Range(0.25f, 0.75f), Random.Range(0.25f, 0.75f)));
        
    }
}
