using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_AI_CameraLock : CameraLock
{
    protected override void Update()
    {
        if (!Level3_AI_Manager.instance.isMeetingKeyCollected)
            base.Update();
    }
}
