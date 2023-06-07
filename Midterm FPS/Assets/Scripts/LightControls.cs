using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControls : MonoBehaviour
{
    [SerializeField] public float flickerDelay; // change to edit delay between flickers
    [Header("----- Flicker Delay Range -----")]
    [Range(0f, 20f)][SerializeField] public float rangeX;
    [Range(0f, 20f)][SerializeField] public float rangeY;

    bool isFlickering; // checks to see if light is flickering

    // Update is called once per frame
    void Update()
    {
        if (!isFlickering)
        {
            StartCoroutine(Flicker());
        }
    }

    IEnumerator Flicker()
    {
        isFlickering = true;

        this.GetComponent<Light>().enabled = false; // turns off the light

        flickerDelay = Random.Range(rangeX, rangeY); // random flicker

        yield return new WaitForSeconds(flickerDelay); // delay between flickers

        this.GetComponent<Light>().enabled = true; // turns on the light

        isFlickering = false;
    }
}
