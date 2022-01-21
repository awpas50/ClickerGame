using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogisticCenter : MonoBehaviour
{
    [HideInInspector] public float collectionSpeed_total = 1.8f;
    [HideInInspector] public float collectionSpeed_base;
    [HideInInspector] public float collectionSpeed_extra;
    public float collectionSpeed_initial = 1.8f;

    public int collectPositionIndex = -1; 
    [SerializeField] private Dictionary<int, Vector3> collectPositionDict; // 1 - 25, omit 13

    [Header("Script reference (Do not edit)")]
    public BuildingBuff buildingBuff;
    public BuildingLevel buildingLevel;
    public BuildingState buildingState;

    [Header("Environmental pollution")]
    private float pollution_auto;
    [HideInInspector] public float pollution_auto_base;
    [HideInInspector] public float pollution_auto_extra;
    [SerializeField] private float pollution_auto_initial = 0.2f;
    public float pollution_auto_interval;

    [Header("Drone")]
    public GameObject droneCompleteModel;
    [SerializeField] private Transform buildingFinder;

    private void Awake()
    {
        collectPositionIndex = Random.Range(1, 26); // 1 ~ 25

        collectPositionDict = new Dictionary<int, Vector3>();

        collectPositionDict.Add(1, new Vector3(-2, 0, -2));
        collectPositionDict.Add(2, new Vector3(-2, 0, -1));
        collectPositionDict.Add(3, new Vector3(-2, 0, 0));
        collectPositionDict.Add(4, new Vector3(-2, 0, 1));
        collectPositionDict.Add(5, new Vector3(-2, 0, 2));

        collectPositionDict.Add(6, new Vector3(-1, 0, -2));
        collectPositionDict.Add(7, new Vector3(-1, 0, -1));
        collectPositionDict.Add(8, new Vector3(-1, 0, 0));
        collectPositionDict.Add(9, new Vector3(-1, 0, 1));
        collectPositionDict.Add(10, new Vector3(-1, 0, 2));

        collectPositionDict.Add(11, new Vector3(0, 0, -2));
        collectPositionDict.Add(12, new Vector3(0, 0, -1));
        collectPositionDict.Add(13, new Vector3(0, 0, 0));
        collectPositionDict.Add(14, new Vector3(0, 0, 1));
        collectPositionDict.Add(15, new Vector3(0, 0, 2));

        collectPositionDict.Add(16, new Vector3(1, 0, -2));
        collectPositionDict.Add(17, new Vector3(1, 0, -1));
        collectPositionDict.Add(18, new Vector3(1, 0, 0));
        collectPositionDict.Add(19, new Vector3(1, 0, 1));
        collectPositionDict.Add(20, new Vector3(1, 0, 2));

        collectPositionDict.Add(21, new Vector3(2, 0, -2));
        collectPositionDict.Add(22, new Vector3(2, 0, -1));
        collectPositionDict.Add(23, new Vector3(2, 0, 0));
        collectPositionDict.Add(24, new Vector3(2, 0, 1));
        collectPositionDict.Add(25, new Vector3(2, 0, 2));

        // Add reference
        buildingFinder.GetComponent<LogisticCenterFinder>().logisticCenter = this;
        droneCompleteModel.GetComponent<DroneFollower>().logisticCenter = this;

        // Detach from the parent gameobject.
        buildingFinder.parent = null;
    }
    void Start()
    {
        buildingBuff = GetComponent<BuildingBuff>();
        buildingLevel = GetComponent<BuildingLevel>();
        buildingState = GetComponent<BuildingState>();
        
        StartCoroutine(AutoCollectResources());
        StartCoroutine(Pollution_AUTOMATIC(pollution_auto_interval));
    }
    private void Update()
    {
        collectionSpeed_base = collectionSpeed_initial - 0.3f * (buildingLevel.level - 1);
        // Efficiency
        // Each house nearby grants 5% efficiency (+1% every level)
        // houseEfficiencyTotal will be multiplied by 0.2 as the original efficiency of a level 1 house is 25%. Multiply by 0.2 makes it 5%.
        collectionSpeed_extra = buildingBuff.houseEfficiencyTotal * 0.2f;
        collectionSpeed_total = collectionSpeed_base - collectionSpeed_extra;
        pollution_auto_base = pollution_auto_initial + 0.05f * (buildingLevel.level - 1);
        pollution_auto_extra = buildingBuff.houseEfficiencyTotal * 0.2f;
        pollution_auto = pollution_auto_base + pollution_auto_extra;
    }

    IEnumerator AutoCollectResources()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        while(true)
        {
            // Find nearest building: 
            if (!GameManager.i.isPaused && GameManager.i.canInput)
            {
                //LeanTween.move(droneCompleteModel, buildingFinder.transform.position, 0.75f).setEase(LeanTweenType.easeInOutQuad);
                buildingFinder.position = gameObject.transform.position + collectPositionDict[collectPositionIndex];
                LeanTween.move(droneCompleteModel, buildingFinder.transform.position + new Vector3(0, 1.5f, 0), 0.4f).setEase(LeanTweenType.easeInOutQuad);
                //droneCompleteModel.transform.position = Vector3.MoveTowards(droneCompleteModel.transform.position, buildingFinder.position + new Vector3(0, 1.5f, 0), 20 * Time.deltaTime);
            }

            yield return new WaitForSeconds(collectionSpeed_total);

            if(GameManager.i.isPaused || !GameManager.i.canInput)
            {
                if (collectPositionIndex >= 25)
                {
                    collectPositionIndex += 0;
                }
                else
                {
                    collectPositionIndex += 0;
                }
            }
            if (!GameManager.i.isPaused && GameManager.i.canInput)
            {
                if (collectPositionIndex >= 25)
                {
                    collectPositionIndex = 1;
                }
                else
                {
                    collectPositionIndex++;
                }
            }
        }
    }

    public IEnumerator Pollution_AUTOMATIC(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Pollution.POLLUTION += pollution_auto;
        }
    }
}
