using UnityEngine;
using UnityEngine.InputSystem;

public class TorchHandler : MonoBehaviour
{
    [SerializeField] GameObject torchPrefab;
    [SerializeField] MonsterAI monsterAI;

    public void StickThrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            
        }
    }
}
