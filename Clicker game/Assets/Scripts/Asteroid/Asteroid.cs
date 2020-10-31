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

        if(Vector3.Distance(transform.position, target.transform.position) <= 0.1f)
        {
            particleEffect1.Stop();
            particleEffect2.Stop();
            particleEffect1.gameObject.transform.parent = null;
            particleEffect2.gameObject.transform.parent = null;
            Destroy(particleEffect1.gameObject, 5f);
            Destroy(particleEffect2.gameObject, 5f);
            if (!reachedDestination)
            {
                // Audio
                int seed = Random.Range(0, 3);
                if (seed == 0)
                {
                    AudioManager.instance.Play(SoundList.explosion1);
                }
                else if (seed == 1)
                {
                    AudioManager.instance.Play(SoundList.explosion2);
                }
                else
                {
                    AudioManager.instance.Play(SoundList.explosion3);
                }

                GameObject effectPrefab = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(effectPrefab, 3f);
                Destroy(gameObject, 0.5f);
                reachedDestination = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "House" ||
            other.gameObject.tag == "Factory" ||
            other.gameObject.tag == "Park" ||
            other.gameObject.tag == "Generator" ||
            other.gameObject.tag == "Airport")
        {
            // Audio
            int seed = Random.Range(0, 3);
            if(seed == 0)
            {
                AudioManager.instance.Play(SoundList.explosion1);
            }
            else if(seed == 1)
            {
                AudioManager.instance.Play(SoundList.explosion2);
            }
            else
            {
                AudioManager.instance.Play(SoundList.explosion3);
            }
            // create a ruin on top of the node & destroy building
            Node nodeLocationForRuin = other.gameObject.GetComponent<BuildingState>().node.GetComponent<Node>();
            Ruin ruinScript;
            // remove building reference from node
            nodeLocationForRuin.building_REF = null;
            
            // different colored ruin
            if (other.gameObject.tag == "House")
            {
                nodeLocationForRuin.building_ruin = Instantiate(ruin1, nodeLocationForRuin.gameObject.transform.position, Quaternion.identity);
                // Store building data for restoring
                ruinScript = nodeLocationForRuin.building_ruin.GetComponent<Ruin>();
                ruinScript.buildingData = GameManager.i.building1.building;

                // ruin needs to recongize node
                ruinScript.node = nodeLocationForRuin.gameObject;
                ruinScript.buildingLevel = other.gameObject.GetComponent<BuildingLevel>().level;
                ruinScript.repairCost = other.gameObject.GetComponent<BuildingLevel>().sellCost / 3;
            }
            if (other.gameObject.tag == "Factory")
            {
                nodeLocationForRuin.building_ruin = Instantiate(ruin2, nodeLocationForRuin.gameObject.transform.position, Quaternion.identity);
                // Store building data for restoring
                ruinScript = nodeLocationForRuin.building_ruin.GetComponent<Ruin>();
                ruinScript.buildingData = GameManager.i.building2.building;

                // ruin needs to recongize node
                ruinScript.node = nodeLocationForRuin.gameObject;
                ruinScript.buildingLevel = other.gameObject.GetComponent<BuildingLevel>().level;
                ruinScript.repairCost = other.gameObject.GetComponent<BuildingLevel>().sellCost / 3;

                // if it has a pop up than destroy the pop up
                other.gameObject.GetComponent<Factory>().StopAllCoroutines();
                GameObject tempPopUpData = other.gameObject.GetComponent<Factory>().factoryPopUpREF;
                Destroy(tempPopUpData, 0.1f);
                other.gameObject.GetComponent<Factory>().factoryPopUpREF = null;
            }
            if (other.gameObject.tag == "Park")
            {
                nodeLocationForRuin.building_ruin = Instantiate(ruin3, nodeLocationForRuin.gameObject.transform.position, Quaternion.identity);
                // Store building data for restoring
                ruinScript = nodeLocationForRuin.building_ruin.GetComponent<Ruin>();
                ruinScript.buildingData = GameManager.i.building3.building;

                // ruin needs to recongize node
                ruinScript.node = nodeLocationForRuin.gameObject;
                ruinScript.buildingLevel = other.gameObject.GetComponent<BuildingLevel>().level;
                ruinScript.repairCost = other.gameObject.GetComponent<BuildingLevel>().sellCost / 3;

                // if it has a pop up than destroy the pop up
                other.gameObject.GetComponent<Park>().StopAllCoroutines();
                GameObject tempPopUpData = other.gameObject.GetComponent<Park>().parkPopUpREF;
                Destroy(tempPopUpData, 0.1f);
                other.gameObject.GetComponent<Park>().parkPopUpREF = null;
            }
            if (other.gameObject.tag == "Generator")
            {
                nodeLocationForRuin.building_ruin = Instantiate(ruin4, nodeLocationForRuin.gameObject.transform.position, Quaternion.identity);
                // Store building data for restoring
                ruinScript = nodeLocationForRuin.building_ruin.GetComponent<Ruin>();
                ruinScript.buildingData = GameManager.i.building4.building;

                // ruin needs to recongize node
                ruinScript.node = nodeLocationForRuin.gameObject;
                ruinScript.buildingLevel = other.gameObject.GetComponent<BuildingLevel>().level;
                ruinScript.repairCost = other.gameObject.GetComponent<BuildingLevel>().sellCost / 3;
            }
            if (other.gameObject.tag == "Airport")
            {
                nodeLocationForRuin.building_ruin = Instantiate(ruin5, nodeLocationForRuin.gameObject.transform.position, Quaternion.identity);
                // Store building data for restoring
                ruinScript = nodeLocationForRuin.building_ruin.GetComponent<Ruin>();
                ruinScript.buildingData = GameManager.i.building5.building;

                // ruin needs to recongize node
                ruinScript.node = nodeLocationForRuin.gameObject;
                ruinScript.buildingLevel = other.gameObject.GetComponent<BuildingLevel>().level;
                ruinScript.repairCost = other.gameObject.GetComponent<BuildingLevel>().sellCost / 3;

                // if it has a pop up than destroy the pop up
                other.gameObject.GetComponent<Airport>().StopAllCoroutines();
                GameObject tempPopUpData = other.gameObject.GetComponent<Airport>().airportPopUpREF;
                Destroy(tempPopUpData, 0.1f);
                other.gameObject.GetComponent<Airport>().airportPopUpREF = null;
            }

            // destroy building
            Destroy(other.gameObject, 0.1f);

            // destroy asteroid
            GameObject effectPrefab = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(effectPrefab, 3f);
            Destroy(gameObject, 0.5f);

        }
    }
}
