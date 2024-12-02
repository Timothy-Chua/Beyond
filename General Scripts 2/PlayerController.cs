using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public AudioSource footSource;
    public GameObject destinationIndicator;

    [Header("Movement Speed")]
    public float speedSneak = 3.5f;             // holds player sneaking speed
    public float speedSprint = 7f;            // holds player sprinting speed

    [Header("Position")]
    public float maxDistance = 25f;
    private Vector3 currentPos;                  // holds player's current position
    private Vector3 targetPos;                   // holds player's target position

    public bool isCrouched;
    public bool isMoving;
    public bool isDisabling;

    private bool isDelay;
    private float delayTime = 0.75f;
    private bool isPaused;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        footSource = GetComponentInChildren<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.isStopped = false;
        isMoving = false;
        currentPos = transform.position;
        isPaused = false;

        isCrouched = GameManager.instance.isPlayerCrouched;
        /*SwitchCrouch();
        SwitchCrouch();*/
        SetDestinationIndicator(false);
        SetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state == GameState.Gameplay)
        {
            isCrouched = GameManager.instance.isPlayerCrouched;

            UnPause();
            UpdateAnimator();
            currentPos = transform.position;

            if (!isDisabling)
            {
                animator.SetBool("isDisabling", false);

                if (Input.GetKeyDown(SettingsManager.instance.keyMove))
                {
                    Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(movePosition, out var hitInfo))
                    {
                        if (hitInfo.transform.gameObject.layer == 10)
                        {
                            if (CheckSelectionDistance(currentPos, hitInfo.point))
                                Move(hitInfo.point);
                            else
                                ErrorReaction("Distance too far.");
                        }
                        else
                            ErrorReaction("Not a valid position.");
                    }
                }

                if (isMoving)
                {
                    if (CheckDestinationDistance(currentPos, targetPos))
                        Stop();

                    if (!footSource.isPlaying)
                        footSource.Play();
                }
                else
                    footSource.Stop();

                if (!isDelay && Input.GetKey(SettingsManager.instance.keyCrouch))
                {
                    SwitchCrouch();
                    StartCoroutine(Delay());
                }
            }
            else
            {
                Stop();
                animator.SetBool("isDisabling", true);

                if (Input.GetKeyDown(SettingsManager.instance.keyMove))
                {
                    ErrorReaction("Currently disabling");
                }

                if (!isDelay && Input.GetKey(SettingsManager.instance.keyCrouch))
                {
                    ErrorReaction("Currently disabling");
                }
            }
        }
        else
        {
            OnPause();
        }
        
    }

    private void SwitchCrouch()
    {
        if (isCrouched)
        {
            GameManager.instance.isPlayerCrouched = false;
            isCrouched = false;
        }
        else
        {
            GameManager.instance.isPlayerCrouched = true;
            isCrouched = true;
        }

        SetSpeed();

        footSource.clip = isCrouched ? SoundManager.instance.playerSneakSFX : SoundManager.instance.playerWalkSFX;
    }

    public void Move(Vector3 targetPos)
    {
        if (agent.isStopped == true)
        {
            agent.autoBraking = true;
            SetSpeed();
            agent.isStopped = false;
        }

        GameManager.instance.isPlayerMoving = true;
        isMoving = true;
        this.targetPos = targetPos;
        agent.SetDestination(targetPos);
        SetDestinationIndicator(true);
    }

    public void Stop()
    {
        if (!agent.isStopped)
        {
            agent.autoBraking = false;
            agent.speed = 0f;
            agent.isStopped = true;
        }

        if (isMoving)
            isMoving = false;

        GameManager.instance.isPlayerMoving = false;
        SetDestinationIndicator(false);
    }
    
    private void SetDestinationIndicator(bool isActive)
    {
        if (isActive)
        {
            destinationIndicator.gameObject.SetActive(true);
            destinationIndicator.transform.position = targetPos;
        }
        else
        {
            destinationIndicator.gameObject.SetActive(false);
        }
    }

    private bool CheckSelectionDistance(Vector3 currentPos, Vector3 finalPos)
    {
        if (Vector3.Distance(currentPos, finalPos) <= maxDistance)
            return true;
        return false;
    }

    private void ErrorReaction(string message)
    {
        SoundManager.instance.PlayErrorSFX();
        UIManager.instance.QuickReaction(message);
    }

    private void UpdateAnimator()
    {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isCrouched", isCrouched);
    }

    private bool CheckDestinationDistance(Vector3 currentPos, Vector3 finalPos)
    {
        if (Vector3.Distance(currentPos, finalPos) <= 0.75f)
            return true;
        return false;
    }

    private void SetSpeed()
    {
        agent.speed = isCrouched ? speedSneak : speedSprint;
    }

    private void OnPause()
    {
        if (animator.speed == 1f)
            animator.speed = 0f;

        if (agent.speed != 0f)
            agent.speed = 0f;

        if (!isPaused)
        {
            footSource.Pause();
            isPaused = true;
        }
    }

    private void UnPause()
    {
        if (animator.speed == 0f)
            animator.speed = 1f;

        if (agent.speed == 0f)
            SetSpeed();

        if (isPaused)
        {
            footSource.UnPause();
            isPaused = false;
        }
    }

    private IEnumerator Delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }
}