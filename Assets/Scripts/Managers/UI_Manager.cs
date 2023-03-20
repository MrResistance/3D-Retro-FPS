using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject gameUI, gameOverUI, continueButton, retryButton, settingsCanvas, mainMenuCanvas;
    void Awake()
    {
        gameUI = GameObject.Find("Canvases");
        gameOverUI = GameObject.Find("Game Over Canvas");
        gameOverUI.SetActive(false);
        settingsCanvas = GameObject.Find("SettingsCanvas");
        settingsCanvas.SetActive(false);
        mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            HideGameUI();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
    }
    public void HideMainMenu()
    {
        mainMenuCanvas.SetActive(false);
    }
    public void ShowSettingsMenu()
    {
        settingsCanvas.SetActive(true);
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            HideMainMenu();
        }
        else
        {
            HidePauseMenu();
        }
    }
    public void HideSettingsMenu()
    {
        
        settingsCanvas.SetActive(false);
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            ShowMainMenu();
        }
        else
        {
            ShowPauseMenu();
        }
    }
    public void ShowPauseMenu()
    {
        ShowGameOverUI();
        continueButton.SetActive(true);
        retryButton.SetActive(false);
    }
    public void HidePauseMenu()
    {
        HideGameOverUI();
    }
    public void ShowGameOverUI()
    { 
        gameOverUI.SetActive(true);
        continueButton.SetActive(false);
        retryButton.SetActive(true);
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
