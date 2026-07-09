using System;
using UnityEngine;

public class TestObject : MonoBehaviour, Iinteractable
{

    [SerializeField] GameObject interactUI;
    
    public void Interact()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        interactUI.SetActive(false);
        
        Destroy(gameObject);
        ItemCounter.Score += 1;

        print(ItemCounter.Score);
    }
}
