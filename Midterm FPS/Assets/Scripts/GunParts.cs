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
        switch(gameManager.instance.partList.Count)
        {
            case 1:
                gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.part1);
                break;
            case 2:
                gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.part2);
                break;
            case 3:
                gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.part3);
                break;
        }
        gunPart.SetActive(false);

        int fullGun = 3;
        if (gameManager.instance.partList.Count == fullGun)
        {
            gunBuilt.SetActive(true);
        }
    }

    public void delete()
    {
        Destroy(gameObject);
    }
}
