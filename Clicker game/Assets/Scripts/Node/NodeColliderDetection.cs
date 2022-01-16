using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeColliderDetection : MonoBehaviour
{
    private Node nodeREF;
    // Detect nearby collision
    // Start is called before the first frame update
    void Start()
    {
        nodeREF = transform.parent.GetComponent<Node>();
        InvokeRepeating("DetectNearbyBuildings", 0f, 0.07f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Node"))
        {
            nodeREF.nearbyNode.Add(other.gameObject);
            nodeREF.nearbyNode_building.Add(null);
        }
        if (other.gameObject.CompareTag("MainBuilding"))
        {
            nodeREF.nearbyNode.Add(other.gameObject);
            nodeREF.nearbyNode_building.Add(null);
        }
    }

    void DetectNearbyBuildings()
    {
        // If a node has building on it, add it to the list.
        // Main building will be added to both list.
        for (int i = 0; i < nodeREF.nearbyNode.Count; i++)
        {
            if (nodeREF.nearbyNode[i].CompareTag("Node") && nodeREF.nearbyNode[i].GetComponent<Node>().building_REF != null)
            {
                nodeREF.nearbyNode_building[i] = nodeREF.nearbyNode[i].GetComponent<Node>().building_REF;
            }
            else if (nodeREF.nearbyNode[i].CompareTag("MainBuilding"))
            {
                nodeREF.nearbyNode_building[i] = nodeREF.nearbyNode[i];
            }
            else
            {
                nodeREF.nearbyNode_building[i] = null;
            }
        }
    }
}
