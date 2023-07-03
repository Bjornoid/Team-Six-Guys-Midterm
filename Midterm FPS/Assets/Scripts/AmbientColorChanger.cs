using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AmbientColorChanger : MonoBehaviour
{
    [SerializeField] Color ambientColor;
    [SerializeField] GameObject dirLight;
    [SerializeField] bool setToOrig;
    [SerializeField] float lerpTime;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LerpColor());
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator LerpColor()
    {
        float time = 0;
        while (time < lerpTime) 
        {
            if (!setToOrig)
            {
                dirLight.GetComponent<Light>().color = Color.Lerp(gameManager.instance.ambientColorOrig, ambientColor, time / lerpTime);
                RenderSettings.ambientLight = Color.Lerp(gameManager.instance.ambientColorOrig, ambientColor, time / lerpTime);
                time += Time.deltaTime;
                yield return null;
            }
            else
            {
                dirLight.GetComponent<Light>().color = Color.Lerp(ambientColor, gameManager.instance.ambientColorOrig, time / lerpTime);
                RenderSettings.ambientLight = Color.Lerp(ambientColor, gameManager.instance.ambientColorOrig, time / lerpTime);
                time += Time.deltaTime;
                yield return null;
            }
        }
        if (!setToOrig)
        {
            RenderSettings.ambientLight = ambientColor;
            dirLight.GetComponent<Light>().color = ambientColor;
        }
        else
        {
            RenderSettings.ambientLight = gameManager.instance.ambientColorOrig;
            dirLight.GetComponent<Light>().color = gameManager.instance.ambientColorOrig;
        }
    }
}
