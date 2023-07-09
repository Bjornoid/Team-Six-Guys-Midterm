using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events; 

public class interactObject : MonoBehaviour
{
    bool inRange;
    [SerializeField] string promptText;
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
            if (Input.GetButtonDown("Interact"))
            {
                gameManager.instance.interactPrompt.SetActive(false);
                action.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.interactPromptText.text = promptText;
            inRange = true;
            gameManager.instance.interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            gameManager.instance.interactPrompt.SetActive(false);
        }
    }
}
