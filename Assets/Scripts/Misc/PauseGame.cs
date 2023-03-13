using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private GameManager gm;
    private void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }
    public void OnPause()
    {
        if (gm.currentState == GameManager.gameState.paused)
        {
            gm.unpauseGame();
        }
        else if (gm.currentState == GameManager.gameState.game)
        {
            gm.pauseGame();
        }

    }
}
