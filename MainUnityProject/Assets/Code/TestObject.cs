using UnityEngine;

public class TestObject : MonoBehaviour, Iinteractable
{
    public void Interact()
    {
        Debug.Log("pickup yay");
        Destroy(gameObject);
    }
}
