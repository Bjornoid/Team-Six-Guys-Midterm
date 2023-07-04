using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private Transform player;

    private GameObject[] enemies;
    void Start()
    {
        player = gameManager.instance.player.transform;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }


    void Update()
    {
        transform.position = player.position + Vector3.up * 5f;
    }
}
