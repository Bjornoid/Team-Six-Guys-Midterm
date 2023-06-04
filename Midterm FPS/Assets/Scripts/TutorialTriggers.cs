using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    public GameObject firstUI;
    public GameObject secondUI;

    void OnTriggerEnter(Collider other)
    { 
        firstUI.SetActive(false);
        secondUI.SetActive(true);
    }
}
