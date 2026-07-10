using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterAI : MonoBehaviour
{
    [Header("Variables & References")] [SerializeField]
    GameObject player;

    [SerializeField] MonsterNoises noises;

    [SerializeField] float pathUpdateTimer = 5f;
    [SerializeField] float minimumTimeBeforeModeChange = 1f;
    [SerializeField] float reactionRange = 10f;
    [SerializeField] float hidingDistanceMinimum = 20f;
    [SerializeField] float hidingDistanceMaximum = 40f;
    [SerializeField] float distanceFromPlayer;

    [Tooltip("The higher this number is the lower the chance is")] [SerializeField]
    int chanceToEndHunt = 5;

    [Tooltip("The higher this number is the lower the chance is")] [SerializeField]
    int chanceToEndHiding = 3;

    [Tooltip("The higher this number is the lower the chance is")] [SerializeField]
    int chanceToEndStalking = 2;

    [SerializeField] bool activePath;
    [SerializeField] bool pathPending;
    [SerializeField] float defaultStalkDistance = 15f;
    [SerializeField] float divideStalkDistanceByXPerWaypoint = 3f;
    [SerializeField] int destinationsReachedInCurrentMode = 0;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] float maxHuntTimer = 20;
    [SerializeField] float huntSpeedBoost = 5f;
    [SerializeField] float stickDestroyTime = 5;
    [SerializeField] Animator animator;
    [SerializeField] bool soundPlayedThisHunt = false;
    public bool StickActive;
    public Transform StickPosition;
    float stalkDistance;
    bool hasGainedNewDestination = false;
    float stickTimer;
    float pathTimer;
    float changeCooldown;
    [SerializeField] float currentHunt;

    NavMeshAgent agent;

    //States
    [Serializable]
    public struct Mode
    {
        public static bool Hunting;
        public static bool Hiding;
        public static bool Stalking;
    }

    [SerializeField] bool hunting;
    [SerializeField] bool hiding;
    [SerializeField] bool stalking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        noises = GetComponent<MonsterNoises>();
        stalkDistance = defaultStalkDistance;
        EnterRandomMode();
    }

    void React(bool tooClose)
    {
        if (Mode.Hiding)
        {
            HideBehaviour();
        }
        else if (tooClose || Mode.Hunting)
        {
            agent.speed = math.clamp(agent.speed + huntSpeedBoost, 10, 15);
            //animator.SetBool("isRunning", true);
            Mode.Hiding = false;
            Mode.Stalking = false;
            Mode.Hunting = true;
            if (!soundPlayedThisHunt)
                currentHunt = maxHuntTimer;
        }

        //else if (Mode.Stalking && !agent.hasPath)
        //{
        //    StalkBehaviour();
        //}
        //else
        {
            EnterRandomMode();
        }
    }

    private void HideBehaviour()
    {
        if (agent.pathPending)
            return;
        //Current version has a chance that the hiding spot makes a path that leads right past the player, could affect scariness of monster
        Vector3 hidingPoint =
            new Vector3(player.transform.position.x + Random.Range(hidingDistanceMinimum, hidingDistanceMaximum), 0,
                player.transform.position.z + Random.Range(hidingDistanceMinimum, hidingDistanceMaximum));
        hidingPoint = ReturnPointWithAccurateHeight(hidingPoint);
        //animator.SetBool("isMoving", true);
        agent.SetDestination(hidingPoint);
        pathPending = true;
        if (Random.Range(1, math.clamp(chanceToEndHiding - destinationsReachedInCurrentMode, 1, chanceToEndHiding)) ==
            1)
        {
            EnterRandomMode();
        }
    }


    //private void StalkBehaviour()
    //{
    //    if (pathPending)
    //        return;
    //    Vector3 playerPos = player.transform.position;

    //    Vector3 waypoint = ReturnPointAroundPlayer(playerPos, Random.Range(0, 360), stalkDistance);
    //    waypoint = ReturnPointWithAccurateHeight(waypoint);
    //    agent.SetDestination(waypoint);
    //    pathPending = true;
    //    stalkDistance = math.clamp(stalkDistance / divideStalkDistanceByXPerWaypoint, 5, stalkDistance);
    //    hasGainedNewDestination = true;
    //    if (destinationsReachedInCurrentMode > 2)
    //    {
    //        if (Random.Range(1, math.clamp(chanceToEndStalking - destinationsReachedInCurrentMode, 1, chanceToEndStalking)) == 1)
    //        {
    //            EnterRandomMode();
    //        }
    //    }
    //}

    private Vector3 ReturnPointWithAccurateHeight(Vector3 pointToCheck)
    {
        Physics.Raycast(new Vector3(pointToCheck.x, 20, pointToCheck.z), Vector3.down, out RaycastHit hit, 30f,
            groundLayerMask);
        Debug.DrawRay(new Vector3(pointToCheck.x, 20, pointToCheck.z), Vector3.down * 30, Color.yellow, 20);
        return hit.point;
    }

    private Vector3 ReturnPointAroundPlayer(Vector3 pointToRotateAround, float angle, float radius)
    {
        return new Vector3(pointToRotateAround.x + (math.cos(angle) * radius), pointToRotateAround.y,
            pointToRotateAround.z + (math.sin(angle) * radius));
    }

    private void EnterRandomMode()
    {
        if ((Mode.Hiding || Mode.Hunting) && changeCooldown > 0)
        {
            return;
        }

        if (Mode.Hunting)
        {
            agent.speed = math.clamp(agent.speed - huntSpeedBoost, 10, 15);
            //animator.SetBool("isRunning", false);
        }

        Mode.Hiding = false;
        Mode.Hunting = false;
        Mode.Stalking = false;
        hiding = false;
        hunting = false;
        stalking = false;
        destinationsReachedInCurrentMode = 0;
        stalkDistance = defaultStalkDistance;
        int temp = Random.Range(0, 2);
        switch (temp)
        {
            case 0:
                Mode.Hiding = true;
                hiding = true;
                noises.EndHuntAmbience();
                break;
            case 1:
                agent.speed = math.clamp(agent.speed + huntSpeedBoost, 10, 15);
                //animator.SetBool("isRunning", true);
                Mode.Hunting = true;
                hunting = true;
                if (!soundPlayedThisHunt)
                    currentHunt = maxHuntTimer;
                noises.StartHuntAmbience();
                break;
            case 2:
                Mode.Hiding = true;
                hiding = true;
                noises.EndHuntAmbience();
                break;
            case 3:
                Mode.Hiding = true;
                hiding = true;
                noises.EndHuntAmbience();
                break;
            // case 2:
            //     Mode.Stalking = true;
            //     stalking = true;
            //     break;
            default:
                Mode.Hunting = true;
                hunting = true;
                break;
        }

        changeCooldown = minimumTimeBeforeModeChange;
        if (!agent.hasPath)
        {
            React(false);
        }
    }

    void Update()
    {
        currentHunt = currentHunt - Time.deltaTime;
        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (Mode.Hunting && !StickActive)
        {
            if (currentHunt <= 0)
            {
                noises.EndHuntAmbience();
                soundPlayedThisHunt = false;
                //animator.SetBool("isRunning", false);
                agent.ResetPath();
                EnterRandomMode();
            }

            //nimator.SetBool("isMoving", true);
            if (!soundPlayedThisHunt)
            {
                noises.StartHuntAmbience();
                soundPlayedThisHunt = true;
            }

            agent.SetDestination(player.transform.position);
        }

        agent.autoBraking = false;
        if (StickActive)
        {
            if (StickPosition == null)
                return;
            agent.autoBraking = true;
            noises.EndHuntAmbience();
            soundPlayedThisHunt = false;
            //animator.SetBool("isMoving", true);
            agent.SetDestination(StickPosition.position);
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                stickTimer = stickTimer - Time.deltaTime;
                if (stickTimer <= 0)
                {
                    Destroy(StickPosition.gameObject);
                    stickTimer = stickDestroyTime;
                    agent.ResetPath();
                }
            }

            return;
        }

        changeCooldown = changeCooldown - Time.deltaTime;
        pathTimer = pathTimer - Time.deltaTime;
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        activePath = agent.hasPath;

        if (distanceFromPlayer < reactionRange && changeCooldown <= 0)
        {
            Debug.Log("Player too close, hunting");
            agent.ResetPath();
            React(true);
        }

        if (pathTimer <= 0 && !agent.hasPath && !agent.pathPending)
        {
            pathPending = false;
            React(false);
            pathTimer = pathUpdateTimer;
        }

        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            hasGainedNewDestination = false;
            agent.destination = transform.position;
            agent.ResetPath();
            pathPending = false;
            destinationsReachedInCurrentMode++;
        }
    }
}