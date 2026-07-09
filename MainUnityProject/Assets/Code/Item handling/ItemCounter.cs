using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ItemCounter : MonoBehaviour
{
   [SerializeField] GameObject[] pictures;
   [SerializeField] GameObject canvas;

   void Start()
   {
      scoreUpdated += showPic;
   }

   static Action<int> scoreUpdated;
   static int score = 0;
   public static int Score
   {
      get => score;
      set
      {
         score = value;
         scoreUpdated?.Invoke(Score);
      }
   }

   public void showPic(int picNum)
   {
      foreach (var pic in pictures)
      {
         pic.SetActive(false);
      }

      canvas.SetActive(true);
      Time.timeScale = 0;
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
      pictures[picNum].SetActive(true);
   }

   public void hidePic()
   {
      foreach (var pic in pictures)
      {
         pic.SetActive(false);
      }

      canvas.SetActive(false);
      Time.timeScale = 1;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }
}
