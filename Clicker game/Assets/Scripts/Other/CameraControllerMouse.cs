using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerMouse : MonoBehaviour
{
    
    [Header("Move")]
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private float maxPosition;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementTime;
    [Header("Rotate")]
    [SerializeField] private Quaternion newRotation;
    [SerializeField] private float rotationAmount;
    [Header("Zoom")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 newZoom;
    [SerializeField] private Vector3 zoomAmount;
    [SerializeField] private float minZoomY;
    [SerializeField] private float maxZoomY;
    [Header("Animator")]
    [SerializeField] private Animator anim;
    [Header("Pop up canvas")]
    [SerializeField] private GameObject popUpCanvas;
    void Start()
    {
        StartCoroutine(DeleteAnimation());
        StartCoroutine(TemperaryDisablePopupCanvas());
        StartCoroutine(TemperaryDisableInput());

        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }
    void Update()
    {
        if(GameManager.i.canInput)
        {
            HandleMovementInput();
        }
    }
    IEnumerator DeleteAnimation()
    {
        yield return new WaitForSeconds(2.01f);
        Destroy(anim);
    }
    IEnumerator TemperaryDisablePopupCanvas()
    {
        yield return new WaitForSeconds(0.01f);
        popUpCanvas.SetActive(false);
        yield return new WaitForSeconds(2.01f);
        popUpCanvas.SetActive(true);
    }
    IEnumerator TemperaryDisableInput()
    {
        //Time.timeScale = 0;
        GameManager.i.canInput = false;
        yield return new WaitForSeconds(2.01f);
        //Time.timeScale = 1;
        GameManager.i.canInput = true;
    }
    void HandleMovementInput()
    {
        if (Mathf.Abs(newPosition.x) + Mathf.Abs(newPosition.z) <= maxPosition)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                newPosition += transform.forward * movementSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                newPosition += transform.right * -movementSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                newPosition += transform.forward * -movementSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                newPosition += transform.right * movementSpeed * Time.deltaTime;
            }
        }
        if (Mathf.Abs(newPosition.x) + Mathf.Abs(newPosition.z) > maxPosition)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                newPosition -= transform.forward * movementSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                newPosition -= transform.right * -movementSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                newPosition -= transform.forward * -movementSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                newPosition -= transform.right * movementSpeed * Time.deltaTime;
            }
        }
        //if(Input.GetKey(KeyCode.Q))
        //{
        //    newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        //}
        //if (Input.GetKey(KeyCode.E))
        //{
        //    newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        //}

        if (newZoom.y >= minZoomY && (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown)))
        {
            newZoom += zoomAmount * Time.deltaTime;
        }
        if (newZoom.y <= maxZoomY && (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp)))
        {
            newZoom -= zoomAmount * Time.deltaTime;
        }
        // Moving the camera
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationAmount);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
