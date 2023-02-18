using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    private bool created;
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Test");
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void QuitApp()
    {
        Application.Quit();
    }
}
