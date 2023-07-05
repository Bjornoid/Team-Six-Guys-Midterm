using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
        SceneManager.LoadScene("New Tutorial");
        gameManager.instance.stateUnPaused();
    }

    public void nextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
        gameManager.instance.stateUnPaused();

        if (nextLevel > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextLevel);
        }
    }

    public void floor1()
    {
        SceneManager.LoadScene("B & J Level");
        gameManager.instance.stateUnPaused();
    }

    public void floor2()
    {
        SceneManager.LoadScene("Level 2");
        gameManager.instance.stateUnPaused();
    }
    public void floor3()
    {
        SceneManager.LoadScene("L & S Level");
        gameManager.instance.stateUnPaused();
    }
    public void floor4()
    {
        SceneManager.LoadScene("ShaunSandbox");
        gameManager.instance.stateUnPaused();
    }

    public void BackToSettings()
    {
        gameManager.instance.SwitchToPause();
    }

    public void InGameAudio()
    {
        gameManager.instance.SwitchToAudioInGame();
    }

    public void InGameGeneral()
    {
        gameManager.instance.SwitchToGeneralInGame();
    }

    public void Back()
    {
        gameManager.instance.switchToSettings();
    }

    public void BackInGame()
    {
        gameManager.instance.SwitchToSettingsInGame();
    }

    public void Audio()
    {
        gameManager.instance.SwitchToAudio();
    }

    public void General()
    {
        gameManager.instance.SwitchToGeneral();
    }

    public void close()
    {
        gameManager.instance.switchToMain();
        gameManager.instance.eventSystem.SetSelectedGameObject(gameManager.instance.eventSystem.firstSelectedGameObject);
    }

    public void closeUI()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = gameManager.instance.timeScaleOrig;
        gameManager.instance.canPause = true;
    }

    public void resume()
    {
        gameManager.instance.stateUnPaused();
    }

   public void quit()
    {
        Application.Quit();
    }

    public void SettingsInGame()
    {
        gameManager.instance.SwitchToSettingsInGame();
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
        SpiderSpawner.playerNotInRange();
        gameManager.instance.playerScript.SpawnPlayer();
    }
}
