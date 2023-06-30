using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject destroyedObject;

    public void DestroyObject()
    {
        Instantiate(destroyedObject, transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
