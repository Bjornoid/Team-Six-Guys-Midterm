using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockArea : MonoBehaviour
{
    [SerializeField] GameObject areaToBlock;

    private void Start()
    {
        areaToBlock.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            areaToBlock.SetActive(true);
        }
    }
    
}
