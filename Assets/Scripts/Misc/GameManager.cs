using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public gameState currentState;
    private bool created;
    private UI_Manager uiManager;
    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
        uiManager = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
    }
    public void pauseGame()
    {
        currentState = gameState.paused;
    }

    public void goToMainMenu()
    {
        currentState = gameState.menu;
        uiManager.LoadMainMenuScene();
    }
    public void playGame()
    {
        currentState = gameState.game;
        uiManager.LoadGameScene();
    }
    public void gameOver()
    {
        currentState = gameState.gameOver;
    }
    public enum gameState
    {
        menu,
        game,
        paused,
        gameOver
    }
}
