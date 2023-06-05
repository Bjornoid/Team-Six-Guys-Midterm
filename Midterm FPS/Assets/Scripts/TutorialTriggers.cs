using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    [SerializeField] Animator myDoor = null;

    public GameObject firstUI;
    public GameObject secondUI;
    public GameObject firstTrigger;
    public GameObject secondTrigger;

    void OnTriggerEnter(Collider other)
    { 
        if (gameObject.Equals(firstTrigger))
        {
            firstUI.SetActive(false);
            secondUI.SetActive(true);
        }
        else if (gameObject.Equals(secondTrigger)) 
        {
                myDoor.Play("DoorOpen", 0, 0f);
                secondTrigger.SetActive(false);
        }
    }
}
 