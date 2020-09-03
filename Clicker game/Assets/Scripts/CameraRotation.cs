using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed;

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }
}
