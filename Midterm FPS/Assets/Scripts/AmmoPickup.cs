using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        IAmmo hasAmmo = other.GetComponent<IAmmo>();
        Throwable item = gameManager.instance.player.GetComponent<Throwable>();
        item.thrown = 0;
        if (hasAmmo != null) 
        {
            hasAmmo.pickupAmmo();
            Destroy(gameObject);
        }
    }
}
