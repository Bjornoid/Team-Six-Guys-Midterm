using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] GunStats gun;

    void Start()
    {
        gun.magAmmoCurr = gun.magAmmoMax;
        gun.reserveAmmoCurr = gun.reserveAmmoMax;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.setGunModel(gun.name);
            gameManager.instance.playerScript.gunPickup(gun);
            Destroy(gameObject);
        }
    }

} 