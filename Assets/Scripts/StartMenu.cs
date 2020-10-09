using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject startUI;
    public GameObject helpUI;
    public GameObject audioManager;
    public AudioClip menuSound;

    public void Start()
    {
        DontDestroyOnLoad(audioManager);
    }
    public void StartGame()
    {
        audioManager.GetComponent<AudioSource>().PlayOneShot(menuSound, 0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Help()
    {
        audioManager.GetComponent<AudioSource>().PlayOneShot(menuSound, 0.5f);
        startUI.SetActive(false);
        helpUI.SetActive(true);
    }

    public void BackToStart()
    {
        audioManager.GetComponent<AudioSource>().PlayOneShot(menuSound, 0.5f);
        startUI.SetActive(true);
        helpUI.SetActive(false);
    }

    public void ExitGame()
    {
        audioManager.GetComponent<AudioSource>().PlayOneShot(menuSound, 0.5f);
        Application.Quit();
    }
}

