using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public LineRenderer lr;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Asteroid")
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, other.gameObject.transform.position);
            Destroy(other.gameObject);
            Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
        }
    }
}
