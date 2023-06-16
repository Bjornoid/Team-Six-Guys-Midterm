using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Header("----- Ammo Controls -----")]
    [SerializeField] public int ammoAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        IAmmo hasAmmo = other.GetComponent<IAmmo>();

        if (hasAmmo != null )
        {
            hasAmmo.PickupAmmo(ammoAmount, gameObject);
        }
    }
}
