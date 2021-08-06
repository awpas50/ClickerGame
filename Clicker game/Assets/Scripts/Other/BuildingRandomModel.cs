using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRandomModel : MonoBehaviour
{
    public int seed = -1;
    public GameObject model1;
    public GameObject model2;

    void Awake()
    {
        seed = Random.Range(0, 2);
    }
    
    void Update()
    {
        if(seed == 0)
        {
            model1.SetActive(true);
            model2.SetActive(false);
        }
        else if(seed == 1)
        {
            model1.SetActive(false);
            model2.SetActive(true);
        }
    }
}
