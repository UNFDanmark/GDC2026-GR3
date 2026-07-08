using System;
using UnityEngine;
using UnityEngine.UI;

public class FreezeUI : MonoBehaviour
{
   [SerializeField] Heat heat;

   [Header("Freeze UI")] 
   [SerializeField] GameObject freeze1;
   [SerializeField] GameObject freeze2;
   [SerializeField] GameObject freeze3;
   [SerializeField] GameObject freeze4;
   [SerializeField] GameObject freeze5;
   [SerializeField] GameObject freeze6;
   [SerializeField] GameObject freeze7;


   void FixedUpdate()
   {
      if (heat.CurrentHeat < 14.2857142857f)
      {
         freeze7.SetActive(true);
         freeze6.SetActive(false);
      }
      else if (heat.CurrentHeat < 14.285714285f * 2)
      {
         freeze6.SetActive(true);
         freeze5.SetActive(false);
      }
      else if (heat.CurrentHeat <  14.2857142857 * 3)
      {
         freeze5.SetActive(true);
         freeze4.SetActive(false);
      }
      else if (heat.CurrentHeat <  14.2857142857 * 4)
      {
         freeze4.SetActive(true);
         freeze3.SetActive(false);
      }
      else if (heat.CurrentHeat <  14.2857142857 * 5)
      {
         freeze3.SetActive(true);
         freeze2.SetActive(false);
      }
      else if (heat.CurrentHeat <  14.2857142857 * 6)
      {
         freeze2.SetActive(true);
         freeze1.SetActive(false);
      }
      else if (heat.CurrentHeat < 14.2857142857 * 7)
      {
         freeze1.SetActive(true);
      }
   }
}
