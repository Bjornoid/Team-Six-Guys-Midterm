using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAndLockTrigger : MonoBehaviour
{
    [Header("----- Game Objects -----")]
    public GameObject theKey;
    public GameObject theLock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            theKey.SetActive(false);
            theLock.SetActive(false);
        }
    }
}
