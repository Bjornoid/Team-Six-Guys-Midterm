using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource sfx;

    public AudioClip backgroundMusic;
    public AudioClip[] steps;
    public AudioClip ammoPickup;
    public AudioClip burstFire;
    public AudioClip current;
    public AudioClip gunCock;
    public AudioClip hit;
    public AudioClip keys;
    public AudioClip master;
    public AudioClip medKit;
    public AudioClip pistol;
    public AudioClip shotty;
    public AudioClip smg;
    public AudioClip sniper;
    public AudioClip startUP;
    public AudioClip voidEater;
    public AudioClip akReload;
    public AudioClip doorOpen;
    public AudioClip explosion;
    public AudioClip gunClick;
    public AudioClip gunReload;
    public AudioClip gunShot;
    public AudioClip jetPack;
    public AudioClip laser;
    public AudioClip pistolReload;
    public AudioClip[] playerHurt;
    public AudioClip[] playerjump;
    public AudioClip ricochet;
    public AudioClip robotHurt;
    public AudioClip robotHit;
    public AudioClip shottyReload;
    public AudioClip spaceStation;
    public AudioClip spawned;
    public AudioClip UIButtonPopUp;
    public AudioClip UIHPGive;
    public AudioClip voiceHeadShot;

    private void Start()
    {
        music.clip = backgroundMusic;
        music.Play();
    }

    public void PlaySFX(AudioClip clip) 
    { 
        sfx.PlayOneShot(clip); 
    }

    public void PlaySFXArray(AudioClip[] clips) 
    {
        sfx.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
    }
}
