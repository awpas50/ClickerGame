using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerMouse : MonoBehaviour
{
    public static CameraControllerMouse i;
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

    void Awake()
    {
        //debug
        if (i != null)
        {
            Debug.LogError("More than one CameraControllerMouse in scene");
            return;
        }
        i = this;
    }

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
        anim = null;
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
        yield return new WaitForSeconds(2.01f + 0.06f);
        //Time.timeScale = 1;
        GameManager.i.canInput = true;
    }
    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        //if (Mathf.Abs(newPosition.x) + Mathf.Abs(newPosition.z) > maxPosition)
        //{
        //    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        //    {
        //        newPosition -= transform.forward * movementSpeed * Time.deltaTime;
        //    }
        //    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //    {
        //        newPosition -= transform.right * -movementSpeed * Time.deltaTime;
        //    }
        //    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        //    {
        //        newPosition -= transform.forward * -movementSpeed * Time.deltaTime;
        //    }
        //    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //    {
        //        newPosition -= transform.right * movementSpeed * Time.deltaTime;
        //    }
        //}
        if ((Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown)))
        {
            ZoomIn();
        }
        if ((Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp)))
        {
            ZoomOut();
        }
        // Moving the camera
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationAmount);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    public void MoveUp()
    {
        if (Mathf.Abs(newPosition.x) + Mathf.Abs(newPosition.z) <= maxPosition)
        {
            newPosition += transform.forward * movementSpeed * Time.deltaTime;
        }
        if (Mathf.Abs(newPosition.x) + Mathf.Abs(newPosition.z) > maxPosition)
        {
            newPosition -= transform.forward * movementSpeed * Time.deltaTime;
        }
    }
    public void MoveDown()
    {
        if (Mathf.Abs(newPosition.x) + Mathf.Abs(newPosition.z) <= maxPosition)
        {
            newPosition += transform.forward * -movementSpeed * Time.deltaTime;
        }
        if (Mathf.Abs(newPosition.x) + Mathf.Abs(newPosition.z) > maxPosition)
        {
            newPosition -= transform.forward * -movementSpeed * Time.deltaTime;
        }
    }
    public void MoveLeft()
    {
        if (Mathf.Abs(newPosition.x) + Mathf.Abs(newPosition.z) <= maxPosition)
        {
            newPosition += transform.right * -movementSpeed * Time.deltaTime;
        }
        if (Mathf.Abs(newPosition.x) + Mathf.Abs(newPosition.z) > maxPosition)
        {
            newPosition -= transform.right * -movementSpeed * Time.deltaTime;
        }
    }
    public void MoveRight()
    {
        if (Mathf.Abs(newPosition.x) + Mathf.Abs(newPosition.z) <= maxPosition)
        {
            newPosition += transform.right * movementSpeed * Time.deltaTime;
        }
        if (Mathf.Abs(newPosition.x) + Mathf.Abs(newPosition.z) > maxPosition)
        {
            newPosition -= transform.right * movementSpeed * Time.deltaTime;
        }
    }
    public void ZoomIn()
    {
        if(newZoom.y >= minZoomY)
        {
            newZoom += zoomAmount * Time.deltaTime;
        }
    }
    public void ZoomOut()
    {
        if(newZoom.y <= maxZoomY)
        {
            newZoom -= zoomAmount * Time.deltaTime;
        }
    }
    public void ResetCameraDefaultSettings()
    {
        FreezeInput();
        LeanTween.move(gameObject, new Vector3(1.37f, 0, -3.37f), 0.2f).setEase(LeanTweenType.easeInQuad).setOnComplete(ResumeInput);
        
    }
    void FreezeInput()
    {
        GameManager.i.canInput = false;
    }
    void ResumeInput()
    {
        GameManager.i.canInput = true;
        newPosition = gameObject.transform.position;
    }
}
