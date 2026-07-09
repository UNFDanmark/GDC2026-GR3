using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject[] pictures;
    
    public void Restart()
    {
        ItemCounter.Score = 0;
        SceneManager.LoadScene(1); //ændre dette til 1 senere når vi har fået lavet en main menu
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void Settings()
    {
        
    }
    
    
}
