using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_Manager : MonoBehaviour
{
    public static Level2_Manager instance;

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
        isDoorKeyCollected = false;
        UIManager.instance.SetMainObjective("Obtain the [Communication] Access Key. [LOC: 2F - Office]");
        UIManager.instance.ClearSubObjective();
        UIManager.instance.ClearReaction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
