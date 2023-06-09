using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    //List used so that the triggers know which trigger they are by index ------> Every Trigger needs to set itself in the right index
    public List<GameObject> triggers;
    //Lists that track the variables that the triggers need to use ------> Every Trigger only needs to know about the variables that it's changing
    public List<GameObject> UIs;
    public List<GameObject> targets;
    public GameObject currDoorP;

    public GameObject key;
    
    void OnTriggerEnter(Collider other)
    {
        if (gameObject.Equals(triggers[0]))
        {
            triggers[0].SetActive(false);
            UIs[0].SetActive(false);
            UIs[1].SetActive(true);
        }
        else if (gameObject.Equals(triggers[1]) && gameManager.instance.getEnemiesRemaining() <= 0)
        {
            triggers[1].SetActive(false);
            DoorController.rotateDoor(currDoorP, 200);
            
        }
        else if (gameObject.Equals(triggers[2]))
        {
            triggers[2].SetActive(false);
            DoorController.rotateDoor(currDoorP, 90);
            UIs[0].SetActive(false);
            UIs[1].SetActive(true);
        }
        else if (gameObject.Equals(triggers[3]))
        {
            UIs[0].SetActive(false);
            UIs[1].SetActive(true);
            gameManager.instance.updateTargetCount(targets.Count);
            for (int i = 0; i < targets.Count; i++) 
            {
                targets[i].SetActive(true);
            }
            triggers[3].SetActive(false);
        }
        else if (gameObject.Equals(triggers[4]) && gameManager.instance.getTargetCount() <= 0) 
        {
            triggers[4].SetActive(false);       
        }
        else if (gameObject.Equals(triggers[5]))
        {
            triggers[5].SetActive(false);
            UIs[0].SetActive(false);
            UIs[1].SetActive(true);
            Destroy(key);
            DoorController.rotateDoor(currDoorP, -110);
            triggers[6].SetActive(true);
        } 
        else if (gameObject.Equals(triggers[6]))
        {
            triggers[6].SetActive(false);
            triggers[7].SetActive(true);
            UIs[0].SetActive(false);
            UIs[1].SetActive(true);
        }
        else if (gameObject.Equals(triggers[7]))
        {
            triggers[7].SetActive(false);
            UIs[0].SetActive(false);
            UIs[1].SetActive(true);
            gameManager.instance.statePaused();
        }
    }
}
 