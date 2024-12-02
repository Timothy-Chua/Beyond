using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_Elev : DoorTriggerAnim
{
    protected override void OnTriggerEnter(Collider actor)
    {
        if (Level2_Manager.instance.isDoorKeyCollected)
            base.OnTriggerEnter(actor);
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
