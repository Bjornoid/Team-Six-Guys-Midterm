using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackTrigger : MonoBehaviour
{
    public GameObject jetpack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.hasJetpack = true;
            jetpack.SetActive(false);
            gameManager.instance.fuelUI.SetActive(true);
        }
    }
}
