using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void NextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Level1()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
    }
    
    public void Quit()
    {
        Application.Quit();
    }
    
}
