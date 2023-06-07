using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControls : MonoBehaviour
{
    [SerializeField] float flickerDelay; // change to edit delay between flickers
    [Header("----- Flicker Delay Range -----")]
    [Range(0f, 20f)][SerializeField] float rangeX;
    [Range(0f, 20f)][SerializeField] float rangeY;

    Light lightFlicker;
    //Material materialFlicker;
    bool isFlickering; // checks to see if light is flickering

    // Start is called before the first frame update
    void Start()
    {
        lightFlicker = GetComponent<Light>();
        
    }

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

        lightFlicker.enabled = false; // turns off the light

        flickerDelay = Random.Range(rangeX, rangeY); // random flicker

        yield return new WaitForSeconds(flickerDelay); // delay between flickers

        lightFlicker.enabled = true; // turns on the light

        isFlickering = false;
    }
}
