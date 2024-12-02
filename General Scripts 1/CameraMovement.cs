using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // This script is referenced to "PIVOT POINT" in the "Camera" prefab
    public Transform cameraHead;

    public float xSensitivity = 50f;
    public float ySensitivity = 25f;

    public float xSpeed = 2f;
    public float ySpeed = 2f;

    public Vector2 xClamps = new Vector2(-180, 0);
    public Vector2 yClamps = new Vector2(-45, 45);

    private float xAccumulator;
    private float yAccumulator;

    private float xRotation;
    private float yRotation;

    private float xRot;
    private float yRot;

    private void Awake()
    {
        xRotation = (xClamps.x + xClamps.y) / 2;
        //yRotation = (yClamps.x + yClamps.y) * 3 / 4;

        xAccumulator = xRotation;
        //yAccumulator = yRotation;

        //CameraMove();
    }

    // Start is called before the first frame update
    void Start()
    {
        /*xRotation = transform.localRotation.z;
        yRotation = cameraHead.localRotation.z;*/
        CameraMove();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.state == GameState.Gameplay || GameManager.instance.state == GameState.Analysis)
        {
            CameraMove();
        }
    }

    void CameraMove()
    {
        transform.localRotation = Quaternion.Euler(transform.rotation.x, xRotation, transform.rotation.z);
        cameraHead.localRotation = Quaternion.Euler(cameraHead.rotation.x, cameraHead.rotation.y, yRotation);
    }

    void GetInput()
    {
        xRot = Input.GetAxisRaw("Horizontal");
        yRot = Input.GetAxisRaw("Vertical");

        xAccumulator = Mathf.Lerp(xAccumulator, xSpeed * xRot, xSensitivity * Time.deltaTime);
        yAccumulator = Mathf.Lerp(yAccumulator, ySpeed * yRot, ySensitivity * Time.deltaTime);

        xRotation += xAccumulator;
        yRotation += yAccumulator;

        xRotation = Mathf.Clamp(xRotation, xClamps.x, xClamps.y);
        yRotation = Mathf.Clamp(yRotation, yClamps.x, yClamps.y);
    }
}
