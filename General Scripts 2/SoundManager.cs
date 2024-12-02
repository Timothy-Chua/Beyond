using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource dialoguePlayer;
    public AudioSource musicPlayer;
    public AudioSource sfxPlayer;

    [Header("Dialogue")]
    public DialogueObject[] dialogue;

    [Header("SFX")]
    public AudioClip playerWalkSFX;
    public AudioClip playerSneakSFX;
    public AudioClip guardWalkSFX;
    public AudioClip guardRunSFX;
    public AudioClip robotWalkSFX;
    public AudioClip robotRunSFX;
    public AudioClip robotShutdownSFX;

    [Header("Pepper SFX")]
    public AudioClip canPickupSFX;
    public AudioClip pepperSpraySFX;

    [Header("UI SFX")]
    public AudioClip errorSFX;
    public AudioClip keyboardSFX;
    public AudioClip analysisSFX;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Say(int dialogueIndex)
    {
        DialogueObject currentDialogue = dialogue[dialogueIndex];

        if (dialoguePlayer.isPlaying)
            dialoguePlayer.Stop();

        // updates subtitle and clears after dialogue is complete
        UIManager.instance.SetSubtitle(currentDialogue.subtitle, currentDialogue.clip.length);

        // plays the called dialogue once with the set master volume (see AudioManager)
        dialoguePlayer.PlayOneShot(currentDialogue.clip, dialoguePlayer.volume);
    }

    public virtual void PlayMultipleDialogue(int startIndex, int endIndex)
    {
        StartCoroutine(PlayOneByOne(startIndex, endIndex));
    }

    public virtual IEnumerator PlayOneByOne(int startIndex, int endIndex)
    {
        for (int i = startIndex; i <= endIndex; i++)
        {
            dialoguePlayer.clip = dialogue[i].clip;
            dialoguePlayer.Play();
            UIManager.instance.SetSubtitle(dialogue[i].subtitle, dialogue[i].clip.length);

            while (dialoguePlayer.isPlaying)
            {
                yield return null;
            }

            yield return new WaitForSeconds(2f);
        }
    }

    public void PlayErrorSFX()
    {
        sfxPlayer.PlayOneShot(errorSFX, sfxPlayer.volume);
    }

    public void PlayKeyboardSFX()
    {
        sfxPlayer.PlayOneShot(keyboardSFX, sfxPlayer.volume);
    }

    public void PlayPepperPickupSFX()
    {
        sfxPlayer.PlayOneShot(canPickupSFX, sfxPlayer.volume);
    }

    public void PlayPepperSpraySFX()
    {
        sfxPlayer.PlayOneShot(pepperSpraySFX, sfxPlayer.volume);
    }

    public void PlayAnalysisSFX()
    {
        sfxPlayer.PlayOneShot(analysisSFX, sfxPlayer.volume);
    }
}
