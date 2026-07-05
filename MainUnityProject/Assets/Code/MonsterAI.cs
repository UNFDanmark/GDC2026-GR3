using System;
using Unity.Mathematics;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterAI : MonoBehaviour
{
    [Header("Variables & References")] 
    [SerializeField] GameObject player;

    [SerializeField] float reactionRange = 10f;
    [SerializeField] float hidingDistanceMinimum = 20f;
    [SerializeField] float hidingDistanceMaximum = 40f;
    [SerializeField] float distanceFromPlayer;
    [Tooltip("The higher this number is the lower the chance is")][SerializeField] int chanceToEndHunt = 3;
    [Tooltip("The higher this number is the lower the chance is")][SerializeField] int chanceToEndHiding = 2;

    NavMeshAgent agent;
    
    //States
    public struct Mode
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
        if (Mode.Hunting)
        {
            HuntBehaviour();
        }
        else if (Mode.Hiding)
        {
            HideBehaviour();
        }
        else
        {
            EnterRandomMode();
        }
    }
    private void HideBehaviour()
    {
        Vector3 hidingPoint =
            new Vector3(player.transform.position.x + Random.Range(hidingDistanceMinimum, hidingDistanceMaximum), 0, player.transform.position.z + Random.Range(hidingDistanceMinimum, hidingDistanceMaximum));
        agent.destination = hidingPoint;
        if (Random.Range(1, chanceToEndHiding) == 1)
        {
            Mode.Hiding = false;
            hiding = false;
        }
    }

    private void HuntBehaviour()
    {
        agent.destination = player.transform.position;

        if (Random.Range(1, chanceToEndHunt) == 1)
        {
            Mode.Hunting = false;
            hunting = false;

        }
    }

    private void EnterRandomMode()
    {
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
    }
    void Update()
    {

        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.ResetPath();
        }
        if (distanceFromPlayer < reactionRange && !agent.hasPath|| agent.hasPath == false)
        {
            React();
        }

    }
}
