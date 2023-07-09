using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCutsceneEnter : MonoBehaviour
{

    GameObject playerCamera;

    GameObject cutsceneCamera;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        cutsceneCamera.SetActive(true);

        playerCamera.SetActive(false);

        StartCoroutine(EndCutscene());
    }

    IEnumerator EndCutscene()
    {
        yield return new WaitForSeconds(10);

        playerCamera.SetActive(true);

        cutsceneCamera.SetActive(false);
    }
}
