using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_2_Manager : MonoBehaviour
{
    public static Level1_2_Manager instance;

    private bool isStarted;
    private bool isDialogueStarted;

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
        isStarted = true;
        //CameraManager.instance.SwitchCam(5);

        UIManager.instance.SetMainObjective("Escape to the [Communication] Department. [LOC: 3F - Corridor]");
        UIManager.instance.ClearSubObjective();
        UIManager.instance.ClearReaction();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            CameraManager.instance.SwitchCam(5);
            isStarted = false;
        }

        if (GameManager.instance.state == GameState.Gameplay)
        {
            if (!isDialogueStarted)
            {
                SoundManager.instance.PlayMultipleDialogue(0, 2);
                isDialogueStarted = true;
            }
        }
    }
}
