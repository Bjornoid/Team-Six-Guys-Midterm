using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviTriggers : MonoBehaviour
{
    public List<GameObject> targets;
    public List<GameObject> triggers;
    public List<Animator> animator;
    public GameObject key;
    void OnTriggerEnter(Collider other)
    {
        if (gameObject.Equals(triggers[0]) && gameManager.instance.getEnemiesRemaining() <= 0)
        {
            animator[0].Play("DoorOpen", 0, 0.0f);
            triggers[0].SetActive(false);
        }
        else if (gameObject.Equals(triggers[1]))
        {
            animator[1].Play("DoorClosed", 0, 0.0f);
            triggers[1].SetActive(false);
        }
        if (gameObject.Equals(triggers[2]))
        {
            triggers[2].SetActive(false);
            Destroy(key);
            animator[2].Play("DoorOpen", 0, 0.1f);
        }
    }
}
