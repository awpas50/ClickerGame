using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningPopUp : MonoBehaviour
{
    [Header("Props")]
    public Image img;
    public float flyingSpeed;
    public Vector3 offset;
    Vector3 pos;
    [Header("Resources")]
    public TextMeshProUGUI warningText;

    public void AssignText(string _text)
    {
        warningText.text = _text;
    }
    void Start()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        img.transform.position = pos;
    }

    void Update()
    {
        img.transform.position += Vector3.up * flyingSpeed * Time.deltaTime;
        Destroy(gameObject, 1.5f);
    }
}
