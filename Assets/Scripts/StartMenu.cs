using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject startUI;
    public GameObject helpUI;

    public void Awake()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Help()
    {
        startUI.SetActive(false);
        helpUI.SetActive(true);
    }

    public void BackToStart()
    {
        startUI.SetActive(true);
        helpUI.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

