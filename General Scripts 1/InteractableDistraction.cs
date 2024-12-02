using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDistraction : InteractableObject
{
    public Animator animator;
    public Transform distractLocation;
    public EnemyFloor floor;
    public float cooldownTime;
    private bool isInteractable;
    private float currentCooldownTime = 10f;

    protected override void Start()
    {
        currentCooldownTime = cooldownTime;
        isInteractable = true;

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (GameManager.instance.state == GameState.Gameplay)
        {
            if (!isInteractable)
            {
                if (currentCooldownTime >= 0)
                {
                    currentCooldownTime -= Time.deltaTime;
                }
                else
                {
                    currentCooldownTime = cooldownTime;
                    isInteractable = true;
                }
            }
        }
    }

    protected override void Interact()
    {
        base.Interact();

        if (isInteractable)
        {
            animator.SetBool("isActive", true);
            UIManager.instance.QuickReaction("Distraction enabled");

            EnemyManager.instance.Distract(distractLocation, floor);

            UIManager.instance._BtnGameplay();

            isInteractable = false;
        }
        else
        {
            UIManager.instance.QuickReaction("On cooldown.");
        }
    }
}
