using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void settings()
    {
        gameManager.instance.switchToSettings();
    }

    public void play()
    {
        gameManager.instance.switchToLevelSelect();
    }

    public void tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void floor1()
    {
        SceneManager.LoadScene("JohnSandbox");
        gameManager.instance.stateUnPaused();
    }

    public void floor2()
    {
        SceneManager.LoadScene("LeviSandbox");
        gameManager.instance.stateUnPaused();
    }
    public void floor3()
    {
        SceneManager.LoadScene("StevenSandbox");
        gameManager.instance.stateUnPaused();
    }
    public void floor4()
    {
        SceneManager.LoadScene("ShaunSandbox");
        gameManager.instance.stateUnPaused();
    }

    public void close()
    {
        gameManager.instance.switchToMain();
    }


   public void quit()
    {
        Application.Quit();
    }
}
