using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    [SerializeField] Animator myDoor = null;
    //[SerializeField] Animator girl = null;

    public GameObject firstUI;
    public GameObject secondUI;
    public GameObject thirdUI;
    public GameObject fourthUI;
    public GameObject fifthUI;
    public GameObject firstTrigger;
    public GameObject secondTrigger;
    public GameObject thirdTrigger;
    public GameObject fourthTrigger;
    public GameObject fifthTrigger;
    public GameObject sixthTrigger;
    public GameObject seventhTrigger;
    public Animator animator;
    public GameObject platform;
    public List<GameObject> targets;
    public GameObject key;
    
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
            secondUI.SetActive(false);
            thirdUI.SetActive(true);
        }
        else if (gameObject.Equals(fourthTrigger))
        {
            thirdUI.SetActive(false);
            fourthUI.SetActive(true);
            gameManager.instance.updateTargetCount(targets.Count);
            for (int i = 0; i < targets.Count; i++) 
            {
                targets[i].SetActive(true);
            }
            fourthTrigger.SetActive(false);
        }
        else if (gameObject.Equals(fifthTrigger) && gameManager.instance.getTargetCount() <= 0) 
        {
            fifthTrigger.SetActive(false);
            animator.Play("MovePlatform");
        }
        else if (gameObject.Equals(sixthTrigger))
        {
            sixthTrigger.SetActive(false);
            fourthUI.SetActive(false);
            fifthUI.SetActive(true);
            Destroy(key);
        }
        else if (gameObject.Equals(seventhTrigger))
        {
            seventhTrigger.SetActive(false);
            myDoor.Play("DoorOpen");
        }
        
    }
}
 