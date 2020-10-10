using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public GameObject exitUI;
    private bool exitUIActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitUI.SetActive(exitUIActive);
            exitUIActive = !exitUIActive;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
