using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPos;
    [SerializeField] int numToSpawn;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] GameObject spawnObj;
    int numSpawned;
    bool playerInRange;
    bool isSpawning;
    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.UpdateGameGoal(numToSpawn);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !isSpawning && numSpawned < numToSpawn)
        {
            StartCoroutine(spawn());
        }
    }

    IEnumerator spawn()
    {
        isSpawning = true;
        Instantiate(spawnObj, spawnPos[Random.Range(0, spawnPos.Length)].position, transform.rotation);
        numSpawned++;
        yield return new WaitForSeconds(timeBetweenSpawns);
        isSpawning = false;
    }
}
