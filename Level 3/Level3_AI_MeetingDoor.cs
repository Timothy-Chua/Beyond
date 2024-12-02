using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_AI_MeetingDoor : DoorTriggerAnim
{
    protected override void OnTriggerEnter(Collider actor)
    {
        if (Level3_AI_Manager.instance.isMeetingKeyCollected)
            base.OnTriggerEnter(actor);

        if (actor.gameObject.CompareTag("Player"))
        {
            if (!Level3_AI_Manager.instance.isMeetingKeyCollected)
                UIManager.instance.SetSubObjective("Find the meeting door key. [LOC: 1F - Shop]");
            else
                if (UIManager.instance.txtSubObjective.text == "Find the meeting door key. [LOC: 1F - Shop]")
                UIManager.instance.SetSubObjective("Find the corridor door key. [LOC: 1F - Meeting]");
        }
    }

    protected override void OnTriggerStay(Collider actor)
    {
        if (Level3_AI_Manager.instance.isMeetingKeyCollected)
            base.OnTriggerStay(actor);
    }

    protected override void OnTriggerExit(Collider actor)
    {
        if (Level3_AI_Manager.instance.isMeetingKeyCollected)
            base.OnTriggerExit(actor);
    }
}
