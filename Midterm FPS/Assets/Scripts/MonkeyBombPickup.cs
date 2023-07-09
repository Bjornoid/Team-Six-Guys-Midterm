using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBombPickup : MonoBehaviour, IPickup
{
    public void pickup()
    {
        Throwable item = gameManager.instance.player.GetComponent<Throwable>();
        item.setObjToThrow(gameManager.instance.monkeyBomb);
        item.throwTimes = 3;
        item.thrown = 0;
        Destroy(gameObject);
    }
}
