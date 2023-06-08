using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("----- Player fields -----")]
    public GameObject player;
    public PlayerControls playerScript;

    [Header("----- UI fields -----")]
    public GameObject activeMenu;
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject playMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public Image playerHPBar;
    public GameObject playerFlashUI;
    public TextMeshProUGUI enemiesRemainingText;

    [Header("----- Game Goal fields -----")]
    int enemiesRemaining;
    int targetsRemaining;
    float timeRemaining;
    

    bool isPaused;
    float timeScaleOrig;

    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerControls>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && activeMenu == null)
        {
            statePaused();
            activeMenu = pauseMenu;
            activeMenu.SetActive(true);
        }
    }

    public int getTargetCount()
    {
        return targetsRemaining;
    }
    public int getEnemiesRemaining()
    {
        return enemiesRemaining;
    }
    public void switchToSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    
    public void switchToMain()
    {
        playMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void switchToLevelSelect()
    {
        mainMenu.SetActive(false);
        activeMenu = playMenu;
        activeMenu.SetActive(true);
    }
    public void statePaused()
    {
        isPaused = !isPaused;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void stateUnPaused()
    {
        Time.timeScale = timeScaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = !isPaused;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void updateTargetCount(int amount)
    {
        targetsRemaining += amount;
    }
    public void UpdateGameGoal(int amount)
    {
        enemiesRemaining += amount;

        enemiesRemainingText.text = enemiesRemaining.ToString("F0");

        if (enemiesRemaining <= 0)
        {
            // win condition
        }
    }
}
