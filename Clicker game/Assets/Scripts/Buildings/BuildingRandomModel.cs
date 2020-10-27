using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRandomModel : MonoBehaviour
{
    public int seed = -1;
    public GameObject model1;
    public GameObject model2;
    // Start is called before the first frame update
    void Start()
    {
        seed = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(seed == 0)
        {
            model1.SetActive(true);
            model2.SetActive(false);
        }
        else
        {
            model1.SetActive(false);
            model2.SetActive(true);
        }
    }
}
