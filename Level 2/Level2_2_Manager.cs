using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_2_Manager : MonoBehaviour
{
    public static Level2_2_Manager instance;

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
        UIManager.instance.SetMainObjective("Move on to the [AI] Department. [LOC: 2F - Corridor]");
        UIManager.instance.ClearSubObjective();
        UIManager.instance.ClearReaction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
