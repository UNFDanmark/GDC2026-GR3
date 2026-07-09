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
    [SerializeField] GameObject uiTorch;
    [SerializeField] MonsterAI monsterAI;
    [SerializeField] Light torchLight;
    [SerializeField] bool hasTorch;

    public void StickThrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started && hasTorch && !ItemCounter.inPicture)
        {
            uiTorch.SetActive(false);
            var temp = Instantiate(torchPrefab, transform.position, transform.rotation);
            temp.GetComponent<Rigidbody>().linearVelocity = new Vector3(transform.forward.x*10, transform.forward.y+1*4, transform.forward.z*10);
            torchLight.enabled = false;
            hasTorch = false;
        }
    }

    public void EnableStick()
    {
        uiTorch.SetActive(true);
        torchLight.enabled = true;
        hasTorch = true;
    }
}
