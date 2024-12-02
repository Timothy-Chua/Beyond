using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_Biotech_P2_Door : DoorTriggerAnim
{
    protected override void OnTriggerEnter(Collider actor)
    {
        if (Level3_Bio_P2_Manager.instance.isDoorKeyCollected)
            base.OnTriggerEnter(actor);

        if (actor.gameObject.CompareTag("Player"))
        {
            if (!Level3_Bio_P2_Manager.instance.isDoorKeyCollected)
                UIManager.instance.SetSubObjective("Find the door key. [LOC: 1F - Shop]");
            else
            {
                UIManager.instance.SetSubObjective("Go to the Head Office. [LOC: 2F - Office]");
            }
                
        }
    }

    protected override void OnTriggerStay(Collider actor)
    {
        if (Level3_Bio_P2_Manager.instance.isDoorKeyCollected)
            base.OnTriggerStay(actor);
    }

    protected override void OnTriggerExit(Collider actor)
    {
        if (Level3_Bio_P2_Manager.instance.isDoorKeyCollected)
            base.OnTriggerExit(actor);
    }
}
