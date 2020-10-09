using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SetTimeScale);
    }

    private void SetTimeScale()
    {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("PauseText").GetComponent<TMP_Text>().text = "";
        GameObject.FindGameObjectWithTag("CreatureSelectUI").GetComponent<Image>().color = new Color(1, 1, 1, 1);

    }
}
