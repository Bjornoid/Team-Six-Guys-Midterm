using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medkit : MonoBehaviour, IPickup
{
    // Start is called before the first frame update
    public void pickup()
    {
        gameManager.instance.playerScript.medKit();
        Destroy(gameObject);
    }
}
