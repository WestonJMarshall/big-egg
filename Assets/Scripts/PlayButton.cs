using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public AudioClip menuSound;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SetTimeScale);
    }

    private void SetTimeScale()
    {
        GameManager.Instance.audioManager.GetComponent<AudioSource>().PlayOneShot(menuSound, 0.5f);
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("PauseText").GetComponent<TMP_Text>().text = "";
        GameObject.FindGameObjectWithTag("CreatureSelectUI").GetComponent<Image>().color = new Color(1, 1, 1, 1);

    }
}
