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

        if (hasAmmo != null) 
        {
            hasAmmo.pickupAmmo();
            Destroy(gameObject);
        }
    }
}
