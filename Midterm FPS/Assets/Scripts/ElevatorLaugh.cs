using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorLaugh : MonoBehaviour
{
    [SerializeField] GunStats ak;
    private void Start()
    {
        gameManager.instance.fuelUI.SetActive(true);
        gameManager.instance.playerScript.gunPickup(ak);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.hotelSpirit);
            gameManager.instance.playerScript.dropGun();
            gameManager.instance.fuelUI.SetActive(false);
            StartCoroutine(loadNextLevel());
        }
    }

    IEnumerator loadNextLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(gameManager.levelToLoad);
    }
}
