using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    private GameObject gameUI, gameOverUI;
    void Awake()
    {
        gameUI = GameObject.Find("Canvases");
        gameOverUI = transform.GetChild(0).transform.GetChild(0).gameObject;
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            HideGameUI();
        }
    }
    public void ShowGameOverUI()
    { 
        gameOverUI.SetActive(true);
    }
    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
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
