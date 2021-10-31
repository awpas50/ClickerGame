using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDestroyer : MonoBehaviour
{
    public float destroySecond;
    public Animator anim;
    void Start()
    {
        StartCoroutine(DestoryWithDelay());
    }

    IEnumerator DestoryWithDelay()
    {
        yield return new WaitForSeconds(destroySecond);
        Destroy(anim);
    }
}
