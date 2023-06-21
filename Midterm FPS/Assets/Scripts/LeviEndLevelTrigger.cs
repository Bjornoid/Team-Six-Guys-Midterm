using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviEndLevelTrigger : MonoBehaviour
{
    public GameObject door;
    public float rot;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.getEnemiesRemaining() <= 0)
        {
            DoorController.rotateDoor(door, rot);
        }
    }
}
