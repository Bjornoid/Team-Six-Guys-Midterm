using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cobweb : MonoBehaviour
{
    [SerializeField] float percent;
    private void OnTriggerEnter(Collider other)
    {
        ISlow slowable = other.GetComponent<ISlow>();

        if (slowable != null)
        {
            slowable.slow(percent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ISlow slowable = other.GetComponent<ISlow>();

        if (slowable != null)
        {
            slowable.slow(1/percent);
        }
    }
}
