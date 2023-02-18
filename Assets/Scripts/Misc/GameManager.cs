using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public gameState currentState;

    public void pauseGame()
    {
        currentState = gameState.paused;
    }

    public void goToMainMenu()
    {
        currentState = gameState.menu;
    }
    public void playGame()
    {
        currentState = gameState.game;
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
