using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 3f;
    public GameObject[] targetList;
    public GameObject target;
    public GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        targetList = GameObject.FindGameObjectsWithTag("Node");
        target = targetList[Random.Range(0, targetList.Length)];
        transform.LookAt(target.transform);
        speed = Random.Range(2f, 4.5f);
    }
    
    void Update()
    {
        //transform.position += Vector3.down * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        Destroy(gameObject, 15f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "House" || collision.collider.gameObject.tag == "Factory" || collision.collider.gameObject.tag == "Park")
        {
            Destroy(gameObject);
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "House" || other.gameObject.tag == "Factory" || other.gameObject.tag == "Park")
        {
            Destroy(gameObject);
            GameObject effectPrefab = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(effectPrefab, 5f);
        }
    }
}
