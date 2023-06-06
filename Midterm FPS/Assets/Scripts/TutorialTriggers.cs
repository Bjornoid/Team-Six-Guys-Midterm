using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    [SerializeField] Animator myDoor = null;
    //[SerializeField] Animator girl = null;

    public GameObject firstUI;
    public GameObject secondUI;
    public GameObject firstTrigger;
    public GameObject secondTrigger;
    public GameObject thirdTrigger;
    
    void OnTriggerEnter(Collider other)
    {
        if (gameObject.Equals(firstTrigger))
        {
            firstUI.SetActive(false);
            secondUI.SetActive(true);
        }
        else if (gameObject.Equals(secondTrigger) && gameManager.instance.getEnemiesRemaining() <= 0)
        {
            myDoor.Play("DoorOpen", 0, 0f);
            secondTrigger.SetActive(false);
        }
        else if (gameObject.Equals(thirdTrigger))
        {
            myDoor.Play("DoorClosed", 0, 0f);
            thirdTrigger.SetActive(false);
        }
    }
}
 