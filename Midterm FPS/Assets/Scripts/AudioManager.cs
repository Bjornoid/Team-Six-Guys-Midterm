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
