using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceControl : MonoBehaviour
{
    private AudioSource m_Source;
    private float localVolume;
    public AudioType audioType;

    private void Awake()
    {
        m_Source = GetComponent<AudioSource>();
        localVolume = m_Source.volume;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeAudio();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (CheckVolume())
            ChangeAudio();
    }

    public void ChangeAudio()
    {
        switch (audioType)
        {
            case AudioType.Music:
                m_Source.volume = localVolume * SettingsManager.instance.volumeMusicActual;
                break;

            case AudioType.SFX:
                m_Source.volume = localVolume * SettingsManager.instance.volumeSFXActual;
                break;

            case AudioType.Dialogue:
                m_Source.volume = localVolume * SettingsManager.instance.volumeDialogueActual;
                break;
        }
    }

    public bool CheckVolume()
    {
        switch (audioType)
        {
            case AudioType.Music:
                if (m_Source.volume != localVolume * SettingsManager.instance.volumeMusicActual)
                    return true;
                break;

            case AudioType.SFX:
                if (m_Source.volume != localVolume * SettingsManager.instance.volumeSFXActual)
                    return true;
                break;

            case AudioType.Dialogue:
                if (m_Source.volume != localVolume * SettingsManager.instance.volumeDialogueActual)
                    return true;
                break;
        }

        return false;
    }
}

public enum AudioType
{
    Music, SFX, Dialogue
}
