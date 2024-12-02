using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIEnemy : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource audioSource;
    protected NavMeshAgent agent;
    public AIState state;

    public GameObject txtStateObj;

    [Header("Overhead Text")]
    protected TMP_Text txtState;
    public Color colorIdle = Color.white;
    public Color colorDetect = Color.yellow;
    public Color colorChase = Color.red;

    [Header("Movement")]
    public float startWaitTime = 4;                 //  Wait time of every action
    public float timeToRotate = 2;                  //  Wait time when the enemy detect near the player without seeing
    public float speedWalk = 6;                     //  Walking speed, speed in the nav mesh agent
    public float speedRun = 9;                      //  Running speed

    [Header("View Options")]
    public float viewRadius = 15;                   //  Radius of the enemy view
    public float viewAngle = 90;                    //  Angle of the enemy view

    [Header("Hearing Options")]
    public float listenCloseRadius = 6;                 //  Radius of enemy listening close (Player cannot move)
    public float listenFarRadius = 15;                   //  Radius of enemy listening far (Player cannot run)

    [Header("Mask Assignment")]
    public LayerMask playerMask;                    //  To detect the player with the raycast
    public LayerMask wallMask;                      //  To detect walls with the raycast
    public LayerMask obstacleMask;                  //  To detect the obstacules with the raycast
    public LayerMask groundMask;

    [Header("Mesh Controls")]
    public float meshResolution = 1.0f;             //  How many rays will cast per degree
    public int edgeIterations = 4;                  //  Number of iterations to get a better performance of the mesh filter when the raycast hit an obstacule
    public float edgeDistance = 0.5f;               //  Max distance to calcule the a minumun and a maximum raycast when hits something

    [Header("Audio SFX")]
    public AudioClip walkSFX;
    public AudioClip runSFX;
    public AudioClip gameoverSFX;

    [Header("Waypoints")]
    public Transform[] waypoints;                   //  All the waypoints where the enemy patrols
    public int m_CurrentWaypointIndex;                     //  Current waypoint where the enemy is going to

    [Header("Player Position")]
    public Vector3 playerLastPosition = Vector3.zero;      //  Last position of the player when was near the enemy
    //public Vector3 m_PlayerPosition;                       //  Last position of the player when the player is seen by the enemy

    [SerializeField] protected float m_WaitTime;                               //  Variable of the wait time that makes the delay
    [SerializeField] protected float m_TimeToRotate;                           //  Variable of the wait time to rotate when the player is near that makes the delay
    [SerializeField] protected bool m_playerInSight;                           //  If the player is in range of vision, state of chasing
    [SerializeField] protected bool m_playerInHearingClose;
    [SerializeField] protected bool m_playerInHearingFar;                      //  If the player is near, state of hearing
    [SerializeField] protected bool m_CaughtPlayer;                            //  if the enemy has caught the player

    [SerializeField] protected bool m_isPaused;
    [SerializeField] protected bool m_isStartPatrol;

    protected virtual void Awake()
    {
        txtState = txtStateObj.GetComponent<TMP_Text>();

        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_isPaused = false;
        //m_isStartPatrol = false;

        m_playerInSight = false;
        m_playerInHearingClose = false;
        m_playerInHearingFar = false;
        m_CaughtPlayer = false;

        state = AIState.Idle;

        Move(speedWalk);
        agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        m_isStartPatrol = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GameManager.instance.state == GameState.Gameplay)
        {
            UnPause();

            BehaviorPattern();

            if (GameManager.instance.state == GameState.Gameplay || GameManager.instance.state == GameState.Analysis)
            {
                if (!txtState.gameObject.activeInHierarchy)
                    txtState.gameObject.SetActive(true);
            }
            else
                txtState.gameObject.SetActive(false);
        }
        else
        {
            OnPause();
        }
    }

    protected virtual void BehaviorPattern()
    {
        /// Used to detail the behavior pattern of the AI
        EnvironmentView();

        if (state == AIState.Alerted)
            Chase();
        else
            Patrol();
    }

    protected virtual void Move(float speed)
    {
        /// Sets the navMeshAgent's speed and updates the Animator and AudioSource
        if (agent.isStopped)
        {
            agent.autoBraking = true;
            agent.isStopped = false;
        }

        if (speed == speedRun)
        {
            animator.SetBool("isChasing", true);

            if (!audioSource.isPlaying)
            {
                audioSource.clip = runSFX;
                audioSource.Play();
            }
        }
        else if (speed == speedWalk)
        {
            animator.SetBool("isMoving", true);

            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkSFX;
                audioSource.Play();
            }
        }

        agent.speed = speed;
    }

    protected virtual void NextPoint()
    {
        /// Sets the navMeshAgent's destination to the next patrol point
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    protected virtual void Stop()
    {
        /// Stops the navMeshAgent and updates the Animator and AudioSource
        if (!agent.isStopped)
        {
            agent.autoBraking = false;
            agent.speed = 0f;
            agent.isStopped = true;
        } 

        animator.SetBool("isMoving", false);

        audioSource.Stop();
    }

    protected virtual void OnPause()
    {
        if (animator.speed == 1f)
            animator.speed = 0f;

        if (agent.speed != 0f)
            agent.speed = 0f;

        if (!m_isPaused)
        {
            audioSource.Pause();
            m_isPaused = true;
            m_isStartPatrol = false;
        }
    }

    protected virtual void UnPause()
    {
        if (animator.speed == 0f)
            animator.speed = 1f;

        if (agent.speed == 0f)
            SetSpeed();

        if (m_isPaused)
        {
            audioSource.UnPause();
            m_isPaused = false;
        }
    }

    protected virtual void SetSpeed()
    {
        if (state == AIState.Alerted)
            agent.speed = speedRun;
        else
            agent.speed = speedWalk;
    }

    protected virtual void Chase()
    {
        /// AI Chase behavior Pattern
        Debug.Log("Chasing");

        txtState.text = "!";
        txtState.color = colorChase;

        state = AIState.Alerted;
        //playerLastPosition = m_PlayerPosition;

        if (!m_CaughtPlayer)
        {
            Move(speedRun);
            agent.SetDestination(playerLastPosition);
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, FindClosestPlayer()) >= listenCloseRadius)
            {
                Debug.Log("Back to patrol");

                animator.SetBool("isPlayerLost", false);
                animator.SetBool("isChasing", false);

                StartPatrol();

                m_isStartPatrol = false;

                state = AIState.Idle;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
                
                if (m_WaitTime <= startWaitTime / 2)
                    animator.SetBool("isPlayerLost", true);
                else
                    animator.SetBool("isPlayerLost", false);
            }
        }
        else
        {
            animator.SetBool("isPlayerLost", false);
        }
    }

    protected virtual void Patrol()
    {
        /// AI Patrol behavior Pattern
        if (state == AIState.Detecting)
        {
            //Debug.Log("Detecting");

            txtState.text = "?";
            txtState.color = colorDetect;

            if (m_TimeToRotate <= 0f)
            {
                Move(speedWalk);
                
                // look for player
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
                m_WaitTime = startWaitTime;
            }
        }
        else
        {
            Debug.Log("Patrolling");

            txtState.text = ".";
            txtState.color = colorIdle;

            state = AIState.Idle;
            playerLastPosition = Vector3.zero;

            if (!m_isStartPatrol)
            {
                Debug.Log("Patrol Started");

                animator.SetBool("isPlayerLost", false);
                animator.SetBool("isChasing", false);

                StartPatrol();

                Debug.Log("Patrol Started");
                m_isStartPatrol = true;
            }

            // check if agent is at the stopping distance
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    //Debug.Log("Next Patrol Point");

                    Move(speedWalk);
                    NextPoint();
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    //Debug.Log("Waiting");

                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    protected virtual void StartPatrol()
    {
        Debug.Log("Patrol Started");

        m_TimeToRotate = timeToRotate;
        m_WaitTime = startWaitTime;

        Move(speedWalk);
        agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    protected virtual void LookingPlayer(Vector3 player)
    {
        /// AI Seeking player behavior pattern
        agent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3f)
        {
            if (m_WaitTime <= 0)
            {
                animator.SetBool("isPlayerLost", false);

                StartPatrol();

                m_isStartPatrol = false;
                state = AIState.Idle;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    protected virtual Vector3 FindClosestPlayer()
    {
        /// Method used to find the closest player gameObject
        GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject obj in playerObjs)
        {
            if (obj.activeInHierarchy)
                return obj.transform.position;
        }

        return Vector3.zero;
    }

    protected virtual void EnvironmentView()
    {
        /// AI Detection behavior pattern
        ListenFar();
        ListenClose();
        CheckVision();
    }

    protected virtual void CheckVision()
    {
        /// Checks if the player is in the AI's vision (viewRadius)
        //  Make an overlap sphere around the enemy to detect the playermask in the view radius
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;

            if (!player.gameObject.activeInHierarchy)
                continue;

            // Check if player is in the view angle
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);          //  Distance of the enemy and the player
                if (GameManager.instance.isPlayerCrouched)
                {
                    if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask) &&
                        !Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, wallMask) &&
                        !Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, groundMask))
                    {
                        m_playerInSight = true;           //  The player has been seen by the enemy and then the enemy starts to chasing the player
                        //state = AIState.Alerted;                 //  Change the state to chasing the player
                        //Debug.Log("Player spotted");
                    }
                    else
                    {
                        /*
                         *  If the player is behind an obstacle or a wall the player position will not be registered
                         * */
                        m_playerInSight = false;
                    }
                }
                else
                {
                    if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, wallMask) &&
                        !Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, groundMask))
                    {
                        m_playerInSight = true;             //  The player has been seen by the enemy and then the enemy starts to chasing the player
                        //state = AIState.Alerted;                 //  Change the state to chasing the player
                        //Debug.Log("Player spotted");
                    }
                    else
                    {
                        /*
                         *  If the player is behind a wall the player position will not be registered
                         */
                        m_playerInSight = false;
                    }
                }
            }

            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                /*
                 *  If the player is further than the view radius, then the enemy will no longer keep the player's current position.
                 *  Or the enemy is a safe zone, the enemy will not chase
                 * */
                m_playerInSight = false;                //  Change the state of chasing
            }

            if (m_playerInSight)
            {
                /*
                 *  If the enemy no longer sees the player, then the enemy will go to the last position that has been registered
                 * */
                playerLastPosition = player.transform.position;       //  Save the player's current position if the player is in range of vision
                state = AIState.Alerted;
            }
        }
    }

    protected virtual void ListenClose()
    {
        /// Used to check if the player is moving within the listenCloseRadius range
        //  Make an overlap sphere around the enemy to detect the playermask in the listen radius
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, listenCloseRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            PlayerController playerController = playerInRange[i].gameObject.GetComponent<PlayerController>();

            if (!player.gameObject.activeInHierarchy)
                continue;

            if (!GameManager.instance.isPlayerCrouched || GameManager.instance.isPlayerMoving)
                m_playerInHearingClose = true;
            else
                m_playerInHearingClose = false;

            if (Vector3.Distance(transform.position, player.position) > listenCloseRadius)
            {
                /*
                 *  If the player is further than the view radius, then the enemy will no longer keep the player's current position.
                 *  Or the enemy is a safe zone, the enemy will not chase
                 * */
                m_playerInHearingClose = false;                //  Change the state of chasing
            }

            if (m_playerInHearingClose && !m_playerInSight)
            {
                /*
                 *  If the enemy no longer sees the player, then the enemy will go to the last position that has been registered
                 * */
                playerLastPosition = player.transform.position;       //  Save the player's current position if the player is in range of vision
                state = AIState.Detecting;
            }
        }
    }

    protected virtual void ListenFar()
    {
        /// Used to check if the player is running within the listenFarRadius range
        //  Make an overlap sphere around the enemy to detect the playermask in the listen radius
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, listenFarRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            PlayerController playerController = playerInRange[i].gameObject.GetComponent<PlayerController>();

            if (!player.gameObject.activeInHierarchy)
                continue;

            if (!GameManager.instance.isPlayerCrouched && GameManager.instance.isPlayerMoving)
                m_playerInHearingFar = true;
            else
                m_playerInHearingFar = false;

            if (Vector3.Distance(transform.position, player.position) > listenFarRadius)
            {
                /*
                 *  If the player is further than the view radius, then the enemy will no longer keep the player's current position.
                 *  Or the enemy is a safe zone, the enemy will not chase
                 * */
                m_playerInHearingFar = false;                //  Change the state of chasing
            }

            if (m_playerInHearingFar && !m_playerInSight)
            {
                /*
                 *  If the enemy no longer sees the player, then the enemy will go to the last position that has been registered
                 * */
                playerLastPosition = player.transform.position;       //  Save the player's current position if the player is in range of vision
                state = AIState.Detecting;
            }
        }
    }

    protected virtual void OnCollisionEnter(Collision actor)
    {
        if (actor.gameObject.CompareTag("Player") && GameManager.instance.state == GameState.Gameplay && state == AIState.Alerted)
        {
            m_CaughtPlayer = true;
            SoundManager.instance.sfxPlayer.PlayOneShot(gameoverSFX, SoundManager.instance.sfxPlayer.volume);
            GameManager.instance.state = GameState.GameOver;
        }
    }
}

public enum AIState
{
    Idle, Detecting, Alerted
}