using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void settings()
    {
        gameManager.instance.switchToSettings();
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
