using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

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

    [Header("----- Game Goal fields -----")]
    int enemiesRemaining;
    float timeRemaining;

    bool isPaused;
    float timeScaleOrig;

    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.gameObject.GetComponent<PlayerControls>();
    }

    void Update()
    {
        
    }

    public void displayUI(GameObject ui, GameObject prevUI = null)
    {
        ui.SetActive(true);
        if (prevUI != null) 
        {
            prevUI.SetActive(false);
        }
    }

    public void switchToSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    
    public void switchToMain()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void statePaused()
    {
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

    public void UpdateGameGoal(int amount)
    {
        enemiesRemaining += amount;

        if (enemiesRemaining <= 0)
        {
            // win condition
        }
    }
}
