using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunStats : ScriptableObject
{
    public int shootDist;
    public float shootRate;
    public int shootDmg;
    public int magAmmoCurr;
    public int magAmmoMax;
    public int reserveAmmoCurr;
    public int reserveAmmoMax;

    public float reloadTime;

    public AudioClip reloadSound;
    public AudioClip shotSound;
    public float soundVol;

    public string gunName;

    public GameObject model;
    public ParticleSystem hitEffect;
}
