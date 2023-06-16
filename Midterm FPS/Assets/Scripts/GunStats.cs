using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class GunStats : ScriptableObject
{
    [Header("----- Gun Settings -----")]
    [SerializeField] public int shootDist; // shooting distance for gun
    [SerializeField] public float shootRate; // shooting speed for the gun
    [SerializeField] public int shootDamage; // amount of damage for the gun

    public GameObject model; // model of gun
    public ParticleSystem hitEffect; // hit effect of the gun
    public int ammoCurr; // current amount of ammo gun has
    public int ammoMax; // max amount of ammo a gun can carry
}
