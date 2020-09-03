using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public float moneyProduced_auto;
    public float interval_auto;

    public float moneyProduced_popup;
    public float interval_popup;

    void Start()
    {
        StartCoroutine(Production_AUTOMATIC(moneyProduced_auto, interval_auto));
        StartCoroutine(Production_POP_UP(moneyProduced_popup, interval_popup));
    }

    IEnumerator Production_AUTOMATIC(float moneyProduced, float interval)
    {
        while(true)
        {
            yield return new WaitForSeconds(interval);
            Currency.MONEY += moneyProduced;
        }
    }
    IEnumerator Production_POP_UP(float moneyProduced, float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Currency.MONEY += moneyProduced;
        }
    }
}
