using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [Header("Panel")]
    public GameObject panelMain;
    public GameObject panelSettings, panelVolume, panelGamma, panelKeybind, panelCredits, panelOverlay;
    public TextMeshProUGUI txtInfo;
    public Vector3 panelOffset;

    [Header("Render")]
    public Slider sliderBrightness;

    [Header("Audio")]
    public Slider sliderMaster;
    public Slider sliderMusic;
    public Slider sliderSFX;
    public Slider sliderDialogue;

    [Header("Button")]
    public Button btnContinue;

    private float delayTime = 0.75f;
    private bool isDelay;

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
        sliderMaster.value = SettingsManager.instance.volumeMaster;
        sliderMusic.value = SettingsManager.instance.volumeMusic;
        sliderSFX.value = SettingsManager.instance.volumeSFX;
        sliderDialogue.value = SettingsManager.instance.volumeDialogue;
        sliderBrightness.value = SettingsManager.instance.brightnessMult;

        if (PlayerPrefs.GetInt("_lastLevel") > 0)
            btnContinue.gameObject.SetActive(true);
        else
            btnContinue.gameObject.SetActive(false);

        txtInfo.text = null;
        isDelay = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDelay && Input.GetKey(SettingsManager.instance.keyPause))
        {
            if (panelSettings.activeInHierarchy || panelCredits.activeInHierarchy)
            {
                panelSettings.SetActive(false);
                panelCredits.SetActive(false);
                panelMain.SetActive(true);

                StartCoroutine(Delay());
            }
            else if (panelVolume.activeInHierarchy || panelGamma.activeInHierarchy || panelKeybind.activeInHierarchy)
            {
                panelVolume.SetActive(false);
                panelGamma.SetActive(false);
                panelKeybind.SetActive(false);
                panelSettings.SetActive(true);

                StartCoroutine(Delay());
            }
        }

        if (txtInfo.text != null)
            panelOverlay.SetActive(true);
        else
            panelOverlay.SetActive(false);
    }

    public void ChangeVolumeMaster()
    {
        SettingsManager.instance.volumeMaster = sliderMaster.value;
        SettingsManager.instance.ChangeVolumeMaster();
    }

    public void ChangeVolumeMusic()
    {
        SettingsManager.instance.volumeMusic = sliderMusic.value;
        SettingsManager.instance.ChangeVolumeMusic();
    }

    public void ChangeVolumeDialogue()
    {
        SettingsManager.instance.volumeDialogue = sliderDialogue.value;
        SettingsManager.instance.ChangeVolumeDialogue();
    }

    public void ChangeVolumeSFX()
    {
        SettingsManager.instance.volumeSFX = sliderSFX.value;
        SettingsManager.instance.ChangeVolumeSFX();
    }

    public void ChangeBrightness()
    {
        SettingsManager.instance.brightnessMult = sliderBrightness.value;
        SettingsManager.instance.ChangeBrightness();
    }

    public void _BtnContinue()
    {
        // get last scene and level
        SceneManager.LoadScene(PlayerPrefs.GetInt("_lastLevel"));
    }

    public void _BtnNewGame()
    {
        PlayerPrefs.DeleteKey("_pepperSpray");
        PlayerPrefs.DeleteKey("_lastLevel");
        PlayerPrefs.DeleteKey("_SceneEnd");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void _BtnQuit()
    {
        Application.Quit();
    }

    private IEnumerator Delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }
}
