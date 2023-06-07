using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{ 
    public List<GameObject> UIs;
    public List<GameObject> targets;
    public List<GameObject> triggers;
    public List<Animator> animators;

    
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
            animators[0].Play("DoorOpen", 0, 0f);
            triggers[1].SetActive(false);
        }
        else if (gameObject.Equals(triggers[2]))
        {
            animators[0].Play("DoorClosed", 0, 0f);
            triggers[2].SetActive(false);
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
            animators[1].Play("MovePlatform");
        }
        else if (gameObject.Equals(triggers[5]))
        {
            triggers[5].SetActive(false);
            UIs[0].SetActive(false);
            UIs[1].SetActive(true);
            Destroy(key);
        }
        else if (gameObject.Equals(triggers[6]))
        {
            triggers[6].SetActive(false);
            animators[2].Play("DoorOpen");
        }
        
    }
}
 