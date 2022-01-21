using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public GameObject droneModel;

    private float randomDelay1;
    private float randomDelay2;
    private float randomPosX;
    private float randomPosY;
    private float randomPosZ;

    private float randomSpinY;

    void Start()
    {
        randomDelay1 = Random.Range(0f, 0.5f);
        randomDelay2 = Random.Range(0f, 0.5f);
        StartCoroutine(DroneSpinning());
        StartCoroutine(DroneFloating());
    }

    public IEnumerator DroneSpinning()
    {
        yield return new WaitForSeconds(randomDelay1);
        while (true)
        {
            randomSpinY = Random.Range(30, 90);
            LeanTween.rotate(droneModel, droneModel.transform.position + new Vector3(0, randomSpinY, 0), 2f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(2f);
        }
    }
    public IEnumerator DroneFloating()
    {
        yield return new WaitForSeconds(randomDelay2);
        while (true)
        {
            randomPosX = Random.Range(-0.02f, 0.02f);
            randomPosY = Random.Range(0.08f, 0.16f);
            randomPosZ = Random.Range(-0.02f, 0.02f);

            LeanTween.move(droneModel, droneModel.transform.position + new Vector3(randomPosX, randomPosY, randomPosZ), 1f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(1f);
            LeanTween.move(droneModel, droneModel.transform.position + new Vector3(-randomPosX, -randomPosY, -randomPosZ), 1f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(1f);
        }
    }
}
