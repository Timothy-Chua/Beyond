using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public CameraGroup[] cameraGroups;
    public int currentGroup;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get last current group
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSwitchCameraGroup(int groupNum)
    {
        currentGroup = groupNum;

        for (int i = 0; i < cameraGroups.Length; i++)
        {
            if (groupNum == i)
            {
                cameraGroups[i].camUI.gameObject.SetActive(true);
                cameraGroups[i].gameObject.SetActive(true);
                continue;
            }

            cameraGroups[i].camUI.gameObject.SetActive(false);
            cameraGroups[i].gameObject.SetActive(false);
        }

        cameraGroups[currentGroup].delayClick = false;
    }

    public void NextCameraGroup()
    {
        if (currentGroup >= cameraGroups.Length)
            currentGroup = 0;
        else
            currentGroup++;

        OnSwitchCameraGroup(currentGroup);
    }

    public void PrevCameraGroup()
    {
        if (currentGroup < 0)
            currentGroup = cameraGroups.Length - 1;
        else
            currentGroup--;

        OnSwitchCameraGroup(currentGroup);
    }

    public void NextCam()
    {
        SoundManager.instance.PlayKeyboardSFX();

        cameraGroups[currentGroup].currentCam++;

        if (cameraGroups[currentGroup].currentCam == cameraGroups[currentGroup].securityCameras.Length)
            cameraGroups[currentGroup].currentCam = 0;

        cameraGroups[currentGroup].SwitchCam(cameraGroups[currentGroup].currentCam);
    }

    public void PrevCam()
    {
        SoundManager.instance.PlayKeyboardSFX();

        cameraGroups[currentGroup].currentCam--;

        if (cameraGroups[currentGroup].currentCam < 0)
            cameraGroups[currentGroup].currentCam = cameraGroups[currentGroup].securityCameras.Length - 1;

        cameraGroups[currentGroup].SwitchCam(cameraGroups[currentGroup].currentCam);
    }

    public void SetCamGroupActive(int index)
    {
        OnSwitchCameraGroup(index);
    }

    public void SwitchCam(int camNum)
    {
        cameraGroups[currentGroup].currentCam = camNum;

        cameraGroups[currentGroup].SwitchCam(cameraGroups[currentGroup].currentCam);
    }
}
