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
            interactUI.SetActive(true);
        }
        
        Debug.DrawRay(cameraOrigin.position, cameraOrigin.forward, Color.red, 5);
        try
        {

            interactTarget.transform.GetComponent<Iinteractable>().Interact();
            interactUI.SetActive(false);

        }
        catch
        {
            
        }
  
    }
}
