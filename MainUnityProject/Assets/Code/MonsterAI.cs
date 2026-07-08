using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterAI : MonoBehaviour
{
    [Header("Variables & References")] 
    [SerializeField] GameObject player;

    [SerializeField] float pathUpdateTimer = 5f;
    [SerializeField] float minimumTimeBeforeModeChange = 1f;
    [SerializeField] float reactionRange = 10f;
    [SerializeField] float hidingDistanceMinimum = 20f;
    [SerializeField] float hidingDistanceMaximum = 40f;
    [SerializeField] float distanceFromPlayer;
    [Tooltip("The higher this number is the lower the chance is")][SerializeField] int chanceToEndHunt = 5;
    [Tooltip("The higher this number is the lower the chance is")][SerializeField] int chanceToEndHiding = 3;
    [Tooltip("The higher this number is the lower the chance is")][SerializeField] int chanceToEndStalking = 2;
    [SerializeField] bool activePath;
    [SerializeField] bool pathPending;
    [SerializeField] float defaultStalkDistance = 15f;
    [SerializeField] float divideStalkDistanceByXPerWaypoint = 3f;
    [SerializeField] int destinationsReachedInCurrentMode = 0;
    [SerializeField] LayerMask groundLayerMask;
    float stalkDistance;
    bool hasGainedNewDestination = false;
    float pathTimer;
    float changeCooldown;
    NavMeshAgent agent;
    //States
    [Serializable]public struct Mode
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
        stalkDistance = defaultStalkDistance;
        EnterRandomMode();
    }

    void React()
    {
        if (Mode.Hunting && !agent.hasPath)
        {
            HuntBehaviour();
        }
        else if (Mode.Hiding && !agent.hasPath)
        {
            HideBehaviour();
        }
        else if (Mode.Stalking && !agent.hasPath)
        {
            StalkBehaviour();
        }
        else
        {
            EnterRandomMode();
        }
    }
    private void HideBehaviour()
    {
        if (pathPending)
            return;
        //Current version has a chance that the hiding spot makes a path that leads right past the player, could affect scariness of monster
        Vector3 hidingPoint =
            new Vector3(player.transform.position.x + Random.Range(hidingDistanceMinimum, hidingDistanceMaximum), 0, player.transform.position.z + Random.Range(hidingDistanceMinimum, hidingDistanceMaximum));
        hidingPoint = ReturnPointWithAccurateHeight(hidingPoint);
        agent.SetDestination(hidingPoint);
        pathPending = true;
        hasGainedNewDestination = true;
        if (Random.Range(1, math.clamp(chanceToEndHiding - destinationsReachedInCurrentMode, 1, chanceToEndHiding)) == 1)
        {
            EnterRandomMode();
        }
    }

    private void HuntBehaviour()
    {
        if (pathPending)
            return;
        agent.SetDestination(player.transform.position);
        pathPending = true;
        hasGainedNewDestination = true;
        if (Random.Range(1, math.clamp(chanceToEndHunt - destinationsReachedInCurrentMode, 1, chanceToEndHunt)) == 1)
        {
            EnterRandomMode();
        }
    }

    private void StalkBehaviour()
    {
        if (pathPending)
            return;
        Vector3 playerPos = player.transform.position;

        Vector3 waypoint = ReturnPointAroundPlayer(playerPos, Random.Range(0, 360), stalkDistance);
        waypoint = ReturnPointWithAccurateHeight(waypoint);
        agent.SetDestination(waypoint);
        pathPending = true;
        stalkDistance = math.clamp(stalkDistance / divideStalkDistanceByXPerWaypoint, 5, stalkDistance);
        hasGainedNewDestination = true;
        if (destinationsReachedInCurrentMode > 2)
        {
            if (Random.Range(1, math.clamp(chanceToEndStalking - destinationsReachedInCurrentMode, 1, chanceToEndStalking)) == 1)
            {
                EnterRandomMode();
            }
        }
    }

    private Vector3 ReturnPointWithAccurateHeight(Vector3 pointToCheck)
    {
        Physics.Raycast(new Vector3(pointToCheck.x, 20, pointToCheck.z), Vector3.down, out RaycastHit hit, 30f,
            groundLayerMask);
        Debug.DrawRay(new Vector3(pointToCheck.x, 20, pointToCheck.z), Vector3.down*30, Color.yellow, 20);
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
        Mode.Hiding = false;
        Mode.Hunting = false;
        Mode.Stalking = false;
        hiding = false;
        hunting = false;
        stalking = false;
        destinationsReachedInCurrentMode = 0;
        stalkDistance = defaultStalkDistance;
        int temp = Random.Range(0, 3);
        switch (temp)
        {
            case 0:
                Mode.Hiding = true;
                hiding = true;
                break;
            case 1:
                Mode.Hunting = true;
                hunting = true;
                break;
            case 2:
                Mode.Stalking = true;
                stalking = true;
                break;
            default:
                Mode.Hunting = true;
                hunting = true;
                break;
        }

        changeCooldown = minimumTimeBeforeModeChange;
        if (!agent.hasPath)
        {
            React();
        }
    }
    void Update()
    {
        changeCooldown = changeCooldown - Time.deltaTime;
        pathTimer = pathTimer - Time.deltaTime;
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        activePath = agent.hasPath;
        
        if (distanceFromPlayer < reactionRange && changeCooldown <= 0 && !agent.pathPending)
        {
            pathPending = false;
            React();
        }
        if (pathTimer <= 0 && !agent.hasPath && !agent.pathPending)
        {
            pathPending = false;
            React();
            pathTimer = pathUpdateTimer;
            
        }
        if (agent.remainingDistance <= agent.stoppingDistance && hasGainedNewDestination)
        {
            hasGainedNewDestination = false;
            agent.destination = transform.position;
            agent.ResetPath();
            pathPending = false;
            destinationsReachedInCurrentMode++;
        }

    }
}
