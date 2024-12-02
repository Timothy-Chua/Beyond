using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificCamActive : MonoBehaviour
{
    public int index;
    private bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            CameraManager.instance.SwitchCam(index);
            isStart = false;
        }
    }
}
