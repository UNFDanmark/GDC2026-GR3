using System;
using Unity.Mathematics;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterAI : MonoBehaviour
{
    [Header("Variables & References")] 
    [SerializeField] GameObject player;

    [SerializeField] float pathUpdateTimer = 5f;
    float pathTimer;
    [SerializeField] float minimumTimeBeforeModeChange = 1f;
    float changeCooldown;
    [SerializeField] float reactionRange = 10f;
    [SerializeField] float hidingDistanceMinimum = 20f;
    [SerializeField] float hidingDistanceMaximum = 40f;
    [SerializeField] float distanceFromPlayer;
    [Tooltip("The higher this number is the lower the chance is")][SerializeField] int chanceToEndHunt = 3;
    [Tooltip("The higher this number is the lower the chance is")][SerializeField] int chanceToEndHiding = 2;
    NavMeshAgent agent;
    [SerializeField] bool activePath;
    
    //States
    [Serializable]public struct Mode
    {
        public static bool Hunting;
        public static bool Hiding;
    }

    [SerializeField] bool hunting;
    [SerializeField] bool hiding;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        EnterRandomMode();
    }

    void React()
    {
        Debug.Log($"Agent has path? " + agent.hasPath);
        Debug.Log($"Hunting mode is: {Mode.Hunting}. Hiding mode is: {Mode.Hiding}");
        if (Mode.Hunting && !agent.hasPath)
        {
            Debug.Log("Running huntbehaviour");
            HuntBehaviour();
        }
        else if (Mode.Hiding && !agent.hasPath)
        {
            Debug.Log("Running hidebehaviour");
            HideBehaviour();
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

    private void EnterRandomMode()
    {
        if ((Mode.Hiding || Mode.Hunting) && changeCooldown > 0)
        {
            Debug.Log("Already in a mode, skipping");
            return;
        }

        Mode.Hiding = false;
        Mode.Hunting = false;
        hiding = false;
        hunting = false;
        int temp = Random.Range(0, 2);
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
}
