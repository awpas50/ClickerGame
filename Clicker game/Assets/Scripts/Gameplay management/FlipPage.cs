using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlipPage : MonoBehaviour
{
    [Header("This Canvas")]
    [SerializeField] private GameObject canvasGO;
    [Header("Buttons")]
    [SerializeField] private Button closePanelButton;
    [SerializeField] private Button previousPageButton;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private TextMeshProUGUI pageText;
    [Header("Pages")]
    public int totalPage;
    public int currentPage = 1;
    [Header("Page list")]
    public GameObject[] pageList;
    
    void Update()
    {
        // Page text
        pageText.text = currentPage + " / " + totalPage;
        // Switch page
        for (int i = 0; i < pageList.Length; i++)
        {
            if(i + 1 == currentPage)
            {
                pageList[i].SetActive(true);
            }
            else
            {
                pageList[i].SetActive(false);
            }
        }

        // properties
        if(totalPage == 1)
        {
            previousPageButton.interactable = false;
            nextPageButton.interactable = false;
        }
        else if(totalPage > 1)
        {
            if (currentPage == 1)
            {
                previousPageButton.interactable = false;
                nextPageButton.interactable = true;
            }
            else if (currentPage == totalPage)
            {
                previousPageButton.interactable = true;
                nextPageButton.interactable = false;
            }
            else if (currentPage > 1 && currentPage < totalPage)
            {
                previousPageButton.interactable = true;
                nextPageButton.interactable = true;
            }
        }
    }
    // Button OnClick Event
    public void OpenPanel()
    {
        currentPage = 1;
        canvasGO.SetActive(true);

        GameManager.i.buildingSelectedInScene = null;
        GameManager.i.buildingObjectType = -1;
        GameManager.i.buildingSelectedInUI = null;

        UIManager.i.TopUI.SetActive(false);
        UIManager.i.BottomUI.SetActive(false);
        UIManager.i.RightUI.SetActive(false);
        UIManager.i.popUpStorage.SetActive(false);
        GameManager.i.buildingDetailsCanvas.SetActive(false);
        GameManager.i.buildingInfoCanvas.SetActive(false);

        GameManager.i.isPaused = true;
    }
    // Button OnClick Event
    public void ClosePanel()
    {
        canvasGO.SetActive(false);

        UIManager.i.TopUI.SetActive(true);
        UIManager.i.BottomUI.SetActive(true);
        UIManager.i.RightUI.SetActive(true);
        UIManager.i.popUpStorage.SetActive(true);

        GameManager.i.isPaused = false;
    }
    // Button OnClick Event
    public void PreviousPage()
    {
        currentPage--;
    }
    // Button OnClick Event
    public void NextPage()
    {
        currentPage++;
    }
}
