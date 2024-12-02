using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4_Biotech_Manager : MonoBehaviour
{
    public static Level4_Biotech_Manager instance;
    private bool isDialogueStarted;

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
        if (GameManager.instance.state == GameState.Gameplay)
        {
            if (!isDialogueStarted)
            {
                SoundManager.instance.Say(0);
                isDialogueStarted = true;
            }
        }
    }
}
