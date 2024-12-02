using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4_AI_Door : DoorTriggerAnim
{
    private bool isFirstTime;

    private void Start()
    {
        isFirstTime = true;
    }

    protected override void OnTriggerEnter(Collider actor)
    {
        if (Level4_AI_Manager.instance.isDoorKeyCollected)
            base.OnTriggerEnter(actor);

        if (actor.gameObject.CompareTag("Player"))
        {
            if (!Level4_AI_Manager.instance.isDoorKeyCollected)
            {
                UIManager.instance.SetSubObjective("Find the door key. [LOC: 3F - Head Lounge]");

                if (isFirstTime)
                {
                    SoundManager.instance.PlayMultipleDialogue(0, 1);
                    isFirstTime = false;
                }
            }
            else
            {
                UIManager.instance.ClearSubObjective();
            }
        }
    }

    protected override void OnTriggerStay(Collider actor)
    {
        if (Level4_AI_Manager.instance.isDoorKeyCollected)
            base.OnTriggerStay(actor);
    }

    protected override void OnTriggerExit(Collider actor)
    {
        if (Level4_AI_Manager.instance.isDoorKeyCollected)
            base.OnTriggerExit(actor);
    }
}
