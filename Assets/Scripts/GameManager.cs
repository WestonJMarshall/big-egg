using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Vector3 eggSpawnLocation = Vector3.zero;
    private Quaternion eggSpawnRotation = Quaternion.identity;

    private void Awake()
    {
        //Setup GameManager Singleton Instance
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        StartLevel();
    }

    private void StartScene(Scene scene, LoadSceneMode mode)
    {
        StartLevel();
    }

    public void StartLevel()
    {
        StartCoroutine(nameof(FindEggStart));
    }

    public void ResetLevel()
    {
        GameObject egg = FindObjectOfType<Egg>().gameObject;
        if (egg != null)
        {
            GameObject newEgg = Instantiate(egg);
            newEgg.name = "Egg";
            newEgg.transform.position = eggSpawnLocation;
            newEgg.transform.rotation = eggSpawnRotation;
            Destroy(egg);
        }

        Creature[] creatures = FindObjectsOfType<Creature>();
        foreach(Creature creature in creatures)
        {
            creature.StopAllCoroutines();
            creature.ResetCanDoAction();
        }
    }

    public void FinishLevel()
    {
        Debug.Log("Level Complete");
    }

    public IEnumerator FindEggStart()
    {
        yield return new WaitForEndOfFrame();
        GameObject egg = FindObjectOfType<Egg>().gameObject;
        if (egg != null)
        {
            eggSpawnLocation = egg.transform.position;
            eggSpawnRotation = egg.transform.rotation;
        }
    }

    public void PauseLevel()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
