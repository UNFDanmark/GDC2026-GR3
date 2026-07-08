using System;
using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    [SerializeField] AudioSource footSteps;
    [SerializeField] Rigidbody playerRigidbody;

    bool isPlaying;
    void FixedUpdate()
    {
        
        
        if (playerRigidbody.linearVelocity.magnitude >= 1)
        {
            if (isPlaying == false)
            {
            footSteps.Play();
            print("Foot stepping");
            isPlaying = true;
            }
        }
        else
        {
            isPlaying = false;
            footSteps.Stop();
        }
    }
}
