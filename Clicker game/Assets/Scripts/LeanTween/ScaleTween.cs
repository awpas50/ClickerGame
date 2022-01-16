using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    public bool canPlayScaleUpSound = false;
    public SoundList scaleUpSound;
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
        LeanTween.scale(gameObject, new Vector3(x, y, z), animationTime).setDelay(delay_in).setEase(inType).setIgnoreTimeScale(true).setOnComplete(PlayScaleUpSound);
    }

    public void ScaleDown()
    {

        LeanTween.scale(gameObject, new Vector3(x, y, z), 0f).setIgnoreTimeScale(true);
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), animationTime).setDelay(delay_out).setOnComplete(DisableMe).setEase(outType).setIgnoreTimeScale(true);
    }
    void DisableMe()
    {
        gameObject.SetActive(false);
        //Destroy(this);
    }
    void PlayScaleUpSound()
    {
        if(canPlayScaleUpSound)
        {
            AudioManager.instance.Play(scaleUpSound);
        }
    }
}
