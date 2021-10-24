using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainBuilding : MonoBehaviour
{
    public float moneyEachClick;
    [Header("What to instantiate")]
    public GameObject mainBuildingPopUp;
    [Header("Where to instantiate (Do not edit)")]
    public GameObject popupStorageCanvas;
    //[Header("Attached pop up (Do not edit)")]
    //public GameObject mainBuildingPopUpREF;

    [Header("Animation")]
    public bool repeatable = false;
    private float speed = 10f;
    public float duration = 0.5f;
    Vector3 originalScale;

    void Start()
    {
        // Animation
        originalScale = transform.localScale;
        popupStorageCanvas = GameObject.FindGameObjectWithTag("StorageCanvas");
    }
    
    public void OnMouseOver()
    {
        //prevent clicking through UI
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}
        if (Input.GetMouseButtonUp(0) && !GameManager.i.buildingSelectedInScene && !GameManager.i.isPaused)
        {
            AudioManager.instance.Play(SoundList.ButtonClicked);
            MainBuildingClickEvent();
            StartCoroutine(BuildingPopAnimation());
        }
    }
    public void MainBuildingClickEvent()
    {
        Currency.MONEY += moneyEachClick;
        GameObject mainBuildingPopUpREF = Instantiate(mainBuildingPopUp, transform.position, Quaternion.identity);
        mainBuildingPopUpREF.transform.SetParent(popupStorageCanvas.transform);
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
