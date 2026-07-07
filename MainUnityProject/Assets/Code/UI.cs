using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0); //ændre dette til 1 senere når vi har fået lavet en main menu
    }
    
    
}
