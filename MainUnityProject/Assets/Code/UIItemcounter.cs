using System;
using TMPro;
using UnityEngine;

public class UIItemcounter : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI score;
  [SerializeField] GameObject winScreen; 

  void FixedUpdate()
  {
    score.text = $"Pictures collected {ItemCounter.score}/5";

    if (ItemCounter.score >= 5) // checks if the player has won :)
    {
      Time.timeScale = 0;
      
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
      winScreen.SetActive(true);

      ItemCounter.score = 0;
    }
    
  }
  
  
}
