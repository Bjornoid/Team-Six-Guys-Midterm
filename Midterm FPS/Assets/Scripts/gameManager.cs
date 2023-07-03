using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("----- Player fields -----")]
    public GameObject player;
    public PlayerControls playerScript;
    public VoidBullet voidScript;
    public GameObject playerSpawnPosition;
    public List<GameObject> partList;
    public EventSystem eventSystem;

    [Header("----- UI fields -----")]
    public GameObject activeMenu;
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject playMenu;
    public GameObject volume;
    public GameObject general;
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject loseFirstButton;
    public GameObject winFirstButton;
    public GameObject audioFirstButton;
    public GameObject generalFirstButton;
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

    [Header("----- Audio -----")]
    public AudioManager audioManager;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;

    bool isPaused;
    public float timeScaleOrig;
    public bool canPause;
    bool beatLevel;
    [Header("----- Lighting/Color -----")]
    public Color ambientColorOrig;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();  
        instance = this;
        timeScaleOrig = Time.timeScale;
        ambientColorOrig = RenderSettings.ambientLight;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerControls>();
        playerSpawnPosition = GameObject.FindGameObjectWithTag("Player Spawn Position");
        canPause = true;
    }

     void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }

        SetMusicVolume();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && activeMenu == null && pauseMenu != null && canPause && SceneManager.GetActiveScene().name != "Main Menu")
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
        general.SetActive(false);
        volume.SetActive(false);
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

    public void SwitchToAudio()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        general.SetActive(false);
        volume.SetActive(true);
        eventSystem.SetSelectedGameObject(audioFirstButton);
    }

    public void SwitchToGeneral()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        volume.SetActive(false);
        general.SetActive(true);
        eventSystem.SetSelectedGameObject(generalFirstButton);
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
        eventSystem.SetSelectedGameObject(winFirstButton);
    }

    public void YouLose()
    {
        statePaused();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(loseFirstButton);
    }

    public void setMainTask(string taskDescription)
    {
        mainTaskDescription.text = taskDescription;
        
    }
    public void setSideQuest(string questDescription)
    {
        sideQuestDescription.text = questDescription;
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMusicVolume();
        SetSFXVolume();
    }
}
