using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_AI_P2_PowerSupply : MonoBehaviour
{
    public GameObject parentObj;

    private void OnTriggerEnter(Collider actor)
    {
        if (actor.gameObject.CompareTag("Player") && GameManager.instance.state == GameState.Gameplay)
        {
            UIManager.instance.SetReactionText("Press [F] to interact");
        }
    }

    private void OnTriggerStay(Collider actor)
    {
        if (actor.gameObject.CompareTag("Player") && GameManager.instance.state == GameState.Gameplay)
        {
            if (Input.GetKey(SettingsManager.instance.keyInteract))
            {
                Level3_AI_P2_Manager.instance.isPowerSupplyCollected = true;
                UIManager.instance.SetMainObjective("Return to the Biotech Department.");
                UIManager.instance.ClearSubObjective();
                UIManager.instance.QuickReaction("Power supply collected");
                parentObj.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider actor)
    {
        if (actor.gameObject.CompareTag("Player") && GameManager.instance.state == GameState.Gameplay)
        {
            UIManager.instance.ClearReaction();
        }
    }
}
