using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    private CameraGroup cameraGroup;
    public int lockedIndex;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        cameraGroup = GetComponent<CameraGroup>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (cameraGroup.currentCam == lockedIndex)
        {
            if (cameraGroup.isNextPressed)
                cameraGroup.NextCam();
            if (cameraGroup.isPreviousPressed)
                cameraGroup.PrevCam();
        }
    }
}
