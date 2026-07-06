using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] Transform cameraOrigin;
    [SerializeField] float interactRange = 3;
    public void Interact()
    {
        Physics.Raycast(cameraOrigin.position, cameraOrigin.forward, out RaycastHit interactTarget, interactRange);
        Debug.DrawRay(cameraOrigin.position, cameraOrigin.forward, Color.red, 5);
        try
        {

            interactTarget.transform.GetComponent<Iinteractable>().Interact();

        }
        catch
        {
            
        }
    }
}
