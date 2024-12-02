using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SecurityCamera
{
    public GameObject cameraObj;
    public Camera camera;
    public CameraMovement movement;

    public void GetCameraMovement()
    {
        movement = cameraObj.GetComponentInChildren<CameraMovement>(true);
        camera = movement.gameObject.GetComponentInChildren<Camera>(true);
    }
}
