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
        if (item.getObjToThrow() == gameManager.instance.stunGrenade)
        {
            gameManager.instance.grenadeAmountText.text = 5.ToString();
        }
        else 
        {
            gameManager.instance.grenadeAmountText.text = 3.ToString();
        }
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
