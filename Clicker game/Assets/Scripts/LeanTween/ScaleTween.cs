﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    public LeanTweenType inType;
    public LeanTweenType outType;
    public float x, y, z;
    public float delay_in;
    public float delay_out;
    public float animationTime;
    
    void OnEnable()
    {
        ScaleUp();
    }

    void ScaleUp()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0f).setIgnoreTimeScale(true);
        LeanTween.scale(gameObject, new Vector3(x, y, z), animationTime).setDelay(delay_in).setEase(inType).setIgnoreTimeScale(true);
    }

    public void ScaleDown()
    {
        LeanTween.scale(gameObject, new Vector3(x, y, z), 0f).setIgnoreTimeScale(true);
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), animationTime).setDelay(delay_out).setOnComplete(DestroyMe).setEase(outType).setIgnoreTimeScale(true);
    }
    void DestroyMe()
    {
        Destroy(this);
    }
}