using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainBuilding : MonoBehaviour
{
    [Header("Click lock")]
    public float limit = 10;
    private float t;
    [Header("Money")]
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

    private void Update()
    {
        t += Time.deltaTime;
        if(t > 0.2f && limit >= 10)
        {
            t = 0;
        }
        if(t > 0.2f && limit < 10)
        {
            limit++;
            t = 0;
        }
    }
    public void OnMouseOver()
    {
        //prevent clicking through UI
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}
        if ((Input.GetMouseButtonUp(0)) && !GameManager.i.isPaused && limit > 0)
        {
            MainBuildingClickEvent();
        }
    }
    public void MainBuildingClickEvent()
    {
        limit--;

        AudioManager.instance.Play(SoundList.ButtonClicked2);
        Currency.MONEY += moneyEachClick;
        GameObject mainBuildingPopUpREF = Instantiate(mainBuildingPopUp, transform.position, Quaternion.identity);
        mainBuildingPopUpREF.GetComponent<MainBuildingPopUp>().realMoneyEachClick = moneyEachClick;
        mainBuildingPopUpREF.transform.SetParent(popupStorageCanvas.transform);
        StartCoroutine(BuildingPopAnimation());
    }
    public void MainBuildingClickEvent_LogisticCenterBuff()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked2);
        Currency.MONEY += moneyEachClick * 4;
        GameObject mainBuildingPopUpREF = Instantiate(mainBuildingPopUp, transform.position, Quaternion.identity);
        mainBuildingPopUpREF.GetComponent<MainBuildingPopUp>().realMoneyEachClick = moneyEachClick * 4;
        mainBuildingPopUpREF.transform.SetParent(popupStorageCanvas.transform);
        StartCoroutine(BuildingPopAnimation());
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
