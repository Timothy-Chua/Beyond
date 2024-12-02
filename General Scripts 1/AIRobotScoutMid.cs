using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRobotScoutMid : AIRobotBasic
{
    [Header("Interactable Object")]
    public InteractableInfo interactableInfo;
    public InteractableObject interactableObject;

    [Header("Line")]
    public GameObject lineRender;

    protected override void Start()
    {
        interactableInfo = GetComponent<InteractableInfo>();
        interactableObject = GetComponent<InteractableObject>();

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
}
