using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_Biotech_Manager : MonoBehaviour
{
    public static Level3_Biotech_Manager instance;

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
        UIManager.instance.SetMainObjective("Get to the Elevator. [LOC: 3F - R&D]");
        UIManager.instance.ClearSubObjective();
        UIManager.instance.ClearReaction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
