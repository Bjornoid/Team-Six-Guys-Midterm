using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject _doorPivot;
    public float _doorRotation;

    private void OnTriggerEnter(Collider other)
    {
        rotateDoor(_doorPivot, _doorPivot.transform.localRotation.y + _doorRotation);
    }

    private void OnTriggerExit(Collider other)
    {
        rotateDoor(_doorPivot, _doorPivot.transform.localRotation.y);
    }

    public static void rotateDoor(GameObject doorPivot, float doorRotation)
    {
        Quaternion rotation = Quaternion.Euler(0, doorRotation, 0);
        doorPivot.transform.rotation = rotation;
    }
    
}
