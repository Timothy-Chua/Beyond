using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGroup : MonoBehaviour
{
    public GameObject[] tutorials;
    public int currentIndex;
    private float delayTime = .75f;
    private bool isDelay;

    // Start is called before the first frame update
    void Start()
    {
        isDelay = false;

        for (int i = 0; i < tutorials.Length; i++)
        {
            if (i == currentIndex) 
            {
                tutorials[i].SetActive(true);
                continue;
            }
            tutorials[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state == GameState.Tutorial)
        {
            if (Input.GetKeyDown(SettingsManager.instance.keyConfirm))
            {
                if (!isDelay)
                {
                    _BtnNext();
                }
            }
        }
    }

    public void _BtnNext()
    {
        SoundManager.instance.PlayKeyboardSFX();

        tutorials[currentIndex].SetActive(false);

        currentIndex++;

        if (currentIndex >= tutorials.Length)
            UIManager.instance._BtnGameplay();
        else
        {
            tutorials[currentIndex].SetActive(true);
            StartCoroutine(Delay());
        }
    }

    public void _BtnPrev()
    {

        tutorials[currentIndex].SetActive(false);

        if (currentIndex > 0) 
        {
            SoundManager.instance.PlayKeyboardSFX();
            currentIndex--;

            tutorials[currentIndex].SetActive(true);
            StartCoroutine(Delay());
        }
        else
        {
            SoundManager.instance.PlayErrorSFX();
        }
    }

    private IEnumerator Delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }
}
