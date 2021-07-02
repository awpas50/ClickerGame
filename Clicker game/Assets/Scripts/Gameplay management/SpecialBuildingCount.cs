using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBuildingCount : MonoBehaviour
{
    public int platform1_Count;
    public static int platform1Count = 0;

    private void Start()
    {
        platform1Count = platform1_Count;
    }
}
