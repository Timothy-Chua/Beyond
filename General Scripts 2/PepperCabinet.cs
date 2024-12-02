using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperCabinet : MonoBehaviour
{
    private void OnTriggerEnter(Collider actor)
    {
        if (actor.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.currentPepperSprays == 3)
            {
                UIManager.instance.QuickReaction("Already at MAX [Pepper Spray]s");
                SoundManager.instance.PlayErrorSFX();
            }
            else
            {
                SoundManager.instance.PlayPepperPickupSFX();
                GameManager.instance.currentPepperSprays = 3;
                UIManager.instance.QuickReaction("Replenished [Pepper Spray]s");
            }
        }
    }
}
