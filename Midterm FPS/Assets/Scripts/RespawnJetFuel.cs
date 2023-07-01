using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnJetFuel : MonoBehaviour
{
    public GameObject[] fuels;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject f in fuels)
            {
                if (!f.activeSelf)
                    f.SetActive(true);
            }
        }
    }

}