using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level5_DialogueManager : MonoBehaviour
{
    public static Level5_DialogueManager instance;

    public int sceneBadIndex;
    public int sceneGoodIndex;

    public int currentGood;
    public int currentBad;

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
        currentBad = 0;
        currentGood = 0;
    }

    public void _BtnGood()
    {
        currentGood++;
    }

    public void _BtnBad()
    {
        currentBad++;
    }

    public void _BtnNextScene()
    {
        if (currentGood >= currentBad)
            PlayerPrefs.SetInt("_SceneEnd", sceneGoodIndex);
        else
            PlayerPrefs.SetInt("_SceneEnd", sceneBadIndex);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
