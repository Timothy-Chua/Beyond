using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_Bio_2F_Manager : MonoBehaviour
{
    public static Level3_Bio_2F_Manager instance;
    private bool isStarted;

    public void Awake()
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
        UIManager.instance.SetSubObjective("Exit through the elevator. [LOC: 2F - Office]");
        UIManager.instance.ClearReaction();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            CameraManager.instance.SwitchCam(4);
            isStarted = false;
        }
    }
}
