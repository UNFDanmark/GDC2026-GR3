using System;
using UnityEngine;

public class BarrelGiveStick : MonoBehaviour, Iinteractable
{
    bool hasStick;
    [SerializeField] float replenishTimer = 10f;
    float cooldown;
    [SerializeField] AudioSource pickUpNoise;

    public void Interact()
    {
        if (!hasStick)
            return;
        TorchHandler.instance.EnableStick();
        hasStick = false;
        pickUpNoise.Play();
        cooldown = replenishTimer;
    }

    void Update()
    {
        if (hasStick)
            return;
        cooldown = cooldown - Time.deltaTime;
        if (cooldown <= 0)
        {
            hasStick = true;
        }
    }
}