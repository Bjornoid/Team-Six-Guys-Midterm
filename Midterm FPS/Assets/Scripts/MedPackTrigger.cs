using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedPackTrigger : MonoBehaviour
{
    public GameObject medPack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.medKit();
            Destroy(medPack);
        }
    }

    // Update is called once per frame
}
