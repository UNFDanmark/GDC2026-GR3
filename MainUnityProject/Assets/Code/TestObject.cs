using UnityEngine;

public class TestObject : MonoBehaviour, Iinteractable
{
    [SerializeField] GameObject interactUI;
    
    public void Interact()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        
        
        interactUI.SetActive(false);
        print("UI : " + interactUI.activeSelf);
        Debug.Log("pickup yay");
        Destroy(gameObject);
        
    }
}
