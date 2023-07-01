using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private Transform player;

    public Transform minimapOverlay;

    private GameObject[] enemies;
    void Start()
    {
        player = gameManager.instance.player.transform;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }


    void Update()
    {
        transform.position = player.position + Vector3.up * 5f;
        HandleEnemyVisible();
        RotateOverlay();
    }

    private void HandleEnemyVisible()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(Vector3.Angle(player.forward, enemies[i].transform.position - player.position) <= 100);
        }
    }

    private void RotateOverlay()
    {
        minimapOverlay.localRotation = Quaternion.Euler(0, 0, -player.eulerAngles.y - 100);
    }
}
