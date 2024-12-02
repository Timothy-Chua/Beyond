using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_AI_P2_Manager : MonoBehaviour
{
    public static Level3_AI_P2_Manager instance;

    public bool isPowerSupplyCollected;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        isPowerSupplyCollected = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.SetMainObjective("Find a power supply. [LOC: 2F - RnD]");
        UIManager.instance.SetSubObjective("Use Analysis Mode to locate the power supply.");
        UIManager.instance.ClearReaction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
