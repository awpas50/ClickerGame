using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScaleTweenOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public LeanTweenType inType;
    public LeanTweenType outType;
    public float ox, oy, oz;
    public float x, y, z;
    public float animationTime;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(gameObject.GetComponent<Button>().interactable)
        {
            LeanTween.scale(gameObject, new Vector3(x, y, z), animationTime).setEase(inType).setIgnoreTimeScale(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(ox, oy, oz), animationTime).setEase(outType).setIgnoreTimeScale(true);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>().interactable)
        {
            LeanTween.scale(gameObject, new Vector3(x * 1.12f, y * 1.12f, z * 1.12f), 0.05f).setEase(inType).setIgnoreTimeScale(true);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>().interactable)
        {
            LeanTween.scale(gameObject, new Vector3(ox, oy, oz), 0.05f).setEase(outType).setIgnoreTimeScale(true);
        }
    }
}
