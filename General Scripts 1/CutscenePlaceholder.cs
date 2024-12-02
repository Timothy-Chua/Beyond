using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutscenePlaceholder : MonoBehaviour
{
    public TextMeshProUGUI txtCountdown;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("_lastLevel", SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(DelayLoadNext());
    }

    private IEnumerator DelayLoadNext()
    {
        for (int i = 5; i > -1; i--)
        {
            txtCountdown.text = "Loads next scene in " + i.ToString() + " seconds.";
            yield return new WaitForSeconds(1f);
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
