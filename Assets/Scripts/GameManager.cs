using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Vector3 eggSpawnLocation = Vector3.zero;
    private Quaternion eggSpawnRotation = Quaternion.identity;

    private GameObject selectedCreature;
    private GameObject selectedCreatureDetection;

    public int levelIndex;
    public GameObject levelCompleteCanvasPrefab;

    public AudioClip menuSound;
    public GameObject audioManager;
    public GameObject audioManagerPrefab;


    private void Awake()
    {
        levelIndex = 0;

        //Setup GameManager Singleton Instance
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += StartScene;
    }

    private void Update()
    {
        if (Time.timeScale < 0.5f)
        {
            if (Input.GetMouseButtonUp(0) && selectedCreature != null)
            {
                CreatureUnSelectedStart();
            }
            else if (selectedCreature != null)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                selectedCreature.transform.position = new Vector3(worldPos.x, worldPos.y, -30);
            }
        }
    }

    private void StartScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "StartMenu")
        {
            StartLevel();
        }
    }

    public void StartLevel()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        if (!audioManager) audioManager = Instantiate(audioManagerPrefab);

        StartCoroutine(nameof(FindEggStart));
        GameObject.FindGameObjectWithTag("CreatureSelectUI").GetComponent<Image>().color = new Color(1, 1, 1, 1);

    }

    public void CreatureSelected(GameObject creature)
    {
        audioManager.GetComponent<AudioSource>().PlayOneShot(menuSound, 0.3f);
        selectedCreature = creature;
        selectedCreature.transform.SetParent(null, true);
        if (selectedCreature.GetComponentsInChildren<Collider2D>().Length == 2)
        {
            selectedCreatureDetection = selectedCreature.GetComponentsInChildren<Collider2D>()[1].gameObject;
        }
        else
        {
            selectedCreatureDetection = selectedCreature.GetComponentInChildren<Collider2D>().gameObject;
        }
        selectedCreatureDetection.SetActive(false);
        selectedCreature.GetComponent<Collider2D>().isTrigger = true;
        selectedCreature.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        selectedCreature.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void CreatureUnSelectedStart()
    {
        Time.timeScale = 1f;

        StartCoroutine(nameof(RepauseGame));
    }

    private void CreatureUnSelectedEnd()
    {
        Time.timeScale = 0.0f;

        //Find where the creature has been placed
        if (CheckValidPlacement())
        {
            selectedCreature.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else //Put back on the creature select UI
        {
            selectedCreature.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            selectedCreature.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            selectedCreature.transform.SetParent(GameObject.FindGameObjectWithTag("CreatureSelectUI").transform);
        }
        selectedCreature.GetComponent<Collider2D>().isTrigger = false;

        selectedCreatureDetection.SetActive(true);
        selectedCreature = null;
    }

    private bool CheckValidPlacement()
    {
        selectedCreature.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();
        foreach(Collider2D c2d in colliders)
        {
            if (selectedCreature.GetComponent<Rigidbody2D>().IsTouching(c2d))
            {
                return false;
            }
        }
        return true;
    }

    public void ResetLevel()
    {
        audioManager.GetComponent<AudioSource>().PlayOneShot(menuSound, 0.3f);
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

        GameObject.FindGameObjectWithTag("PauseText").GetComponent<TMP_Text>().text = "Paused";
        GameObject.FindGameObjectWithTag("CreatureSelectUI").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        Time.timeScale = 0.0f;
    }

    public void FinishLevel()
    {
        GameObject lcc = Instantiate(levelCompleteCanvasPrefab);
        lcc.GetComponentInChildren<TMP_Text>().text = "Level " + levelIndex + " Complete!";
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
        Time.timeScale = 0;

        if(levelIndex == 1)
        {
            audioManager.GetComponent<AudioSource>().PlayOneShot(menuSound, 0.5f);
            Time.timeScale = 1;
            GameObject.FindGameObjectWithTag("PauseText").GetComponent<TMP_Text>().text = "";
            GameObject.FindGameObjectWithTag("CreatureSelectUI").GetComponent<Image>().color = new Color(1, 0.6f, 0.6f, 1);
        }
    }

    public IEnumerator RepauseGame()
    {
        yield return new WaitForFixedUpdate();
        Time.timeScale = 0;
        CreatureUnSelectedEnd();
    }

    public void LoadNextLevel()
    {
        levelIndex++;
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }
}
