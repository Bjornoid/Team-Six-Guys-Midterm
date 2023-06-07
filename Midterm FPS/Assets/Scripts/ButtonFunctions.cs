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

    }

    public void floor2()
    {

    }
    public void floor3()
    {

    }
    public void floor4()
    {

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
