using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AppManager : MonoBehaviour
{
    public static AppManager instance;

    [SerializeField] static string activeScene;
    [SerializeField] static string lastScene;

    public GameObject EventSystem;

    private void Awake()
    {
        if(instance != null)
        {
            DestroyImmediate(gameObject, true);
            DontDestroyOnLoad(EventSystem);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(EventSystem);
        }
    }
    private void Start()
    {
        activeScene = SceneManager.GetActiveScene().name;
    }
    public void OpenLastPanel()
    {
        string sceneName = lastScene;
        lastScene = SceneManager.GetActiveScene().name;
        activeScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }

    public void OpenMenu()
    {
        OpenPanel(PanelNames.Menu);
    }
    public void OpenMainScene()
    {
        OpenPanel(PanelNames.MainScene);
    }

    public void OpenPanel(PanelNames panelName)
    {
        if (panelName.ToString() != activeScene)
        {
            string sceneName;
            lastScene = SceneManager.GetActiveScene().name;
            sceneName = panelName.ToString();
            activeScene = sceneName;
            SceneManager.LoadScene(sceneName);
        }
    }

    public enum PanelNames
    {
        Menu = 0,
        MainScene = 1
    }

    public void ExitApp()
    {
        Application.Quit();
    }

}
