using System;
using UnityEngine;


public class ItemCounter : MonoBehaviour
{
   [SerializeField] GameObject[] pictures;
   [SerializeField] GameObject canvas;
   public static bool inPicture;
   void Start()
   {
      inPicture = false;
      score = 0;
      scoreUpdated += showPic;
   }

   static Action<int> scoreUpdated;
   static int score = 0;
   public static int Score
   {
      get => score;
      set
      {
         score = Math.Clamp(value, 0, 5);
         scoreUpdated?.Invoke(value);
      }
   }

   public void showPic(int picNum)
   {
      if (picNum == 0)
         return;
      
      foreach (var pic in pictures)
      {
         pic.SetActive(false);
      }

      inPicture = true;
      canvas.SetActive(true);
      Time.timeScale = 0;
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
      pictures[Math.Clamp(picNum -1, 0, pictures.Length - 1)].SetActive(true);
   }

   public void hidePic()
   {
      foreach (var pic in pictures)
      {
         pic.SetActive(false);
      }

      inPicture = false;
      canvas.SetActive(false);
      Time.timeScale = 1;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      
      print("Picture fading");
      //fix.code;
   }
}
