using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class UIManager_CutsceneSimple : MonoBehaviour
{
    public static UIManager_CutsceneSimple instance;

    public GameObject panelPause;

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

    private IEnumerator Delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }

    public void _BtnPause()
    {
        CutsceneManager.instance.PlayPressSFX();
        isPause = true;
        panelPause.SetActive(true);
        StartCoroutine(Delay());
    }

    public void _BtnPlay()
    {
        CutsceneManager.instance.PlayPressSFX();
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
