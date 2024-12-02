using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_AI_CorridorDoor : DoorTriggerAnim
{
    public GameObject colliderNext;

    private void Start()
    {
        colliderNext.SetActive(false);
    }

    protected override void OnTriggerEnter(Collider actor)
    {
        if (Level3_AI_Manager.instance.isExitKeyCollected)
            base.OnTriggerEnter(actor);

        if (actor.gameObject.CompareTag("Player"))
        {
            if (!Level3_AI_Manager.instance.isExitKeyCollected)
                UIManager.instance.SetSubObjective("Find the corridor door key. [LOC: 1F - Meeting Room]");
            else
            {
                UIManager.instance.ClearSubObjective();
                colliderNext.SetActive(true);
            }
        }
    }

    protected override void OnTriggerStay(Collider actor)
    {
        if (Level3_AI_Manager.instance.isExitKeyCollected)
            base.OnTriggerStay(actor);
    }

    protected override void OnTriggerExit(Collider actor)
    {
        if (Level3_AI_Manager.instance.isExitKeyCollected)
            base.OnTriggerExit(actor);
    }
}
