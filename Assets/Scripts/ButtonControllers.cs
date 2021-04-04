using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControllers : MonoBehaviour
{
    // Start is called before the first frame update
    public void startGame()
    {
        SceneManager.LoadScene(0);
    }
    public void nextPage()
    {
        SceneManager.LoadScene(1);
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
