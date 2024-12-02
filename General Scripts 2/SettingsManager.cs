using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    [Header("Render")]
    public Volume volume;
    public ColorAdjustments colorAdjustments;
    private float baseBrightness;
    public bool isNightVisionPresent;

    [Header("Keybinds")]
    public KeyCode keyPause = KeyCode.Escape;
    public KeyCode keyInteract = KeyCode.F;
    public KeyCode keySelect = KeyCode.Mouse0;
    public KeyCode keyMove = KeyCode.Mouse1;
    public KeyCode keyCrouch = KeyCode.LeftShift;
    public KeyCode keyAnalysis = KeyCode.V;
    public KeyCode keyCamNext = KeyCode.E;
    public KeyCode keyCamPrev = KeyCode.Q;
    public KeyCode keyConfirm = KeyCode.Return;

    [Header("Volume")]
    public float volumeMaster = 1.0f;
    public float volumeMusic = 0.6f;
    public float volumeSFX = 0.75f;
    public float volumeDialogue = 0.8f;

    public float volumeMusicActual, volumeSFXActual, volumeDialogueActual;

    [Header("Display")]
    public float brightnessMult = 1;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        if (!isNightVisionPresent)
        {
            if (!volume.profile.TryGet(out colorAdjustments)) throw new System.NullReferenceException(nameof(colorAdjustments));
            baseBrightness = colorAdjustments.postExposure.value;
        }

        if (PlayerPrefs.GetInt("_isFirstTime") == 0)
        {
            PresetSetting();
            PlayerPrefs.SetInt("_isFirstTime", 1);
        }

        GetSettingValues();
        ChangeVolumeMaster();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeVolumeMaster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isNightVisionPresent)
        {
            if (!volume.profile.TryGet(out colorAdjustments)) throw new System.NullReferenceException(nameof(colorAdjustments));
            baseBrightness = colorAdjustments.postExposure.value;
        }
    }

    public void ChangeVolumeMaster()
    {
        volumeMusicActual = volumeMusic * volumeMaster;
        volumeSFXActual = volumeSFX * volumeMaster;
        volumeDialogueActual = volumeDialogue * volumeMaster;

        PlayerPrefs.SetFloat("_volMaster", volumeMaster);
        PlayerPrefs.SetFloat("_volMusic", volumeMusic);
        PlayerPrefs.SetFloat("_volSFX", volumeSFX);
        PlayerPrefs.SetFloat("_volDialogue", volumeDialogue);
    }

    public void ChangeVolumeMusic() 
    { 
        volumeMusicActual = volumeMusic * volumeMaster;
        PlayerPrefs.SetFloat("_volMusic", volumeMusic);

        try
        {
            SoundManager.instance.musicPlayer.volume = volumeMusicActual;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void ChangeVolumeSFX() 
    { 
        volumeSFXActual = volumeSFX * volumeMaster;
        PlayerPrefs.SetFloat("_volSFX", volumeSFX);
    }

    public void ChangeVolumeDialogue() 
    { 
        volumeDialogueActual = volumeDialogue * volumeMaster;
        PlayerPrefs.SetFloat("_volDialogue", volumeDialogue);

        try
        {
            SoundManager.instance.dialoguePlayer.volume = volumeDialogueActual;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void ChangeBrightness()
    {
        colorAdjustments.postExposure.value = baseBrightness * brightnessMult;

        PlayerPrefs.SetFloat("_brightness", brightnessMult);
    }

    public void GetSettingValues()
    {
        brightnessMult = PlayerPrefs.GetFloat("_brightness");
        volumeMaster = PlayerPrefs.GetFloat("_volMaster");
        volumeMusic = PlayerPrefs.GetFloat("_volMusic");
        volumeDialogue = PlayerPrefs.GetFloat("_volDialogue");
        volumeSFX = PlayerPrefs.GetFloat("_volSFX");
    }

    public void PresetSetting()
    {
        brightnessMult = 1f;
        volumeMaster = 1.0f;
        volumeMusic = 0.6f;
        volumeSFX = 0.75f;
        volumeDialogue = 0.8f;

        SaveSettingValues();
    }

    public void SaveSettingValues()
    {
        PlayerPrefs.SetFloat("_volMaster", volumeMaster);
        PlayerPrefs.SetFloat("_volMusic", volumeMusic);
        PlayerPrefs.SetFloat("_volSFX", volumeSFX);
        PlayerPrefs.SetFloat("_volDialogue", volumeDialogue);
        PlayerPrefs.SetFloat("_brightness", brightnessMult);
    }
}
