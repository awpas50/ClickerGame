using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundWithDelay : MonoBehaviour, IPointerDownHandler
{
    public float delay;
    public SoundList soundList;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(PlaySoundWithDelay());
    }

    IEnumerator PlaySoundWithDelay()
    {
        yield return new WaitForSeconds(delay);
        if (gameObject.GetComponent<Button>() && gameObject.GetComponent<Button>().interactable)
        {
            AudioManager.instance.Play(soundList);
        }
        else if (!gameObject.GetComponent<Button>())
        {
            AudioManager.instance.Play(soundList);
        }
    }
}
