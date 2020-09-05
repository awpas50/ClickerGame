using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public float money;
    public static float MONEY;

    private void Start()
    {
        MONEY = money;
    }

    void Update()
    {
        money = MONEY;
    }
}
