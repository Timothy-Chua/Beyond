using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLoadResult : MonoBehaviour
{
    public int sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        sceneToLoad = PlayerPrefs.GetInt("_SceneEnd");
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
