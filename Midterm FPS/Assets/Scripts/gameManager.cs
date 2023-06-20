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
    public VoidBullet voidScript;
    public GameObject playerSpawnPosition;

    [Header("----- UI fields -----")]
    public GameObject activeMenu;
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject playMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject checkpointPopUp;
    public GameObject checkpointPopUpTwo;
    public Image playerHPBar;
    public Image fuelBar;
    public GameObject fuelUI;
    public GameObject playerFlashUI;
    public TextMeshProUGUI enemiesRemainingText;
    public TextMeshProUGUI mainTaskDescription;
    public TextMeshProUGUI sideQuestDescription; 
    public TextMeshProUGUI ammoMaxText;
    public TextMeshProUGUI ammoCurText;

    [Header("----- Game Goal fields -----")]
    int enemiesRemaining;
    int targetsRemaining;
    int locksRemaining;

    bool isPaused;
    float timeScaleOrig;
    bool beatLevel;

    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerControls>();
        playerSpawnPosition = GameObject.FindGameObjectWithTag("Player Spawn Position");
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && activeMenu == null)
        {
            statePaused();
            activeMenu = pauseMenu;
            activeMenu.SetActive(isPaused);
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

    public int getLocksRemaining()
    {
        return locksRemaining;
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
        if (activeMenu != null)
        {
            activeMenu.SetActive(false);
            activeMenu = null;
        }
    }

    public void UpdateGameGoal(int amount)
    {
        enemiesRemaining += amount;

        enemiesRemainingText.text = enemiesRemaining.ToString("F0");

        if (enemiesRemaining <= 0 && beatLevel)
        {
            StartCoroutine(youWin());
        }
    }

    public void updateTargetCount(int amount)
    {
        targetsRemaining += amount;
    }

    public void updateLockCount(int amount)
    {
        locksRemaining += amount;
    }

    IEnumerator youWin()
    {
        yield return new WaitForSeconds(3);
        activeMenu = winMenu;
        activeMenu.SetActive(true);
        statePaused();
    }

    public void YouLose()
    {
        statePaused();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
    }

    public void setMainTask(string taskDescription)
    {
        mainTaskDescription.text = taskDescription;
        
    }
    public void setSideQuest(string questDescription)
    {
        sideQuestDescription.text = questDescription;
    }
}
