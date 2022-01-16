using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGlowingMat : MonoBehaviour
{
    [Header("If only one model, just leave the second space empty")]
    public GameObject[] model1Glows;
    public GameObject model2Glow;

    private Material mat1;
    private Material mat1_2;
    private Material mat1_3;
    private Material mat2;

    private float t1;
    private float t1_2;
    private float t1_3;
    private float t2;

    private void Start()
    {
        StartCoroutine(SetGlow());
        for (int i = 0; i < model1Glows.Length; i++)
        {
            if(model1Glows[i] != null)
            {
                Destroy(model1Glows[i], 5f);
            }
            
        }
        if(model2Glow != null)
        {
            Destroy(model2Glow, 5f);
        }
    }
    public IEnumerator SetGlow()
    {
        yield return new WaitForSeconds(0.02f);
        // House / Factory / Park model 1
        if (gameObject.GetComponent<BuildingRandomModel>() && gameObject.GetComponent<BuildingRandomModel>().seed == 0)
        {
            model1Glows[0].SetActive(true);
            mat1 = model1Glows[0].transform.GetChild(0).GetComponent<MeshRenderer>().material;
        }
        // House / Factory / Park model 2
        else if (gameObject.GetComponent<BuildingRandomModel>() && gameObject.GetComponent<BuildingRandomModel>().seed == 1)
        {
            model2Glow.SetActive(true);
            mat2 = model2Glow.transform.GetChild(0).GetComponent<MeshRenderer>().material;
            // Avoid performance issue
        }
        // Other buildings
        else if(!gameObject.GetComponent<BuildingRandomModel>())
        {
            if (model1Glows[0] != null)
            {
                model1Glows[0].SetActive(true);
                mat1 = model1Glows[0].transform.GetChild(0).GetComponent<MeshRenderer>().material;
            }
            if (model1Glows[1] != null)
            {
                model1Glows[1].SetActive(true);
                mat1_2 = model1Glows[1].transform.GetChild(0).GetComponent<MeshRenderer>().material;
            }
            if (model1Glows[2] != null)
            {
                model1Glows[2].SetActive(true);
                mat1_3 = model1Glows[2].transform.GetChild(0).GetComponent<MeshRenderer>().material;
            }
        }

        if (mat1)
        {
            mat1.SetFloat("_EmissionIntensity", 1.8f);
            t1 = 1.8f;
        }
        if (mat1_2)
        {
            mat1_2.SetFloat("_EmissionIntensity", 1.8f);
            t1_2 = 1.8f;
        }
        if (mat1_3)
        {
            mat1_3.SetFloat("_EmissionIntensity", 1.8f);
            t1_3 = 1.8f;
        }
        if (mat2)
        {
            mat2.SetFloat("_EmissionIntensity", 1.8f);
            t2 = 1.8f;
        }
    }

    private void Update()
    {
        if (mat1 && mat1.GetFloat("_EmissionIntensity") >= 0)
        {
            t1 -= Time.deltaTime;
            mat1.SetFloat("_EmissionIntensity", t1);
        }
        if (mat1_2 && mat1_2.GetFloat("_EmissionIntensity") >= 0)
        {
            t1_2 -= Time.deltaTime;
            mat1_2.SetFloat("_EmissionIntensity", t1_2);
        }
        if (mat1_3 && mat1_3.GetFloat("_EmissionIntensity") >= 0)
        {
            t1_3 -= Time.deltaTime;
            mat1_3.SetFloat("_EmissionIntensity", t1_3);
        }
        if (mat2 && mat2.GetFloat("_EmissionIntensity") >= 0)
        {
            t2 -= Time.deltaTime;
            mat2.SetFloat("_EmissionIntensity", t2);
        }
    }
}
