using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpUI : MonoBehaviour
{
    public Vector2 targetPos;
    public Transform UI;
    public float duration;

    void OnTriggerEnter(Collider other)
    {
        gameManager.instance.canPause = false;
        StartCoroutine(lerpUI());
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    IEnumerator lerpUI()
    {
        float time = 0;
        Vector2 startingPos = UI.localPosition;
        while (time < duration) 
        { 
            UI.localPosition = Vector2.Lerp(startingPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        UI.localPosition = targetPos;
        Time.timeScale = 0;
    }
}
