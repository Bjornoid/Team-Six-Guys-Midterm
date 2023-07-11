using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBombPickup : MonoBehaviour, IPickup
{
    int monkeyBombCurr;
    int monkeyBombMax = 3;

    void Start()
    {
        monkeyBombCurr = monkeyBombMax;
    }

    public void pickup()
    {

        Throwable item = gameManager.instance.player.GetComponent<Throwable>();
        item.setObjToThrow(gameManager.instance.monkeyBomb);
        gameManager.instance.stunGrenadeUI.SetActive(false);
        gameManager.instance.monkeyBombUI.SetActive(true);
        gameManager.instance.grenadeAmountText.text = 3.ToString();
        item.throwTimes = 3;
        item.thrown = 0;
        Destroy(gameObject);
    }
}
