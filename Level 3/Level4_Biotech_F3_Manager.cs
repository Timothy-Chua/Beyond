using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4_Biotech_F3_Manager : MonoBehaviour
{
    public static Level4_Biotech_F3_Manager instance;

    private bool isStarted;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        isStarted = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.SetMainObjective("Head to the [AI] Department.");
        UIManager.instance.SetSubObjective("Exit through the corridor. [LOC: 3F - Lobby]");
        UIManager.instance.ClearReaction();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            CameraManager.instance.SwitchCam(8);
            isStarted = false;
        }
    }
}
