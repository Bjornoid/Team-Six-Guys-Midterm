using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialControls : MonoBehaviour
{
    [SerializeField] public float flickerDelay; // change to edit delay between flickers
    [Header("----- Flicker Delay Range -----")]
    [Range(0f, 20f)][SerializeField] public float rangeX;
    [Range(0f, 20f)][SerializeField] public float rangeY;

    Material materialFlicker; // material for the light to flicker
    bool isFlickering; // checks to see if material is flickering 

    // Start is called before the first frame update
    void Start()
    {
        materialFlicker = GetComponent<Renderer>().material; // on start get the material that we need to flicker
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFlickering)
        {
            StartCoroutine(MaterialFlickerRed());
        }
    }

    IEnumerator MaterialFlickerRed()
    {

        isFlickering = true;

        materialFlicker.SetColor("_EmissionColor", Color.red); // turn off light 

        flickerDelay = Random.Range(rangeX, rangeY);

        yield return new WaitForSeconds(flickerDelay);

        materialFlicker.SetColor("_EmissionColor", Color.black); // turn on light

        isFlickering = false;
    }
}
