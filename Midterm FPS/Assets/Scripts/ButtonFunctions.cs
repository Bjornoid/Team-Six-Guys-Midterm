using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public GameObject levelSelectFirstButton;
    public GameObject settingsFirstButton;
  
    public void settings()
    {
        gameManager.instance.switchToSettings();
        gameManager.instance.eventSystem.SetSelectedGameObject(settingsFirstButton);
    }

    public void play()
    {
        gameManager.instance.switchToLevelSelect();
        gameManager.instance.eventSystem.SetSelectedGameObject(levelSelectFirstButton);
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        gameManager.instance.stateUnPaused();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void tutorial()
    {
        SceneManager.LoadScene("Tutorial");
        gameManager.instance.stateUnPaused();
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameManager.instance.stateUnPaused();
    }

    public void floor1()
    {
        SceneManager.LoadScene("BjornsLevel");
        gameManager.instance.stateUnPaused();
    }

    public void floor2()
    {
        SceneManager.LoadScene("JohnSandbox");
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

    public void floor5()
    {
        SceneManager.LoadScene("LeviSandbox");
        gameManager.instance.stateUnPaused();
    }

    public void floor6()
    {
        SceneManager.LoadScene("jamesSandbox");
        gameManager.instance.stateUnPaused();
    }

    public void close()
    {
        gameManager.instance.switchToMain();
        gameManager.instance.eventSystem.SetSelectedGameObject(gameManager.instance.eventSystem.firstSelectedGameObject);
    }

    public void resume()
    {
        gameManager.instance.stateUnPaused();
    }

   public void quit()
    {
        Application.Quit();
    }

    public void RespawnPlayer()
    {
        gameManager.instance.stateUnPaused();
        gameManager.instance.playerScript.SpawnPlayer();
    }

    public void Restart()
    {
        gameManager.instance.stateUnPaused();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
