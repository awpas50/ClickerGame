using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager i;
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
        AudioManager.instance.Play(SoundList.ButtonClicked);
        SceneManager.LoadScene(1);
    }
    public void StartGame()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);
        SceneManager.LoadScene(1);
    }
    public void Settings()
    {

    }
    public void Credit()
    {

    }
    public void QuitGame()
    {
        AudioManager.instance.Play(SoundList.ButtonClicked);
        Application.Quit();
    }
}
