using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFollower : MonoBehaviour
{
    [HideInInspector] public LogisticCenter logisticCenter;
    public GameObject droneCompleteModel;
    //public GameObject objectToFollow;

    private float randomSpinY;
    //private float randomDelay1;

    //private float randomPosX;
    //private float randomPosY;
    //private float randomPosZ;

    void Start()
    {
        gameObject.transform.parent = null;
        //randomDelay1 = Random.Range(0f, 0.5f);

        StartCoroutine(DroneSpinning());
        //StartCoroutine(DroneMoving());
    }
    
    private void Update()
    {
        if (!logisticCenter)
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator DroneSpinning()
    {
        //yield return new WaitForSeconds(randomDelay1);
        while (true)
        {
            randomSpinY = Random.Range(180, 450);
            LeanTween.rotate(droneCompleteModel, droneCompleteModel.transform.position + new Vector3(0, randomSpinY, 0), 2f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(1f);
        }
    }
    //public IEnumerator DroneMoving()
    //{
    //    while (true)
    //    {
    //        //randomPosX = Random.Range(-0.02f, 0.02f);
    //        //randomPosY = Random.Range(0.08f, 0.16f);
    //        //randomPosZ = Random.Range(-0.02f, 0.02f);

    //        LeanTween.move(droneCompleteModel, objectToFollow.transform.position, 1f).setEase(LeanTweenType.easeInOutQuad);
    //        yield return new WaitForSeconds(logisticCenter.collectionSpeed_total);
    //    }
    //}
}
