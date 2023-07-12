using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerSwitch : MonoBehaviour
{
    [SerializeField] private EndOfLevel endOfLevelScript;
    [SerializeField] UnityEvent evt;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (endOfLevelScript != null)
            {
                evt.Invoke();
            }
        }
    }
}
