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
    [SerializeField] float defaultStalkDistance = 15f;
    [SerializeField] float divideStalkDistanceByXPerWaypoint = 3f;
    float stalkDistance;
    
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
        //Current version has a chance that the hiding spot makes a path that leads right past the player, could affect scariness of monster
        Vector3 hidingPoint =
            new Vector3(player.transform.position.x + Random.Range(hidingDistanceMinimum, hidingDistanceMaximum), 0, player.transform.position.z + Random.Range(hidingDistanceMinimum, hidingDistanceMaximum));
        agent.SetDestination(hidingPoint);
        if (Random.Range(1, chanceToEndHiding) == 1)
        {
            EnterRandomMode();
        }
    }

    private void HuntBehaviour()
    {
        agent.SetDestination(player.transform.position);
        if (Random.Range(1, chanceToEndHunt) == 1)
        {
            EnterRandomMode();
        }
    }

    private void StalkBehaviour()
    {
        Vector3 startPos = transform.position;
        Vector3 playerPos = player.transform.position;

        Vector3 waypoint1 = ReturnPointAroundPlayer(playerPos, 0, stalkDistance);
        Debug.Log(waypoint1);
        agent.SetDestination(waypoint1);
    }

    private Vector3 ReturnPointAroundPlayer(Vector3 pointToRotateAround, float angle, float radius)
    {
        Debug.Log($"Sin of angle {angle} is equal to " + math.sin(angle));
        Debug.Log($"Cos of angle {angle} is equal to " + math.cos(angle));
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
        if (distanceFromPlayer < reactionRange && changeCooldown <= 0)
        {
            React();
        }
        if (pathTimer <= 0 && !agent.hasPath)
        {
            React();
            pathTimer = pathUpdateTimer;
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.destination = transform.position;
            agent.ResetPath();
        }

    }

    void OnDrawGizmosSelected()
    {
        
    }
}
