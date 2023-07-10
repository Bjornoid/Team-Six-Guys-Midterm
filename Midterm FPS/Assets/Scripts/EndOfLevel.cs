using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField] GameObject menu;                   //Menu to fill in for boss level and tutorial
    [SerializeField] bool needsPower;                   //Check if the elevator needs power from a switch
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!needsPower)
            {
                gameManager.instance.statePaused();         //Pause the game

                if (menu != null && gameManager.instance.winMenu != menu)
                {
                    gameManager.instance.winMenu = menu;    //If menu field is filled in, override default win menu
                    gameManager.instance.amountOfDeathsText.text = PlayerPrefs.GetInt("deaths").ToString();     //Sets death text at end of game to current deaths in this playthrough
                }

                //set win menu as active menu and turn it on
                gameManager.instance.activeMenu = gameManager.instance.winMenu;
                gameManager.instance.activeMenu.SetActive(true);
                Button[] btns = gameManager.instance.winMenu.GetComponentsInChildren<Button>();
                //set first button of menu to be selected for keyboard menu navigation 
                gameManager.instance.eventSystem.SetSelectedGameObject(btns[0].gameObject);
            }
        }
    }
}
