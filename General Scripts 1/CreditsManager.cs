using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CreditsManager : MonoBehaviour
{
    public VideoPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player.Play();
        StartCoroutine(CreditsScene());
    }

    private IEnumerator CreditsScene()
    {
        yield return new WaitForSeconds((float) player.clip.length);
        SceneManager.LoadScene(0);
    }
}
