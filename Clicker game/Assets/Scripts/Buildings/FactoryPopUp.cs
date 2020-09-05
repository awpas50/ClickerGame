using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryPopUp : MonoBehaviour
{
    public Image img;
    public Vector3 offset;
    public GameObject factoryREF;
    public Factory factoryREF_script;

    private void Start()
    {
        factoryREF_script = factoryREF.GetComponent<Factory>();
    }
    void Update()
    {
        // Screen border
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector3 pos = Camera.main.WorldToScreenPoint(factoryREF.transform.position + offset);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
    }

    // When clicked
    public void ButtonEvent()
    {
        factoryREF_script.GetResources(factoryREF_script.moneyProduced_popup, factoryREF_script.pollutionProduced_popup);
        // Close this pop up.
        Destroy(gameObject);
    }
}
