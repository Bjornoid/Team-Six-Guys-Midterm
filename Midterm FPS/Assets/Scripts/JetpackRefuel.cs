using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackRefuel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.pickupFuel();
            gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.laser);
            gameObject.SetActive(false);
        }
    }
}
