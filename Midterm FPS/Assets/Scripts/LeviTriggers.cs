using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviTriggers : MonoBehaviour
{
    public List<GameObject> UIs;
    public List<GameObject> targets;
    public List<GameObject> triggers;
    public List<Animator> animator;

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.Equals(triggers[1]) && gameManager.instance.getEnemiesRemaining() <= 0)
        {
            animator[0].Play("DoorOpen");
            animator[3].Play("Rig_inspect_ground_loop");
            triggers[1].SetActive(false);
        }
        else if (gameObject.Equals(triggers[2]))
        {
            animator[0].Play("DoorClosed");
            triggers[2].SetActive(false);
            UIs[0].SetActive(false);
            UIs[1].SetActive(true);
        }
    }
}
