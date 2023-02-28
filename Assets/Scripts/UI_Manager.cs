using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject gameUI, gameOverUI;
    void Awake()
    {
        gameUI = GameObject.Find("Canvases");
        gameOverUI = GameObject.Find("Game Over Canvas");
        gameOverUI.SetActive(false);
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            HideGameUI();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
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
