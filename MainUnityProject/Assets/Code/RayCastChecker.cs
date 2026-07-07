using System;
using Unity.VisualScripting;
using UnityEngine;

public class RayCastChecker : MonoBehaviour
{
   [SerializeField] Transform cameraOrigin;
   [SerializeField] float interactRange = 3;
   [SerializeField] GameObject interactUI;

   
   void OnTriggerStay(Collider other)
   {
      
      bool rayHit = Physics.Raycast(cameraOrigin.position, cameraOrigin.forward, out RaycastHit interactTarget, interactRange);
      print("raycast " + rayHit);
      print("trigger " + other.GameObject().CompareTag("InteractableObject"));
      
      if (rayHit && other.GameObject().CompareTag("InteractableObject"))
      {
         interactUI.SetActive(true);
      }
   }

   void OnTriggerExit(Collider other)
   {
      interactUI.SetActive(false);
   }
}
