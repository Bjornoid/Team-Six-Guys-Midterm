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
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void play()
    {
        gameManager.instance.switchToLevelSelect();
        gameManager.instance.eventSystem.SetSelectedGameObject(levelSelectFirstButton);
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        gameManager.instance.stateUnPaused();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void newGame()
    {
        if (PlayerPrefs.GetInt("levelAt") > 2)
        {
            gameManager.instance.confirmDialog.SetActive(true);
            
            foreach (Button b in gameManager.instance.mainMenuBtns)
            {
                b.interactable = false;
            }
        }
        else
            play();
    }

    public void resetGame()
    {
        PlayerPrefs.SetInt("levelAt", 2);
        PlayerPrefs.SetInt("deaths", 0);
        gameManager.instance.handleLevelUnlocks();

        foreach (Button b in gameManager.instance.mainMenuBtns)
        {
            b.interactable = true;
        }
        gameManager.instance.confirmDialog.SetActive(false);
        gameManager.instance.loadGame.SetActive(false);
    }

    public void cancel()
    {
        foreach (Button b in gameManager.instance.mainMenuBtns)
        {
            b.interactable = true;
        }
        gameManager.instance.confirmDialog.SetActive(false);
    }

    public void tutorial()
    {
        SceneManager.LoadScene("New Tutorial");
        gameManager.instance.stateUnPaused();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void nextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        gameManager.levelToLoad = nextLevel;
        SceneManager.LoadScene("Elevator");
        gameManager.instance.stateUnPaused();

        if (nextLevel > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextLevel);
        }
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void floor1()
    {
        SceneManager.LoadScene("B & J Level");
        gameManager.instance.stateUnPaused();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void floor2()
    {
        SceneManager.LoadScene("Level 2");
        gameManager.instance.stateUnPaused();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }
    public void floor3()
    {
        SceneManager.LoadScene("L & S Level");
        gameManager.instance.stateUnPaused();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }
    public void floor4()
    {
        SceneManager.LoadScene("Boss Level"); 
        gameManager.instance.stateUnPaused();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void BackToSettings()
    {
        gameManager.instance.SwitchToPause();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void InGameAudio()
    {
        gameManager.instance.SwitchToAudioInGame();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void InGameGeneral()
    {
        gameManager.instance.SwitchToGeneralInGame();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void Back()
    {
        gameManager.instance.switchToSettings();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void BackInGame()
    {
        gameManager.instance.SwitchToSettingsInGame();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void Audio()
    {
        gameManager.instance.SwitchToAudio();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void General()
    {
        gameManager.instance.SwitchToGeneral();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void close()
    {
        gameManager.instance.switchToMain();
        if (PlayerPrefs.GetInt("levelAt") <= 2)
            gameManager.instance.eventSystem.SetSelectedGameObject(gameManager.instance.eventSystem.firstSelectedGameObject);
        else
            gameManager.instance.eventSystem.SetSelectedGameObject(gameManager.instance.loadGame);

        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    } 

    public void closeUI()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = gameManager.instance.timeScaleOrig;
        gameManager.instance.canPause = true;
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void resume()
    {
        gameManager.instance.stateUnPaused();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

   public void quit()
    {
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
        Application.Quit();
    }

    public void SettingsInGame()
    {
        gameManager.instance.SwitchToSettingsInGame();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void RespawnPlayer()
    {
        gameManager.instance.stateUnPaused();
        gameManager.instance.playerScript.SpawnPlayer();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }

    public void Restart()
    {
        gameManager.instance.stateUnPaused();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SpiderSpawner.playerNotInRange();
        gameManager.instance.playerScript.SpawnPlayer();
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.buttonPress);
    }
}
