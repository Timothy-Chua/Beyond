using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4_AI_Manager : MonoBehaviour
{
    public static Level4_AI_Manager instance;

    public bool isDoorKeyCollected;

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
        UIManager.instance.SetMainObjective("Obtain the [AI] Access Key. [LOC: 3F - Head Office]");
        UIManager.instance.ClearSubObjective();
        UIManager.instance.ClearReaction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
