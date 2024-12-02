using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperSpray : MonoBehaviour
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
                UIManager.instance.QuickReaction("Pepper Spray Collected");
                GameManager.instance.currentPepperSprays++;
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
