using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackTrigger : MonoBehaviour, IPickup
{
    public void pickup()
    {
        gameManager.instance.playerScript.hasJetpack = true;
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.jetpackFull);
        gameObject.SetActive(false);
        gameManager.instance.fuelUI.SetActive(true);
    }
}
