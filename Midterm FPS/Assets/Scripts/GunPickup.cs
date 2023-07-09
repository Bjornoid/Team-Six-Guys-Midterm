using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour, IPickup
{
    [SerializeField] GunStats gun;

    void Start()
    {
        gun.magAmmoCurr = gun.magAmmoMax;
        gun.reserveAmmoCurr = gun.reserveAmmoMax;
    }

    public void pickup()
    {
        gameManager.instance.playerScript.setGunModel(gun.name);
        gameManager.instance.playerScript.gunPickup(gun);
        Destroy(gameObject);
    }

} 