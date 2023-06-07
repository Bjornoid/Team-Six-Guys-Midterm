using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    public GameObject openTrigger;
    public GameObject clsoeTrigger;
    public Animator animation;


    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.Equals(openTrigger))
        {
            openTrigger.SetActive(false);
            animation.Play("DoorOpen", 0, 0f);

        }
        else if (gameObject.Equals(clsoeTrigger))
        {
            clsoeTrigger.SetActive(false);
            animation.Play("DoorClosed", 0, 0f);
        }
    }
}
