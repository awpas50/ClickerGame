using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    public float relativeSpeed = 0.1f;
    public GameObject[] destinations;
    public int index;

    [Header("What to instantiate")]
    public GameObject factoryPopUp;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(destinations[index + 1].transform);
        transform.position = Vector3.MoveTowards(transform.position, destinations[index + 1].transform.position, relativeSpeed);

        if (Vector3.Distance(transform.position, destinations[0].transform.position) <= 0.3f)
        {
            StartCoroutine(Stop());
        }
        if (Vector3.Distance(transform.position, destinations[index + 1].transform.position) <= 0.3f)
        {
            if (index == destinations.Length - 2)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }

    IEnumerator Stop()
    {
        bool isSpawned = false;
        relativeSpeed = 0f;
        yield return new WaitForSeconds(4f);
        //if(!isSpawned)
        //{
        //    Instantiate(factoryPopUp, transform.position, Quaternion.identity);
        //    Currency.MONEY += 10;
        //    isSpawned = true;
        //}
        relativeSpeed = 0.1f;
        StopCoroutine(Stop());

        
    }
}
