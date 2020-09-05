using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Colliders to detect neraby node")]
    public BoxCollider boxCollider;
    [Header("Neraby node list")]
    public List<GameObject> nearbyNode;
    [Header("Check neraby node with building (Don not edit)")]
    public List<GameObject> nearbyNode_building;
    [Header("Node REF (Do not edit)")]
    public int nodeIndex = -1;
    public GameObject placeHolder;
    public GameObject placeHolder_building_REF;
    public GameObject building_REF;
    public Vector3 offset;

    private void Update()
    {
        GetNearbyNode();
    }
    private void OnMouseDown()
    {
        // Select building in scene
        if (building_REF != null)
        {
            GameManager.instance.buildingSelectedInScene = building_REF;
            //Remove reference of builing selected in the UI
            GameManager.instance.buildingSelectedInList = null;
            GameManager.instance.buildingCost = 0;
        }
        
        if (building_REF == null && GameManager.instance.buildingSelectedInList && Currency.MONEY >= GameManager.instance.buildingCost)
        {
            AddPlaceHolder();
            EstimateCost();
        }
    }

    public void AddPlaceHolder()
    {
        // Add the node into the list 
        GameManager.instance.nodeList.Add(gameObject);
        // Using a index to recongnize the order (from 0)
        nodeIndex = GameManager.instance.nodeList.IndexOf(gameObject);
        // indicator only, make the node recongnize the placeHolder
        placeHolder = Instantiate(GameManager.instance.placeHolder, transform.position, Quaternion.identity);
        // Oppositely, the placeHolder needs to recongnize the node.
        PlaceHolder placeHolder_script = placeHolder.GetComponent<PlaceHolder>();
        placeHolder_script.attachedNode = gameObject;
        // Store what building will be placed in the particular node
        placeHolder_building_REF = GameManager.instance.buildingSelectedInList;
    }
    public void EstimateCost()
    {
        // Add the esitmated cost into a list
        GameManager.instance.estimatedCostList.Add(GameManager.instance.buildingCost);
        // Deduct the money needed
        Currency.MONEY -= GameManager.instance.buildingCost;
    }
    public void RemovePlaceHolder()
    {
        Destroy(placeHolder);
        placeHolder = null;
    }
    public void ConfirmPlaceBuilding()
    {
        GameObject buildingPrefab = Instantiate(placeHolder_building_REF, transform.position, Quaternion.identity);
        // Add reference
        building_REF = buildingPrefab;

        GameManager.instance.estimatedCostList.Clear();
    }
    public void RemoveBuildingPlaceHolder()
    {
        placeHolder_building_REF = null;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Node"))
        {
            Debug.Log("Touched");
            nearbyNode.Add(other.gameObject);
        }
    }
    public void GetNearbyNode()
    {
        OnTriggerEnter(boxCollider);
    }
}
