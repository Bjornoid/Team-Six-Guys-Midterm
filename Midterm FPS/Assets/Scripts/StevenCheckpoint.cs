using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StevenCheckpoint : MonoBehaviour
{

    [SerializeField] Renderer model;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.playerSpawnPosition.transform.position != transform.position)
        {
            gameManager.instance.playerSpawnPosition.transform.position = transform.position;
            StartCoroutine(flashColor());
        }
    }
    IEnumerator flashColor()
    {
        model.material.color = Color.red;
        gameManager.instance.checkpointPopUp.SetActive(true);
        yield return new WaitForSeconds(6.0f);
        model.material.color = Color.white;
        gameManager.instance.checkpointPopUp.SetActive(false);

    }

}
