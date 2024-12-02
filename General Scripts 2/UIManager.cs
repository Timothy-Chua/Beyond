using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Panels")]
    public GameObject panelGame;
    public GameObject panelMenu;
    public GameObject panelHelp;
    public GameObject panelSettings;
    public GameObject panelGameOver;
    public GameObject panelTutorial;

    [Header("Analysis")]
    public GameObject analysisOverlay;
    public Slider analysisSlider;
    public Button btnAnalysis;
    public Sprite activeAnalysis;
    public Sprite deactiveAnalysis;

    [Header("Settings")]
    public Slider sliderMaster;
    public Slider sliderMusic;
    public Slider sliderSFX;
    public Slider sliderDialogue;
    public Slider sliderBrightness;

    [Header("In Game")]
    public TextMeshProUGUI txtSprays;
    public TextMeshProUGUI txtReaction;
    public float reactionClearDelay = 2f;
    public TextMeshProUGUI txtSubtitle;
    public TextMeshProUGUI txtMainObjective;
    public TextMeshProUGUI txtSubObjective;

    [Header("Interaction")]
    public GameObject panelInteraction;
    public TextMeshProUGUI txtItemName;
    public TextMeshProUGUI txtItemDescription;
    public Vector3 panelInteractionOffset;

    [Header("Camera")]
    public Sprite activeCam;
    public Sprite deactiveCam;

    private GameState previousState;
    private bool isDelay;
    private float reactionTimer;
    private bool isQuickReaction;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        sliderMaster.value = SettingsManager.instance.volumeMaster;
        sliderMusic.value = SettingsManager.instance.volumeMusic;
        sliderSFX.value = SettingsManager.instance.volumeSFX;
        sliderDialogue.value = SettingsManager.instance.volumeDialogue;
        sliderBrightness.value = SettingsManager.instance.brightnessMult;

        isDelay = false;

        if (!GameManager.instance.isTutorialPresent)
            _BtnGameplay();
        else
            _BtnTutorial();

        analysisSlider.maxValue = GameManager.instance.maxAnalysisTime;
        analysisSlider.value = GameManager.instance.currentAnalysisTime;

        txtItemName.text = null;
        txtItemDescription.text = null;

        isQuickReaction = false;

        panelInteraction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.instance.state)
        {
            case GameState.Gameplay:

                UpdateAnalysisSlider();
                UpdatePepperSpray();

                panelInteraction.SetActive(false);

                if (Input.GetKey(SettingsManager.instance.keyPause))
                {
                    if (!isDelay)
                    {
                        previousState = GameState.Gameplay;
                        _BtnPause();
                        StartCoroutine(Delay());
                    }
                }

                if (isQuickReaction)
                {
                    if (reactionTimer <= 0)
                    {
                        txtReaction.text = null;
                    }
                    else
                    {
                        reactionTimer -= Time.deltaTime;
                    }
                }

                break;

            case GameState.Analysis:

                UpdateAnalysisSlider();
                UpdatePepperSpray();

                if (txtItemName.text != null || txtItemDescription.text != null)
                    panelInteraction.SetActive(true);
                else
                    panelInteraction.SetActive(false);

                if (Input.GetKey(SettingsManager.instance.keyPause))
                {
                    if (!isDelay)
                    {
                        previousState = GameState.Analysis;
                        _BtnPause();
                        StartCoroutine(Delay());
                    }
                }

                break;

            case GameState.Paused:

                if (Input.GetKey(SettingsManager.instance.keyPause))
                {
                    if (!isDelay)
                    {
                        if (panelMenu.activeSelf)
                        {
                            if (previousState == GameState.Analysis)
                                _BtnAnalysis();
                            else if (previousState == GameState.Gameplay)
                                _BtnGameplay();
                        }
                        else
                        {
                            _BtnReturnPause();
                        }

                        StartCoroutine(Delay());
                    } 
                }

                break;

            case GameState.GameOver:

                panelGameOver.SetActive(true);
                GameManager.instance.state = GameState.GameOver;

                break;

            case GameState.Tutorial:

                panelTutorial.SetActive(true);
                GameManager.instance.state = GameState.Tutorial;

                break;
        }
    }

    private IEnumerator Delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(0.75f);
        isDelay = false;
    }

    public void SwitchAnalysis()
    {
        if (GameManager.instance.state == GameState.Gameplay)
        {
            _BtnAnalysis();
        }
        else if (GameManager.instance.state == GameState.Analysis)
        {
            _BtnGameplay();
        }

        SoundManager.instance.PlayAnalysisSFX();
    }

    public virtual void SetSubtitle(string subtitle)
    {
        txtSubtitle.text = subtitle;
    }

    public virtual void SetSubtitle(string subtitle, float delay)
    {
        // Sets subtitle to assigned string (see SoundManager)
        txtSubtitle.text = subtitle;
        StartCoroutine(ClearAfterSeconds(delay));
    }

    public virtual void ClearSubtitle()
    {
        txtSubtitle.text = string.Empty;
    }

    public virtual IEnumerator ClearAfterSeconds(float delay)
    {
        // Clears subtitle after delay = clip length (see SoundManager)
        yield return new WaitForSeconds(delay);
        ClearSubtitle();
    }

    public void UpdateAnalysisSlider()
    {
        analysisSlider.value = GameManager.instance.currentAnalysisTime;
    }

    public void UpdatePepperSpray()
    {
        txtSprays.text = GameManager.instance.currentPepperSprays.ToString();
    }

    public void _BtnGameplay()
    {
        GameManager.instance.state = GameState.Gameplay;

        btnAnalysis.GetComponent<Image>().sprite = deactiveAnalysis;
        analysisOverlay.SetActive(false);

        panelGame.SetActive(true);
        panelMenu.SetActive(false);
        panelTutorial.SetActive(false);
    }

    public void _BtnPause()
    {
        GameManager.instance.state = GameState.Paused;

        panelGame.SetActive(false);
        panelMenu.SetActive(true);
    }

    public void _BtnAnalysis()
    {
        GameManager.instance.state = GameState.Analysis;

        btnAnalysis.GetComponent<Image>().sprite = activeAnalysis;
        analysisOverlay.SetActive(true);

        panelGame.SetActive(true);
        panelMenu.SetActive(false);
        panelTutorial.SetActive(false);
        SoundManager.instance.PlayAnalysisSFX();
    }

    public void _BtnReturnPause()
    {
        panelMenu.SetActive(true);
        panelHelp.SetActive(false);
        panelSettings.SetActive(false);
    }

    public void _BtnMainMenu()
    {
        SettingsManager.instance.SaveSettingValues();
        SceneManager.LoadScene(0);
    }

    public void _BtnRestart()
    {
        SettingsManager.instance.SaveSettingValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void _BtnTutorial()
    {
        panelGame.SetActive(false);
        panelMenu.SetActive(false);
        panelHelp.SetActive(false);
        panelSettings.SetActive(false);
        panelTutorial.SetActive(true);
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

    public void QuickReaction(string reaction)
    {
        reactionTimer = reactionClearDelay;
        isQuickReaction = true;
        txtReaction.text = reaction;
    }

    public void SetReactionText(string text)
    {
        reactionTimer = reactionClearDelay;
        isQuickReaction = false;
        txtReaction.text = text;
    }

    public void SetMainObjective(string text)
    {
        txtMainObjective.text = text;
    }

    public void SetSubObjective(string text)
    {
        txtSubObjective.text = text;
    }

    public void ClearMainObjective()
    {
        txtMainObjective.text = null;
    }

    public void ClearSubObjective()
    {
        txtSubObjective.text = null;
    }

    public void ClearReaction()
    {
        if (!isQuickReaction)
            txtReaction.text = null;
    }

    public void PlayInteractSFX()
    {
        SoundManager.instance.PlayKeyboardSFX();
    }

    public void _BtnCameraNext()
    {
        CameraManager.instance.NextCam();
    }

    public void _BtnCameraPrev()
    {
        CameraManager.instance.PrevCam();
    }
}
