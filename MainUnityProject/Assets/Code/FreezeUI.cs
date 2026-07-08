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
      if (heat.CurrentHeat < 12.8571428571f)
      {
         freeze7.SetActive(true);
         freeze6.SetActive(false);
      }
      else if (heat.CurrentHeat < 12.8571428571f * 2)
      {
         freeze7.SetActive(false);
         freeze6.SetActive(true);
         freeze5.SetActive(false);
      }
      else if (heat.CurrentHeat <  12.8571428571f * 3)
      {
         freeze6.SetActive(false);
         freeze5.SetActive(true);
         freeze4.SetActive(false);
      }
      else if (heat.CurrentHeat <  12.8571428571f * 4)
      {
         freeze5.SetActive(false);
         freeze4.SetActive(true);
         freeze3.SetActive(false);
      }
      else if (heat.CurrentHeat <  12.8571428571f * 5)
      {
         freeze4.SetActive(false);
         freeze3.SetActive(true);
         freeze2.SetActive(false);
      }
      else if (heat.CurrentHeat <  12.8571428571f * 6)
      {
         freeze3.SetActive(false);
         freeze2.SetActive(true);
         freeze1.SetActive(false);
      }
      else if (heat.CurrentHeat < 12.8571428571f * 7)
      {
         freeze2.SetActive(false);
         freeze1.SetActive(true);
      }
      else if (heat.CurrentHeat > 12.85 * 7)
      {
         freeze1.SetActive(false);
      }
   }
}
