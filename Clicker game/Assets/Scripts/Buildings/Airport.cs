using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airport : MonoBehaviour
{
    [Header("Auto generated pollution")]
    public float pollutionProduced_auto;
    public float pollutionInterval_auto;
    
    void Start()
    {
        StartCoroutine(Pollution_AUTOMATIC(pollutionProduced_auto, pollutionInterval_auto));
    }

    public IEnumerator Pollution_AUTOMATIC(float pollutionProduced, float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Pollution.POLLUTION += pollutionProduced;
        }
    }
}
