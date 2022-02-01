using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager i;

    [SerializeField] private GameObject title;
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject button3;
    [SerializeField] private GameObject button4;
    [SerializeField] private GameObject button5;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditPanel;

    void Awake()
    {
        //debug
        if (i != null)
        {
            Debug.LogError("More than one MainMenuManager in scene");
            return;
        }
        i = this;
    }
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void ContinueGame()
    {
        StartCoroutine(ContinueGameAsync());
    }
    IEnumerator ContinueGameAsync()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);

        AllUIScaleDown();
        yield return new WaitForSeconds(1f);
        DontDestroyOnLoad(gameObject);
        SaveLoadHandler slh = FindObjectOfType<SaveLoadHandler>();
        // CreateSceneLoader()
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called
        yield return new WaitForEndOfFrame();
        // Load save data
        slh.LoadGame();
        // Destroy itself after everything has loaded
        Destroy(gameObject);
    }
    public void StartGame()
    {
        StartCoroutine(StartGameAsync());
    }
    IEnumerator StartGameAsync()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);

        AllUIScaleDown();
        yield return new WaitForSeconds(1f);
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called
        yield return new WaitForEndOfFrame();
    }
    public void Settings()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);
    }
    public void Credit()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);
    }
    public void QuitGame()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);
        Application.Quit();
    }

    void AllUIScaleDown()
    {
        title.GetComponent<ScaleTween>().ScaleDown();
        button1.GetComponent<ScaleTween>().ScaleDown();
        button2.GetComponent<ScaleTween>().ScaleDown();
        button3.GetComponent<ScaleTween>().ScaleDown();
        button4.GetComponent<ScaleTween>().ScaleDown();
        button5.GetComponent<ScaleTween>().ScaleDown();
        settingPanel.GetComponent<ScaleTween>().ScaleDown();
        creditPanel.GetComponent<ScaleTween>().ScaleDown();

        title.GetComponent<ScaleTween>().DestroyMouseHoveringEvent();
        button1.GetComponent<ScaleTween>().DestroyMouseHoveringEvent();
        button2.GetComponent<ScaleTween>().DestroyMouseHoveringEvent();
        button3.GetComponent<ScaleTween>().DestroyMouseHoveringEvent();
        button4.GetComponent<ScaleTween>().DestroyMouseHoveringEvent();
        button5.GetComponent<ScaleTween>().DestroyMouseHoveringEvent();
        settingPanel.GetComponent<ScaleTween>().DestroyMouseHoveringEvent();
        creditPanel.GetComponent<ScaleTween>().DestroyMouseHoveringEvent();
    }

    public void OpenItchLink()
    {
        Application.OpenURL("https://awpas50.itch.io/");
    }
}
