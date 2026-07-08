using System;
using UnityEngine;

public class MonsterColdZone : MonoBehaviour
{
    [SerializeField] Collider trigger;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Heat>().AddToCurrentHeat(-100);
        }
    }
}