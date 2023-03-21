using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject gameUI, gameOverUI, continueButton, retryButton, settingsCanvas, mainMenuCanvas, weaponUI;
    void Awake()
    {
        gameUI = GameObject.Find("GameCanvas");
        weaponUI = GameObject.Find("WeaponCanvas");
        gameOverUI = GameObject.Find("Game Over Canvas");
        settingsCanvas = GameObject.Find("SettingsCanvas");
        mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            HideGameUI();
            HideSettingsMenu();
        }
        else
        {
            ShowGameUI();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void ShowMainMenu()
    {
        mainMenuCanvas.GetComponent<Canvas>().enabled = true;
    }
    public void HideMainMenu()
    {
        mainMenuCanvas.GetComponent<Canvas>().enabled = false;
    }
    public void ShowSettingsMenu()
    {
        settingsCanvas.GetComponent<Canvas>().enabled = true;
    }
    public void HideSettingsMenu()
    {

        settingsCanvas.GetComponent<Canvas>().enabled = false;
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
        HideSettingsMenu();
    }
    public void ShowGameOverUI()
    { 
        gameOverUI.GetComponent<Canvas>().enabled = true;
        continueButton.SetActive(false);
        retryButton.SetActive(true);
    }
    public void HideGameOverUI()
    {
        gameOverUI.GetComponent<Canvas>().enabled = false;
    }
    public void ShowGameUI()
    {
        gameUI.GetComponent<Canvas>().enabled = true;
        weaponUI.GetComponent<Canvas>().enabled = true;
    }
    public void HideGameUI()
    {
        gameUI.GetComponent<Canvas>().enabled = false;
        weaponUI.GetComponent<Canvas>().enabled = false;
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
