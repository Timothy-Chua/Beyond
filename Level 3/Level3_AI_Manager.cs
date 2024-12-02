using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_AI_Manager : MonoBehaviour
{
    public static Level3_AI_Manager instance;

    public bool isMeetingKeyCollected;
    public bool isExitKeyCollected;

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
        isMeetingKeyCollected = false;
        isExitKeyCollected = false;

        UIManager.instance.SetMainObjective("Get to the [Biotech] Department. [LOC: 2F - Corridor]");
        UIManager.instance.ClearSubObjective();
        UIManager.instance.ClearReaction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
