using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger : MonoBehaviour
{
    public List<GameObject> targets;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject t in targets) 
            {
                t.SetActive(true);
            }
            gameManager.instance.updateTargetCount(targets.Count);
        }
    }
}
