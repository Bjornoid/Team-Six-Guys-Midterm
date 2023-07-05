using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.statePaused();
            gameManager.instance.activeMenu = gameManager.instance.winMenu;
            gameManager.instance.activeMenu.SetActive(true);
            gameManager.instance.eventSystem.SetSelectedGameObject(gameManager.instance.winFirstButton);
        }
    }
}
