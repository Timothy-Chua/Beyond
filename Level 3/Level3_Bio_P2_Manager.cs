using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_Bio_P2_Manager : MonoBehaviour
{
    public static Level3_Bio_P2_Manager instance;
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
        UIManager.instance.SetMainObjective("Obtain the [Biotech] Access Key. [LOC: 2F - Head Office]");
        UIManager.instance.ClearSubObjective();
        UIManager.instance.ClearReaction();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
