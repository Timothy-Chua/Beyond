using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_Key : MonoBehaviour
{
    public GameObject parent;
    private bool isInteracted;

    private void Start()
    {
        isInteracted = false;
    }

    private void OnTriggerEnter(Collider actor)
    {
        if (actor.gameObject.CompareTag("Player"))
        {
            UIManager.instance.SetReactionText("Press [F] to interact");
        }
    }

    private void OnTriggerStay(Collider actor)
    {
        if (actor.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(SettingsManager.instance.keyInteract) && !isInteracted)
            {
                UIManager.instance.ClearSubObjective();
                UIManager.instance.QuickReaction("Key collected");
                Level1_Manager.instance.isKeyCollected = true;
                parent.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider actor)
    {
        if (actor.gameObject.CompareTag("Player"))
        {
            UIManager.instance.ClearReaction();
        }
    }
}
