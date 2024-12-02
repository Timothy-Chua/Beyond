using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGuard : AIEnemy
{
    [Header("Pepper Spray")]
    public float stunTime = 8f;
    public bool isStunned;
    public float stunCurrentTime;

    protected override void Start()
    {
        walkSFX = SoundManager.instance.guardWalkSFX;
        runSFX = SoundManager.instance.guardRunSFX;
        stunCurrentTime = stunTime;

        base.Start();
    }

    protected override void BehaviorPattern()
    {
        if (!isStunned)
        {
            stunCurrentTime = stunTime;
            base.BehaviorPattern();
        }
        else
        {
            m_WaitTime = startWaitTime;
            m_TimeToRotate = timeToRotate;

            if (stunCurrentTime <= 0)
            {
                animator.SetBool("isStunned", false);
                isStunned = false;
            }
            else
            {
                animator.SetBool("isStunned", true);
                stunCurrentTime -= Time.deltaTime;
            }
        }
    }

    protected override void EnvironmentView()
    {
        if (!isStunned)
            base.EnvironmentView();
    }

    protected override void OnCollisionEnter(Collision actor)
    {
        if (state == AIState.Alerted && actor.gameObject.CompareTag("Player"))
        {
            if (!isStunned)
            {
                if (GameManager.instance.currentPepperSprays > 0)
                    Stun();
                else
                {
                    base.OnCollisionEnter(actor);
                }
            }
        }
    }

    private void Stun()
    {
        GameManager.instance.ConsumePepperSpray();
        SoundManager.instance.PlayPepperSpraySFX();

        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        isStunned = true;
    }
}
