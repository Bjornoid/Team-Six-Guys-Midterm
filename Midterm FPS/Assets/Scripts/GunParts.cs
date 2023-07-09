using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParts : MonoBehaviour, IPickup
{
    [SerializeField] GameObject gunPart;
    [SerializeField] GameObject gunBuilt;

    public void pickup()
    {
        gameManager.instance.partList.Add(gunPart);
        gunPart.SetActive(false);

        int fullGun = 3;
        if (gameManager.instance.partList.Count == fullGun)
        {
            gunBuilt.SetActive(true);
        }
    }
}
