using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLaugh : MonoBehaviour
{
    [SerializeField] GunStats ak;
    private void Start()
    {
        gameManager.instance.fuelUI.SetActive(true);
        gameManager.instance.playerScript.gunPickup(ak);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.hotelSpirit);
            gameManager.instance.playerScript.dropGun();
            gameManager.instance.fuelUI.SetActive(false);
        }
    }
}
