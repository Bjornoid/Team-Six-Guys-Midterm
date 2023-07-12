using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource sfx;

    public AudioClip backgroundMusic;
    public AudioClip bossSong;
    public AudioClip shaunLevel; 
    public AudioClip credits; 
    public AudioClip cutScene;
    public AudioClip[] steps;
    public AudioClip ammoPickup;
    public AudioClip current;
    public AudioClip burst;
    public AudioClip gunCock;
    public AudioClip hit;
    public AudioClip keys;
    public AudioClip master;
    public AudioClip medKit;
    public AudioClip pistol;
    public AudioClip shotty;
    public AudioClip sniper;
    public AudioClip startUP;
    public AudioClip voidEater;
    public AudioClip gunClick;
    public AudioClip jetPack;
    public AudioClip laser;
    public AudioClip[] playerHurt;
    public AudioClip[] playerjump;
    public AudioClip pistolReload;
    public AudioClip shottyReload;
    public AudioClip akReload;
    public AudioClip flamethrower;
    public AudioClip popUp;
    public AudioClip buttonPress;
    public AudioClip buttonSelect;
    public AudioClip monkeyBomb;
    public AudioClip stunGrenade;
    public AudioClip[] humanCrickets;
    public AudioClip[] skeleton;
    public AudioClip[] swordSwing;
    public AudioClip[] soldierHurts;
    public AudioClip[] spiderDeath;
    public AudioClip[] spiderWalk;
    public AudioClip[] spiderHiss;
    public AudioClip[] zombieGroans;
    public AudioClip hotelSpirit;
    public AudioClip jetpackEmpty;
    public AudioClip jetpackFull;
    public AudioClip targetPing;
    public AudioClip part1;
    public AudioClip part2;
    public AudioClip part3;
    public AudioClip wwBuilt;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Boss Level" && SceneManager.GetActiveScene().name != "L & S Level" && SceneManager.GetActiveScene().name != "Ending_Scene" && SceneManager.GetActiveScene().name != "EndCredits")
            music.clip = backgroundMusic;
        else if (SceneManager.GetActiveScene().name == "Boss Level")
            music.clip = bossSong;
        else if (SceneManager.GetActiveScene().name == "L & S Level")
            music.clip = shaunLevel;
        else if (SceneManager.GetActiveScene().name == "EndCredits")
            music.clip = credits;
        else if (SceneManager.GetActiveScene().name == "Ending_Scene")
            music.clip = cutScene;

        music.Play();
    }

    public void PlaySFX(AudioClip clip) 
    { 
        sfx.PlayOneShot(clip); 
    }

    public void PlaySFX(AudioClip clip, AudioSource source)
    {
        source.PlayOneShot(clip);
    }

    public void PlaySFXArray(AudioClip[] clips) 
    {
        sfx.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
    }

    public void PlaySFXArray(AudioClip[] clips, AudioSource source)
    {
        source.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
    }
}
