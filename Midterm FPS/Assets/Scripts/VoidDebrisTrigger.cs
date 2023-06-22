using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDebrisTrigger : MonoBehaviour
{
    [SerializeField] GameObject debris;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VoidBullet"))
        {
            debris.SetActive(false);
        }
    }
}
