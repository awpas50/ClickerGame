using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSelection : MonoBehaviour
{
    public string buildingName;
    public Sprite sprite;
    public GameObject indicator;

    [Header("Animation")]
    public bool repeatable = false;
    private float speed = 10f;
    public float duration = 0.5f;
    Vector3 originalScale;
    // This script is attached into the buildings

    private void Start()
    {
        // Animation
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Add a indicator surrounded the building
        // if not selected, indicator disappears
        if(GameManager.i.buildingSelectedInScene != null)
        {
            if (GameManager.i.buildingSelectedInScene != gameObject)
            {
                indicator.SetActive(false);
            }
            else
            {
                indicator.SetActive(true);
            }
        }
        //if (Input.GetMouseButtonDown(1))
        //{
        //    // Select building in scene
        //    GameManager.instance.buildingSelectedInScene = null;

        //    //Remove reference of builing selected in the UI
        //    GameManager.instance.buildingSelectedInUI = null;
        //    GameManager.instance.buildingCost = 0;

        //    UIManager.instance.buildingName.text = buildingName;

        //    // Remove indicator surrounded the building
        //    indicator.SetActive(false);
        //}
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
