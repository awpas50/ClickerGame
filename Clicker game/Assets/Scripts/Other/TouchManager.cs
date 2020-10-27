using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    public static TouchManager instance;
    private Touch touch;
    private Plane plane;
    
    public bool timerTrigger = false;
    public float timer = 0f;
    public bool isDragging = false;
    public bool isRotating = false;

    //Mouse drag
    Vector3 touchStart;
    void Awake()
    {
        //debug
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene");
            return;
        }
        instance = this;
    }

    void Update()
    {
        // building selection
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                isDragging = true;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                timerTrigger = true;
            }
        }
        if (timerTrigger)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 0.2f)
        {
            isDragging = false;
            timer = 0f;
            timerTrigger = false;
        }
        // Mouse drag
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        ////Update plane
        //if(Input.touchCount >= 1)
        //{
        //    plane.SetNormalAndPosition(transform.up, transform.position);
        //}

        //var Delta1 = Vector3.zero;
        //var Delta2 = Vector3.zero;

        ////Scroll
        //if(Input.touchCount >= 1)
        //{
        //    Delta1 = PlanePositionDelta(Input.GetTouch(0));
        //    if(Input.GetTouch(0).phase == TouchPhase.Moved)
        //    {
        //        Camera.main.transform.Translate(Delta1, Space.World);
        //    }
        //}
        ////Pinch
        //if(Input.touchCount >= 2)
        //{
        //    var pos1 = PlanePosition(Input.GetTouch(0).position);
        //    var pos2 = PlanePosition(Input.GetTouch(1).position);
        //    var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
        //    var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

        //    // calculate zoom
        //    var zoom = Vector3.Distance(pos1, pos2) / Vector3.Distance(pos1b, pos2b);
        //    // edge case
        //    if(zoom == 0 || zoom > 10)
        //    {
        //        return;
        //    }
        //    //Move cam amount the mid ray
        //    Camera.main.transform.position = Vector3.LerpUnclamped(pos1, Camera.main.transform.position, 1 / zoom);

        //    if(isRotating && pos2b != pos2)
        //    {
        //        Camera.main.transform.RotateAround(pos1, plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, plane.normal));
        //    }
        //}

    }

    //public Vector3 PlanePosition(Vector2 screenPos)
    //{
    //    var rayNow = Camera.main.ScreenPointToRay(screenPos);
    //    if(plane.Raycast(rayNow, out var enterNow))
    //    {
    //        return rayNow.GetPoint(enterNow);
    //    }
    //    return Vector3.zero;
    //}

    //public Vector3 PlanePositionDelta(Touch touch)
    //{
    //    //not moved
    //    if(touch.phase != TouchPhase.Moved)
    //    {
    //        return Vector3.zero;
    //    }

    //    //delta
    //    var rayBefore = Camera.main.ScreenPointToRay(touch.position - touch.deltaPosition);
    //    var rayNow = Camera.main.ScreenPointToRay(touch.position);
    //    if(plane.Raycast(rayBefore, out var enterBefore) && plane.Raycast(rayNow, out var enterNow))
    //    {
    //        return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);
    //    }
    //    return Vector3.zero;
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(transform.position, transform.position + transform.up);
    //}
}
