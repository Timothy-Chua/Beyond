using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRobotScout : AIRobotBasic
{
    [Header("Interactable Object")]
    public InteractableInfo interactableInfo;
    public InteractableObject interactableObject;

    [Header("Disable")]
    public bool isBeingDisabled;
    public float disableStartTime = 5f;
    public float lightFlickerStartTime = 1f;

    private float disableTime;
    private float lightFlickerTime;

    [Header("Line")]
    public GameObject lineRender;

    public GameObject robotDisableCollider;

    protected override void Start()
    {
        interactableInfo = GetComponent<InteractableInfo>();
        interactableObject = GetComponent<InteractableObject>();

        isBeingDisabled = false;
        disableTime = disableStartTime;
        lightFlickerTime = lightFlickerStartTime;
        animator.SetBool("isDisabling", false);

        lineRender.SetActive(false);

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (interactableObject.isSelected)
        {
            if (GameManager.instance.state == GameState.Analysis)
                lineRender.SetActive(true);
            else
                lineRender.SetActive(false);
        }
        else
            lineRender.SetActive(false);
    }

    protected override void BehaviorPattern()
    {
        if (!isBeingDisabled)
        {
            animator.SetBool("isDisabling", false);
            base.BehaviorPattern();
        }
        else
        {
            if (disableTime <= 0f)
            {
                interactableInfo.itemDescription = "DISABLED. Unable to move.";
                GameManager.instance.FindActivePlayer().GetComponent<PlayerController>().isDisabling = false;

                audioSource.Stop();
                audioSource.PlayOneShot(SoundManager.instance.robotShutdownSFX);

                txtStateObj.SetActive(false);
                robotLight.enabled = false;
                agent.enabled = false;
                robotDisableCollider.SetActive(false);
                this.enabled = false;
            }
            else
            {
                animator.SetBool("isChasing", false);
                animator.SetBool("isPlayerLost", false);
                animator.SetBool("isDisabling", true);
                Stop();

                GameManager.instance.FindActivePlayer().GetComponent<PlayerController>().isDisabling = true;

                if (lightFlickerTime <= 0f)
                {
                    robotLight.enabled = !robotLight.enabled;
                    lightFlickerTime = lightFlickerStartTime;
                }
                else
                    lightFlickerTime -= Time.deltaTime;

                disableTime -= Time.deltaTime;
            }
        }
    }

    public void DisableRobot()
    {
        isBeingDisabled = true;
    }
}
