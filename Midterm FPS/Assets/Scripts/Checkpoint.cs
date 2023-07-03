using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Renderer model;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.playerSpawnPosition.transform.position != transform.position)
        {
            gameManager.instance.playerSpawnPosition.transform.position = transform.position;
            if (model != null)
                StartCoroutine(flashColor());
        }
    }
    IEnumerator flashColor()
    {
        model.material.color = Color.red;
        //gameManager.instance.checkpointpopUp.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        model.material.color = Color.white;
        //gameManager.instance.checkpointpopUp.SetActive(false);
    }
}
