using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_AI_P2_Door : DoorTriggerAnim
{
    public GameObject colliderNext;

    private void Start()
    {
        colliderNext.SetActive(false);
    }

    protected override void OnTriggerEnter(Collider actor)
    {
        if (Level3_AI_P2_Manager.instance.isPowerSupplyCollected)
            base.OnTriggerEnter(actor);

        if (actor.gameObject.CompareTag("Player"))
        {
            if (!Level3_AI_P2_Manager.instance.isPowerSupplyCollected)
                UIManager.instance.SetSubObjective("Use Analysis Mode to locate the power supply.");
            else
            {
                UIManager.instance.ClearSubObjective();
                colliderNext.SetActive(true);
            }
        }
    }

    protected override void OnTriggerStay(Collider actor)
    {
        if (Level3_AI_P2_Manager.instance.isPowerSupplyCollected)
            base.OnTriggerStay(actor);
    }

    protected override void OnTriggerExit(Collider actor)
    {
        if (Level3_AI_P2_Manager.instance.isPowerSupplyCollected)
            base.OnTriggerExit(actor);
    }
}
