using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyAndLockTrigger : MonoBehaviour, IPickup
{
    [Header("----- Game Objects -----")]
    public GameObject theKey;
    public GameObject theLock;
    [SerializeField] UnityEvent action;
    void Start()
    {
        gameManager.instance.updateLockCount(1);
    }

    public void pickup()
    {
        gameManager.instance.playerScript.pickupKey();
        theKey.SetActive(false);
        theLock.SetActive(false);
        gameManager.instance.updateLockCount(-1);
        if (gameManager.instance.getLocksRemaining() == 0)
            action.Invoke();
    }

}
