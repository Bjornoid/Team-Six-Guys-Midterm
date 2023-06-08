using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    public GameObject openTrigger;
    public GameObject clsoeTrigger;
    public Animator animator;


    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.Equals(openTrigger))
        {
            openTrigger.SetActive(false);
            animator.Play("DoorOpen", 0, 0f);

        }
        else if (gameObject.Equals(clsoeTrigger))
        {
            clsoeTrigger.SetActive(false);
            animator.Play("DoorClosed", 0, 0f);
        }
    }
}
