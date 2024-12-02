using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioDebug : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AudioSource.isPlaying)
        {
            Debug.Log("Audio is playing");
        }
    }
}
