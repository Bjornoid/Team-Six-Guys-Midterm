using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBombPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Throwable item = gameManager.instance.player.GetComponent<Throwable>();
            item.setObjToThrow(gameManager.instance.monkeyBomb);
            item.throwTimes = 3;
            item.thrown = 0;
            Destroy(gameObject);
        }
    }
}
