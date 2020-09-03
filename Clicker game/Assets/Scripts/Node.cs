using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Props")]
    public GameObject building;
    public float cost;

    [Header("Node REF")]
    public GameObject building_REF;
    public Vector3 offset;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && Currency.MONEY >= cost)
        {
            Currency.MONEY -= cost;
            Instantiate(building, transform.position, Quaternion.identity);
        }
    }
}
