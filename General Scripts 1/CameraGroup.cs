using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGroup : MonoBehaviour
{
    //public static CameraGroup instance;

    public SecurityCamera[] securityCameras;
    public int currentCam;
    public CameraIndicator camUI;

    public bool isNextPressed;
    public bool isPreviousPressed;

    public bool delayClick;

    private void Awake()
    {
        /*if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);*/

        foreach (SecurityCamera sc in securityCameras)
        {
            sc.GetCameraMovement();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        delayClick = false;
        // get last cam
        currentCam = 0;

        SwitchCam(currentCam);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state == GameState.Gameplay || GameManager.instance.state == GameState.Analysis)
        {
            if (!delayClick)
            {
                if (Input.GetKey(SettingsManager.instance.keyCamNext))
                {
                    NextCam();
                    StartCoroutine(Delay());
                }

                if (Input.GetKey(SettingsManager.instance.keyCamPrev))
                {
                    PrevCam();
                    StartCoroutine(Delay());
                }
            }
            
        }
    }

    public void NextCam()
    {
        SoundManager.instance.PlayKeyboardSFX();

        isNextPressed = true;
        isPreviousPressed = false;

        currentCam++;

        if (currentCam == securityCameras.Length)
            currentCam = 0;

        SwitchCam(currentCam);
    }

    public void PrevCam()
    {
        SoundManager.instance.PlayKeyboardSFX();

        isNextPressed = false;
        isPreviousPressed = true;

        currentCam--;

        if (currentCam < 0)
            currentCam = securityCameras.Length - 1;

        SwitchCam(currentCam);
    }

    public void SwitchCam(int cameraNext)
    {
        for (int i = 0; i < securityCameras.Length; i++)
        {
            if (i == cameraNext)
            {
                securityCameras[i].camera.enabled = true;
                securityCameras[i].movement.enabled = true;
                continue;
            }

            securityCameras[i].camera.enabled = false;
            securityCameras[i].movement.enabled = false;
        }

        camUI.ChangeCam(cameraNext);
    }

    public IEnumerator Delay()
    {
        delayClick = true;

        yield return new WaitForSeconds(0.5f);

        delayClick = false;
    }
}
