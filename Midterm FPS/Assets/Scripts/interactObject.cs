using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events; 

public class interactObject : MonoBehaviour
{
    bool inRange;
    [SerializeField] TextMeshProUGUI interactPrompt;
    [SerializeField] string promptText;
    [SerializeField] UnityEvent action;
    
    // Start is called before the first frame update
    void Start()
    {
        interactPrompt.text = promptText;
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
