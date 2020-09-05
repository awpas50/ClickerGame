using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    [Header("Indicate which node is belongs to")]
    public GameObject attachedNode;
    private void OnMouseDown()
    {
        // When clicked that means cancel building on the particular node:
        // Find the node in the node list (attached in GameManager and delete it)
        GameManager.instance.nodeList.Remove(GameManager.instance.nodeList[attachedNode.GetComponent<Node>().nodeIndex]);
        // Give back the money.
        Currency.MONEY += GameManager.instance.estimatedCostList[attachedNode.GetComponent<Node>().nodeIndex];
        GameManager.instance.estimatedCostList.Remove(GameManager.instance.estimatedCostList[attachedNode.GetComponent<Node>().nodeIndex]);
        // Rearrange list
        for(int i = attachedNode.GetComponent<Node>().nodeIndex; i < GameManager.instance.nodeList.Count; i++)
        {
            GameManager.instance.nodeList[i].GetComponent<Node>().nodeIndex--;
        }
        // remove the references on the node.
        attachedNode.GetComponent<Node>().nodeIndex = -1;
        attachedNode.GetComponent<Node>().placeHolder = null;
        attachedNode.GetComponent<Node>().placeHolder_building_REF = null;
        // FInally destroy the placeholder after all reference is removed
        Destroy(gameObject);
    }
}
