using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorTrigger : MonoBehaviour
{
    public GameObject chains; // chains that will get destroyed 
    public GameObject lockedDoor; // locked door that will get destroyed

    public float rotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.getLocksRemaining() <= 0)
        {
            if(lockedDoor == null)
            {
                chains.SetActive(false);
            }
            else
            {
                DoorController.rotateDoor(lockedDoor, rotation);
            }
        }
    }
}
