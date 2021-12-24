using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonOnClickSound : MonoBehaviour, IPointerDownHandler
{
    public SoundList soundList;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>() && gameObject.GetComponent<Button>().interactable)
        {
            AudioManager.instance.Play(soundList);
        }
        else if(!gameObject.GetComponent<Button>())
        {
            AudioManager.instance.Play(soundList);
        }
    }
}
