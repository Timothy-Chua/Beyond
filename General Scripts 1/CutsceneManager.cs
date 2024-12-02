using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;

    public AudioSource SFXPlayer;
    public AudioClip sfxClick;

    public bool isInteractable;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = SettingsManager.instance.volumeMaster;
        PlayerPrefs.SetInt("_lastLevel", SceneManager.GetActiveScene().buildIndex);

        if (!isInteractable)
            Cursor.visible = false;
        else
            Cursor.visible = true;
    }

    private void Update()
    {

    }

    public void PlayPressSFX()
    {
        SFXPlayer.PlayOneShot(sfxClick, SettingsManager.instance.volumeSFX);
    }
}
