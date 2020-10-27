using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRotation : MonoBehaviour
{
    public bool isDragging;
    Vector3 touchStart;
    public float zoomMin;
    public float zoomMax;
    public float zoomSpeed = 5f;
    public float rotationSpeed;

    Touch touch;

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    isDragging = true;
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    isDragging = false;
        //}
        //if (touch.phase == TouchPhase.Moved)
        //{
        //    isDragging = true;
        //}
        //if (touch.phase != TouchPhase.Moved)
        //{
        //    isDragging = false;
        //}
        #if UNITY_STANDALONE || UNITY_EDITOR_WIN
        // Scroll
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize += zoomSpeed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize -= zoomSpeed;
        }
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, zoomMin, zoomMax);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        #endif
        //// prevent clicking through UI
        //if (!EventSystem.current.IsPointerOverGameObject())
        //{
        //    // Drag
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    }
        //    if (Input.touchCount == 2)
        //    {
        //        Touch touchZero = Input.GetTouch(0);
        //        Touch touchOne = Input.GetTouch(1);

        //        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        //        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        //        float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        //        float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

        //        float difference = currentMagnitude - prevMagnitude;

        //        Zoom(difference * 0.01f);
        //    }
        //    else if (Input.GetMouseButton(0))
        //    {
        //        // prevent clicking through UI
        //        if (EventSystem.current.IsPointerOverGameObject())
        //        {
        //            return;
        //        }
        //        Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        Camera.main.transform.position += direction;
        //    }
        //}
        
    }

    //void Zoom (float increment)
    //{
    //    Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomMin, zoomMax);
    //}
}
