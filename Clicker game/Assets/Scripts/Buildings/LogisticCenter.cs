using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogisticCenter : MonoBehaviour
{
    [HideInInspector] public float collectionSpeed = 1.7f;
    public float collectionSpeed_initial = 1.7f;

    [SerializeField] private int collectPositionIndex = -1; 
    [SerializeField] private Dictionary<int, Vector3> collectPositionDict; // 1 - 25, omit 13

    [SerializeField] private Transform buildingFinder;

    [Header("Script reference (Do not edit)")]
    public BuildingBuff buildingBuff;
    public BuildingLevel buildingLevel;
    public BuildingState buildingState;

    [Header("Environmental pollution")]
    public float pollution_auto;
    private float pollution_auto_initial = 0.15f;
    public float pollution_auto_interval;

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

        // Detach from the parent gameobject.
        buildingFinder.parent = null;
    }
    void Start()
    {
        buildingBuff = GetComponent<BuildingBuff>();
        buildingLevel = GetComponent<BuildingLevel>();
        buildingState = GetComponent<BuildingState>();
        
        StartCoroutine(AutoCollectResources());
    }
    private void Update()
    {
        collectionSpeed = collectionSpeed_initial - 0.4f * (buildingLevel.level - 1);
        pollution_auto = pollution_auto_initial + 0.03f * (buildingLevel.level - 1);
    }

    IEnumerator AutoCollectResources()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        while(true)
        {
            // Find nearest building: 
            buildingFinder.position = gameObject.transform.position + collectPositionDict[collectPositionIndex];
            yield return new WaitForSeconds(0.25f);
            if(collectPositionIndex >= 25)
            {
                collectPositionIndex = 1;
            }
            else
            {
                collectPositionIndex++;
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
