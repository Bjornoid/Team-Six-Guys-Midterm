using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParts : MonoBehaviour
{
    [SerializeField] GameObject gunPart;
    [SerializeField] GameObject gunBuilt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
}
