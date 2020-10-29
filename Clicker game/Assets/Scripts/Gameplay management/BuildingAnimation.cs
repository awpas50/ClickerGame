using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAnimation : MonoBehaviour
{
    [Header("Animation")]
    public bool repeatable = false;
    private float speed = 10f;
    public float duration = 0.5f;
    Vector3 originalScale;

    void Start()
    {
        // Animation
        originalScale = transform.localScale;
    }

    public IEnumerator BuildingPopAnimation()
    {
        // Animation
        yield return RepeatLerp(originalScale, originalScale * 1.2f, duration);
        yield return RepeatLerp(originalScale * 1.2f, originalScale, duration);
    }

    //Animation
    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0f;
        float rate = (1 / time) * speed;
        while (i < 1f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
