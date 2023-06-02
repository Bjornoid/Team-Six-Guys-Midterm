using System.Collections;
using System.Collections.Generic;
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

    bool isInSettings;
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
}
