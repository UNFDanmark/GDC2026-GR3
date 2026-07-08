using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] Transform cameraOrigin;
    [SerializeField] float interactRange = 3;
    [SerializeField] GameObject interactUI;
    [SerializeField] Collider rayCastChecker;
    
    public void Interact()
    {
       bool rayHit = Physics.Raycast(cameraOrigin.position, cameraOrigin.forward, out RaycastHit interactTarget, interactRange);

        if (rayHit)
        {
            if (interactTarget.transform.TryGetComponent<Iinteractable>(out var interactable))
            {
                interactable.Interact();
                interactUI.SetActive(false);
            }
        }
        
        Debug.DrawRay(cameraOrigin.position, cameraOrigin.forward, Color.red, 5);
    }
}
