using System;
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
        if(MONEY >= 1000)
        {
            MONEY = Mathf.RoundToInt(MONEY);
        }
        else if(MONEY < 1000)
        {
            MONEY = (float)Math.Round(MONEY, 1);
        }
        money = MONEY;

        if (Input.GetKeyDown(KeyCode.K))
        {
            MONEY += 1000;
        }
    }
}
