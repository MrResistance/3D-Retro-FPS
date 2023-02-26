using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    private GameObject gameUI;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        UI_Manager Instance = UI_Manager.instance;
        gameUI = GameObject.Find("Canvases");
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            HideGameUI();
        }
    }
    public void ShowGameUI()
    {
        gameUI.SetActive(true);
    }
    public void HideGameUI()
    {
        gameUI.SetActive(false);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Test");
        ShowGameUI();
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
        HideGameUI();
    }
    public void QuitApp()
    {
        Application.Quit();
    }
}
