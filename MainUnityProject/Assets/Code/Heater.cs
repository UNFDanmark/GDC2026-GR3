using System;
using UnityEngine;

public class Heater : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] float heatGainPerSecond = 2f;
    void OnTriggerEnter(Collider other)
    {
        try
        {
            //hGPS gotta be negative to heat player
            other.GetComponent<Heat>().HeatDrainModifier = -heatGainPerSecond;
        }
        catch
        {
            Debug.Log("Collider does not have Heat component");
        }
    }

    void OnTriggerExit(Collider other)
    {
        try
        {
            other.GetComponent<Heat>().HeatDrainModifier = 0;
        }
        catch
        {
            Debug.Log("Collider does not have Heat component");
        }
    }
}
