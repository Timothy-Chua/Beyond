using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_Manager : MonoBehaviour
{
    public static Level1_Manager instance;

    [Header("Floor 2")]
    public bool isKeyCollected;
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
        isKeyCollected = false;
        UIManager.instance.SetMainObjective("Obtain the [Robotics] Access Key. [LOC: 3F - Workshop]");
        UIManager.instance.ClearSubObjective();
        UIManager.instance.ClearReaction();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state == GameState.Gameplay)
        {
            if (!isDialogueStarted)
            {
                SoundManager.instance.PlayMultipleDialogue(0, 4);
                isDialogueStarted = true;
            }
        }
    }
}
