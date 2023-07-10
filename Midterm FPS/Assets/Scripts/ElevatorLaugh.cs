using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLaugh : MonoBehaviour
{
    private void Start()
    {
        gameManager.instance.fuelUI.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.hotelSpirit);
        }
    }
}
