using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitTrigger : MonoBehaviour
{
    [Header("----- Pit Info -----")]
    public Vector3 _destination; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();

            cc.enabled = false; // disables character controller

            gameManager.instance.player.transform.position = _destination; // teleorts player

            cc.enabled = true; // enables character controller
        }
    }
}
