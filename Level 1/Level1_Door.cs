using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_Door : DoorTriggerAnim
{
    public GameObject colliderNext;

    private void Start()
    {
        colliderNext.SetActive(false);
    }

    private void Update()
    {
        if (Level1_Manager.instance.isKeyCollected)
            colliderNext.SetActive(true);
    }

    protected override void OnTriggerEnter(Collider actor)
    {
        if (Level1_Manager.instance.isKeyCollected)
            base.OnTriggerEnter(actor);

        if (actor.gameObject.CompareTag("Player"))
        {
            if (!Level1_Manager.instance.isKeyCollected)
            {
                if (!(UIManager.instance.txtSubObjective.text == "Find the door key. [LOC: 2F - Meeting]"))
                    UIManager.instance.SetSubObjective("Find the door key. [LOC: 2F - Meeting]");

                SoundManager.instance.PlayErrorSFX();
            }
            else
            {
                /*if (UIManager.instance.txtSubObjective.text == "Find the door key. [LOCATION: 2F - Meeting]")
                {
                    UIManager.instance.ClearSubObjective();
                    //colliderNext.SetActive(true);
                }*/
            }
        }
    }

    protected override void OnTriggerStay(Collider actor)
    {
        if (Level1_Manager.instance.isKeyCollected)
            base.OnTriggerStay(actor);
    }

    protected override void OnTriggerExit(Collider actor)
    {
        if (Level1_Manager.instance.isKeyCollected)
            base.OnTriggerExit(actor);
    }
}
