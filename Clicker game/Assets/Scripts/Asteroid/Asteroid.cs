using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float speed;
    public float speedMin = 5f;
    public float speedMax = 10f;
    public GameObject[] targetList;
    public GameObject target;
    public GameObject explosionEffect;
    public ParticleSystem particleEffect1;
    public ParticleSystem particleEffect2;

    public bool isLockedByTurret = false;
    public bool reachedDestination = false;

    [Header("Building Ruins")]
    public GameObject ruin1;
    public GameObject ruin2;
    public GameObject ruin3;
    public GameObject ruin4;
    public GameObject ruin5;

    // Start is called before the first frame update
    void Start()
    {
        targetList = GameObject.FindGameObjectsWithTag("Node");
        target = targetList[Random.Range(0, targetList.Length)];
        transform.LookAt(target.transform);
        speed = Random.Range(speedMin, speedMax);
    }
    
    void Update()
    {
        //transform.position += Vector3.down * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target.transform.position) <= 0.2f)
        {
            particleEffect1.Stop();
            particleEffect2.Stop();
            particleEffect1.gameObject.transform.parent = null;
            particleEffect2.gameObject.transform.parent = null;
            Destroy(particleEffect1.gameObject, 5f);
            Destroy(particleEffect2.gameObject, 5f);
            if (!reachedDestination)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.5f);
                reachedDestination = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "House" || 
            collision.collider.gameObject.tag == "Factory" || 
            collision.collider.gameObject.tag == "Park" || 
            collision.collider.gameObject.tag == "Generator")
        {
            Destroy(gameObject);
            Destroy(collision.collider.gameObject);
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
