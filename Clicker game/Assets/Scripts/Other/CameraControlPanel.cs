using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControlPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CameraControllerMouse cameraControllerMouse;
    private bool isOnButton = false;
    public string invokeFunction;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOnButton = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isOnButton = false;
    }
    private void Update()
    {
        if(isOnButton && Input.GetMouseButton(0))
        {
            switch (invokeFunction) {
                case "MoveUp":
                    cameraControllerMouse.MoveUp();
                    break;
                case "MoveDown":
                    cameraControllerMouse.MoveDown();
                    break;
                case "MoveLeft":
                    cameraControllerMouse.MoveLeft();
                    break;
                case "MoveRight":
                    cameraControllerMouse.MoveRight();
                    break;
                case "ZoomIn":
                    cameraControllerMouse.ZoomIn();
                    break;
                case "ZoomOut":
                    cameraControllerMouse.ZoomOut();
                    break;
                default:
                    break;
            }
        }
    }
}
