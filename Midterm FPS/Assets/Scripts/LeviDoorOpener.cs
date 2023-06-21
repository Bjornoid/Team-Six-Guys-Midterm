using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviDoorOpener : MonoBehaviour
{
    public GameObject door;
    public float rot;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorController.rotateDoor(door, rot);
        }
    }
}
