using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkPopUp : MonoBehaviour
{
    public Image img;
    public Vector3 offset;
    [Header("Assigned in script - do not edit")]
    public GameObject parkREF;
    public Park parkREF_script;

    private void Start()
    {
        parkREF_script = parkREF.GetComponent<Park>();
    }
    void Update()
    {
        // Screen border
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector3 pos = Camera.main.WorldToScreenPoint(parkREF.transform.position + offset);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
    }

    // When clicked
    public void ButtonEvent()
    {
        parkREF_script.GetResources(parkREF_script.pollutionReduced_popup);
        // Close this pop up.
        Destroy(gameObject);
    }
}
