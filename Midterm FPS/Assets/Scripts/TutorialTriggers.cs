using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    [SerializeField] GameObject firstUI;
    [SerializeField] GameObject secondUI;

    [SerializeField] GameObject firstTrigger;
    [SerializeField] GameObject secondTrigger;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject == firstTrigger)
            {
                gameManager.instance.displayUI(firstUI);
            }
            else if (gameObject == secondTrigger)
            {
                gameManager.instance.displayUI(secondUI, firstUI);
            }
        }
    }
}
