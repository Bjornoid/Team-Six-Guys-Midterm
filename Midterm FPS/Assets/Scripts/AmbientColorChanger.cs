using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AmbientColorChanger : MonoBehaviour
{
    [Header("Fill in if changing")]
    [SerializeField] Color ambientColor;
    [Header("Check if setting back")]
    [SerializeField] bool setToOrig;

    [Header("Other")]
    [SerializeField] GameObject dirLight;
    [SerializeField] float lerpTime;

    private Color origColor;

    private void Start()
    {
        origColor = gameManager.instance.ambientColorOrig;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!setToOrig)
                StartCoroutine(LerpColor());
            else
                StartCoroutine(LerpBack());
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
        Color starting = RenderSettings.ambientLight;
        while (time < lerpTime) 
        {
            dirLight.GetComponent<Light>().color = Color.Lerp(starting, ambientColor, time / lerpTime);
            RenderSettings.ambientLight = Color.Lerp(starting, ambientColor, time / lerpTime);
            time += Time.deltaTime;
            yield return null;
        }
       
        RenderSettings.ambientLight = ambientColor;
        dirLight.GetComponent<Light>().color = ambientColor;
    }

    IEnumerator LerpBack()
    {
        float time = 0;
        Color starting = RenderSettings.ambientLight;
        while (time < lerpTime)
        {
            dirLight.GetComponent<Light>().color = Color.Lerp(starting, gameManager.instance.ambientColorOrig, time / lerpTime);
            RenderSettings.ambientLight = Color.Lerp(starting, gameManager.instance.ambientColorOrig, time / lerpTime);
            time += Time.deltaTime;
            yield return null;
        }

        RenderSettings.ambientLight = gameManager.instance.ambientColorOrig;
        dirLight.GetComponent<Light>().color = gameManager.instance.ambientColorOrig;
    }
}
