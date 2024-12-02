using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDisable : MonoBehaviour
{
    public AIRobotScout robotScout;

    // Start is called before the first frame update
    void Start()
    {
        robotScout = GetComponentInParent<AIRobotScout>();
    }

    private void OnTriggerEnter(Collider actor)
    {
        if (GameManager.instance.state == GameState.Gameplay)
        {
            if (actor.gameObject.CompareTag("Player"))
            {
                UIManager.instance.txtReaction.text = "Press [F] to disable.";
            }
        }
    }

    private void OnTriggerStay(Collider actor)
    {
        if (GameManager.instance.state == GameState.Gameplay)
        {
            if (actor.gameObject.CompareTag("Player") && !robotScout.isBeingDisabled)
            {
                if (Input.GetKeyDown(SettingsManager.instance.keyInteract))
                {
                    UIManager.instance.ClearReaction();
                    GameManager.instance.FindActivePlayer().GetComponent<PlayerController>().isDisabling = true;
                    robotScout.txtStateObj.SetActive(false);
                    robotScout.isBeingDisabled = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider actor)
    {
        if (GameManager.instance.state == GameState.Gameplay)
        {
            if (actor.gameObject.CompareTag("Player"))
            {
                UIManager.instance.ClearReaction();
            }
        }
    }
}
