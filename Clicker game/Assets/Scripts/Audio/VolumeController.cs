using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float SfxVolume = 1f;

    public static VolumeController i;
    void Awake()
    {
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
