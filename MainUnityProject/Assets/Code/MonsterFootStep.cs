using UnityEngine;
using UnityEngine.AI;

public class MonsterFootStep : MonoBehaviour
{
  
    [SerializeField] AudioSource mFootSteps;
    [SerializeField] NavMeshAgent monsterRigidbody;

    bool isPlaying;

    void FixedUpdate()
    {


        if (monsterRigidbody.velocity.magnitude > 0)
        {
            if (isPlaying == false)
            {
                mFootSteps.Play();
                print("Foot stepping");
                isPlaying = true;
            }
        }
        else
        {
            isPlaying = false;
            mFootSteps.Stop();
        }
    }
}
