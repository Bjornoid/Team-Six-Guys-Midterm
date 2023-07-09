using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour, IPickup
{
    public void pickup()
    {
        gameManager.instance.playerScript.pickupAmmo();
        Destroy(gameObject);
        Throwable item = gameManager.instance.player.GetComponent<Throwable>();
        item.thrown = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        IAmmo hasAmmo = other.GetComponent<IAmmo>();
        if (hasAmmo != null) 
        {
            hasAmmo.pickupAmmo();
            Destroy(gameObject);
        }
    }
}
