using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5_AI_Manager : MonoBehaviour
{
    public static Level5_AI_Manager instance;
    private bool isStarted;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.SetMainObjective("Head to the [Communication] Department.");
        UIManager.instance.SetSubObjective("Exit through the corridor. [LOC: 2F - R&D]");
        UIManager.instance.ClearReaction();

        isStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            CameraManager.instance.SwitchCam(5);
            isStarted = false;
        }
    }
}
