using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Assigned in asteroid spawner
    public int uniqueID;
    public float speed;
    public float speedMin = 5f;
    public float speedMax = 7f;
    public GameObject[] targetList;
    public GameObject target;
    public Vector3 target_vector3;
    public GameObject explosionEffect;
    public ParticleSystem particleEffect1;
    public ParticleSystem particleEffect2;

    public bool isLockedByTurret = false;
    public bool reachedDestination = false;
    // this variable will be recorded by the save system, used to prevent reassigning targets when loading a game.
    //public bool assignedTarget = false; 
    [Header("Building Ruins")]
    public GameObject ruin1;
    public GameObject ruin2;
    public GameObject ruin3;
    public GameObject ruin4;
    public GameObject ruin5;

    // OnEnable(): initialize before Start();
    void OnEnable()
    {
        targetList = GameObject.FindGameObjectsWithTag("Node");
        List<GameObject> targetList_2 = new List<GameObject>();
        for (int i = 0; i < targetList.Length; i++)
        {
            // != 10: MainBuildingNode
            if (targetList[i].layer != 10)
            {
                targetList_2.Add(targetList[i]);
            }
        }
        target = targetList_2[Random.Range(0, targetList_2.Count)];
        target_vector3 = target.transform.position;
        transform.LookAt(target_vector3);
    }
    
    void Update()
    {
        //transform.position += Vector3.down * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target_vector3, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target_vector3) <= 0.1f)
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
                Destroy(gameObject, 0.05f);
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
                ruinScript.buildingType = 1;

                // ruin needs to recongize node
                ruinScript.node = nodeLocationForRuin.gameObject;
                ruinScript.buildingLevel = other.gameObject.GetComponent<BuildingLevel>().level;
                ruinScript.repairCost = other.gameObject.GetComponent<BuildingLevel>().sellCost / 1.25f;
            }
            if (other.gameObject.tag == "Factory")
            {
                nodeLocationForRuin.building_ruin = Instantiate(ruin2, nodeLocationForRuin.gameObject.transform.position, Quaternion.identity);
                // Store building data for restoring
                ruinScript = nodeLocationForRuin.building_ruin.GetComponent<Ruin>();
                ruinScript.buildingData = GameManager.i.building2.building;
                ruinScript.buildingType = 2;

                // ruin needs to recongize node
                ruinScript.node = nodeLocationForRuin.gameObject;
                ruinScript.buildingLevel = other.gameObject.GetComponent<BuildingLevel>().level;
                ruinScript.repairCost = other.gameObject.GetComponent<BuildingLevel>().sellCost / 1.25f;

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
                ruinScript.buildingType = 3;

                // ruin needs to recongize node
                ruinScript.node = nodeLocationForRuin.gameObject;
                ruinScript.buildingLevel = other.gameObject.GetComponent<BuildingLevel>().level;
                ruinScript.repairCost = other.gameObject.GetComponent<BuildingLevel>().sellCost / 1.25f;

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
                ruinScript.buildingType = 4;

                // ruin needs to recongize node
                ruinScript.node = nodeLocationForRuin.gameObject;
                ruinScript.buildingLevel = other.gameObject.GetComponent<BuildingLevel>().level;
                ruinScript.repairCost = other.gameObject.GetComponent<BuildingLevel>().sellCost / 1.25f;
            }
            if (other.gameObject.tag == "Airport")
            {
                nodeLocationForRuin.building_ruin = Instantiate(ruin5, nodeLocationForRuin.gameObject.transform.position, Quaternion.identity);
                // Store building data for restoring
                ruinScript = nodeLocationForRuin.building_ruin.GetComponent<Ruin>();
                ruinScript.buildingData = GameManager.i.building5.building;
                ruinScript.buildingType = 5;

                // ruin needs to recongize node
                ruinScript.node = nodeLocationForRuin.gameObject;
                ruinScript.buildingLevel = other.gameObject.GetComponent<BuildingLevel>().level;
                ruinScript.repairCost = other.gameObject.GetComponent<BuildingLevel>().sellCost / 1.25f;

                // if it has a pop up than destroy the pop up
                other.gameObject.GetComponent<Airport>().StopAllCoroutines();
                GameObject tempPopUpData = other.gameObject.GetComponent<Airport>().airportPopUpREF;
                Destroy(tempPopUpData);
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
