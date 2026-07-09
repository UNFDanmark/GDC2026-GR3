using System;
using UnityEngine;

public class TestObject : MonoBehaviour, Iinteractable
{

    [SerializeField] GameObject interactUI;
    [SerializeField] AudioSource pickUpNoise;
    
    public void Interact()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        interactUI.SetActive(false);
        pickUpNoise.Play();
        Destroy(gameObject);
        ItemCounter.Score += 1;

        print(ItemCounter.Score);
    }
}
