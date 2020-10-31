using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    [Header("Indicate which node is belongs to")]
    public GameObject attachedNode;
    private void OnMouseDown()
    {
        // Audio
        AudioManager.instance.Play(SoundList.BuildingPlaced);

        // When clicked that means cancel building on the particular node:
        // Find the node in the node list (attached in GameManager and delete it)
        // **(GameManager - nodeList)
        // **(GameManager - futureBuildingList)
        GameManager.i.nodeList.Remove(GameManager.i.nodeList[attachedNode.GetComponent<Node>().nodeIndex]);
        GameManager.i.futureBuildingList.Remove(GameManager.i.futureBuildingList[attachedNode.GetComponent<Node>().nodeIndex]);
        // Give back the money.
        // **(GameManager - estimatedCostList)
        Currency.MONEY += GameManager.i.estimatedCostList[attachedNode.GetComponent<Node>().nodeIndex];
        GameManager.i.estimatedCostList.Remove(GameManager.i.estimatedCostList[attachedNode.GetComponent<Node>().nodeIndex]);
        // Rearrange list
        for(int i = attachedNode.GetComponent<Node>().nodeIndex; i < GameManager.i.nodeList.Count; i++)
        {
            GameManager.i.nodeList[i].GetComponent<Node>().nodeIndex--;
        }
        // remove the references on the node.
        attachedNode.GetComponent<Node>().nodeIndex = -1;
        attachedNode.GetComponent<Node>().placeHolder = null;
        attachedNode.GetComponent<Node>().placeHolder_building_REF = null;
        // FInally destroy the placeholder after all reference is removed
        Destroy(gameObject);
    }
}
