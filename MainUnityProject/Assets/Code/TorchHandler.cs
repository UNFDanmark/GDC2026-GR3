using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TorchHandler : MonoBehaviour
{
    public static TorchHandler instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
    }

    [SerializeField] GameObject torchPrefab;
    [SerializeField] MonsterAI monsterAI;
    [SerializeField] Light torchLight;
    [SerializeField] bool hasTorch;

    public void StickThrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started && hasTorch && !ItemCounter.inPicture)
        {
            var temp = Instantiate(torchPrefab, transform.position, transform.rotation);
            temp.GetComponent<Rigidbody>().linearVelocity = new Vector3(transform.forward.x*5, transform.forward.y+1*2, transform.forward.z*5);
            torchLight.enabled = false;
            hasTorch = false;
        }
    }

    public void EnableStick()
    {
        torchLight.enabled = true;
        hasTorch = true;
    }
}
