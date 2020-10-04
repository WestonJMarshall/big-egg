using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Update()
    {
        if(Input.GetMouseButtonUp(0) && selectedCreature != null)
        {
            CreatureUnSelected();
        }
        else if(selectedCreature != null)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedCreature.transform.position = new Vector3(worldPos.x, worldPos.y, -30);
        }
    }

    private void StartScene(Scene scene, LoadSceneMode mode)
    {
        StartLevel();
    }

    public void StartLevel()
    {
        StartCoroutine(nameof(FindEggStart));
    }

    public void CreatureSelected(GameObject creature)
    {
        selectedCreature = creature;
        selectedCreature.transform.SetParent(null, true);
        selectedCreatureDetection = selectedCreature.GetComponentsInChildren<Collider2D>()[1].gameObject;
        selectedCreatureDetection.SetActive(false);
        selectedCreature.GetComponent<Collider2D>().isTrigger = true;
        selectedCreature.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        selectedCreature.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void CreatureUnSelected()
    {
        //Find where the creature has been placed
        if(CheckValidPlacement())
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
