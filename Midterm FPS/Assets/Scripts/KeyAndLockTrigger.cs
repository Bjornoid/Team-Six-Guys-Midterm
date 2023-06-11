using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAndLockTrigger : MonoBehaviour
{
    [Header("----- Game Objects -----")]
    public GameObject theKey;
    public GameObject theLock;

    void Start()
    {
        gameManager.instance.updateLockCount(1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            theKey.SetActive(false);
            theLock.SetActive(false);
            gameManager.instance.updateLockCount(-1);
        }
    }
}
