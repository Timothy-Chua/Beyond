using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class UIManager_Cutscene : MonoBehaviour
{
    public static UIManager_Cutscene instance;

    public GameObject panelPause;
    public PlayableDirector director;

    public float delayTime = 1f;
    private bool isDelay;
    public bool isPause;

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
        isPause = false;
        isDelay = false;
        panelPause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPause)
        {
            if (Input.GetKey(SettingsManager.instance.keyPause))
            {
                if (!isDelay)
                {
                    _BtnPause();
                }
            }
        }
        else
        {
            if (Input.GetKey(SettingsManager.instance.keyPause))
            {
                if (!isDelay)
                {
                    _BtnPlay();
                }
            }
        }
    }

    private void SetDirectorSpeed(PlayableDirector director, float speed)
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(speed);
        director.Play();
    }

    private IEnumerator Delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }

    public void _BtnPause()
    {
        Cursor.visible = true;
        CutsceneManager.instance.PlayPressSFX();
        SetDirectorSpeed(director, 0f);
        isPause = true;
        panelPause.SetActive(true);
        StartCoroutine(Delay());
    }

    public void _BtnPlay()
    {
        Cursor.visible = false;
        CutsceneManager.instance.PlayPressSFX();
        SetDirectorSpeed(director, 1f);
        isPause = false;
        panelPause.SetActive(false);
        StartCoroutine(Delay());
    }

    public void _BtnSkip()
    {
        CutsceneManager.instance.PlayPressSFX();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void _BtnMainMenu()
    {
        CutsceneManager.instance.PlayPressSFX();
        SceneManager.LoadScene(0);
    }
}
