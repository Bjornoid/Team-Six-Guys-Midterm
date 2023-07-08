using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class pickupObject : MonoBehaviour
{
    bool inRange;
    [SerializeField] UnityEvent action;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Input.GetButtonDown("Pickup"))
            {
                gameManager.instance.pickupPrompt.SetActive(false);
                action.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            gameManager.instance.pickupPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            gameManager.instance.pickupPrompt.SetActive(false);
        }
    }
}
