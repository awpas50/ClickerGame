using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleTweenOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public LeanTweenType inType;
    public LeanTweenType outType;
    public float ox, oy, oz;
    public float x, y, z;
    public float animationTime;

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(x, y, z), animationTime).setEase(inType).setIgnoreTimeScale(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(ox, oy, oz), animationTime).setEase(outType).setIgnoreTimeScale(true);
    }
    private void OnMouseEnter()
    {
        LeanTween.scale(gameObject, new Vector3(x, y, z), animationTime).setEase(inType).setIgnoreTimeScale(true);
    }
    private void OnMouseExit()
    {
        LeanTween.scale(gameObject, new Vector3(ox, oy, oz), animationTime).setEase(outType).setIgnoreTimeScale(true);
    }
}
