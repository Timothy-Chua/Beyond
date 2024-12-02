using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_Biotech_3F_SubUpdate : MonoBehaviour
{
    private void OnTriggerEnter(Collider actor)
    {
        if (GameManager.instance.state == GameState.Gameplay)
        {
            if (actor.gameObject.CompareTag("Player"))
            {
                if (UIManager.instance.txtSubObjective.text != "Find another way to the elevator.")
                    UIManager.instance.SetSubObjective("Find another way to the elevator.");
            }
        }
    }
}
