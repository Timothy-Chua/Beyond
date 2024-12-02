using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_Door : DoorTriggerAnim
{
    protected override void OnTriggerEnter(Collider actor)
    {
        if (Level2_Manager.instance.isDoorKeyCollected)
            base.OnTriggerEnter(actor);

        if (actor.gameObject.CompareTag("Player"))
        {
            if (!Level2_Manager.instance.isDoorKeyCollected)
                UIManager.instance.SetSubObjective("Find the door key. [LOC: 1F - R&D]");
            else
                UIManager.instance.ClearSubObjective();
        }
    }

    protected override void OnTriggerStay(Collider actor)
    {
        if (Level2_Manager.instance.isDoorKeyCollected)
            base.OnTriggerStay(actor);
    }

    protected override void OnTriggerExit(Collider actor)
    {
        if (Level2_Manager.instance.isDoorKeyCollected)
            base.OnTriggerExit(actor);
    }
}
