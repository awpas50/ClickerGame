using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerMouse : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementTime;
    [Header("Rotate")]
    [SerializeField] private Quaternion newRotation;
    [SerializeField] private float rotationAmount;
    [Header("Zoom")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 newZoom;
    [SerializeField] private Vector3 zoomAmount;

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += transform.forward * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += transform.right *-movementSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += transform.forward * -movementSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += transform.right * movementSpeed;
        }

        //if(Input.GetKey(KeyCode.Q))
        //{
        //    newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        //}
        //if (Input.GetKey(KeyCode.E))
        //{
        //    newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        //}

        if(Input.GetKey(KeyCode.F))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.R))
        {
            newZoom -= zoomAmount;
        }
        // Moving the camera
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationAmount);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
