using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public gameState currentState;
    private bool created;
    private UI_Manager uiManager;
    private DJ dj;
    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
        uiManager = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
        dj = GameObject.Find("DJ").GetComponent<DJ>();
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void pauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        currentState = gameState.paused;
        Time.timeScale = 0;
    }

    public void goToMainMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        currentState = gameState.menu;
        uiManager.LoadMainMenuScene();
        dj.PlayMenuMusic();
    }
    public void playGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentState = gameState.game;
        uiManager.LoadGameScene();
        dj.PlayGameMusic();
        Time.timeScale = 1;
    }
    public void gameOver()
    {
        Cursor.lockState = CursorLockMode.Confined;
        currentState = gameState.gameOver;
        dj.PlayLossSound();
        Time.timeScale = 0;
    }
    public enum gameState
    {
        menu,
        game,
        paused,
        gameOver
    }
}
