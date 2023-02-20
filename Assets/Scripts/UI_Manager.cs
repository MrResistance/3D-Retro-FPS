using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
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
