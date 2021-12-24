using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject whatToShow;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //SFX
        int seed = Random.Range(0, 3);
        switch (seed)
        {
            case 0:
                AudioManager.instance.Play(SoundList.Hover1);
                break;
            case 1:
                AudioManager.instance.Play(SoundList.Hover2);
                break;
            case 2:
                AudioManager.instance.Play(SoundList.Hover3);
                break;
        }

        if (whatToShow)
        {
            whatToShow.SetActive(true);
        }
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (whatToShow)
        {
            whatToShow.SetActive(false);
        }
    }
    public void OnDisable()
    {
        if (whatToShow)
        {
            whatToShow.SetActive(false);
        }
    }
}
