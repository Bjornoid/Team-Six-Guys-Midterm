using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPos;
    [SerializeField] float spawnSpeed;
    [SerializeField] int numSpawned;
    [SerializeField] GameObject spawnEntity;
    bool isSpawning;
    bool playerInRange;
    int numDestroyedEnemies;
    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.UpdateGameGoal(numSpawned);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            DestroyAllEntities();
        }
    }

    void DestroyAllEntities()
    {
        GameObject[] entities = GameObject.FindGameObjectsWithTag("Enemy"); // Replace "Enemy" with the appropriate tag for the entities you want to destroy

        foreach (GameObject entity in entities)
        {
            Destroy(entity);
            numDestroyedEnemies++;
        }
        gameManager.instance.UpdateGameGoal(-numDestroyedEnemies);
        numDestroyedEnemies = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !isSpawning)
        {
            StartCoroutine(spawnContinous());
        }
    }
    IEnumerator spawnContinous()
    {
        isSpawning = true;
        gameManager.instance.UpdateGameGoal(1);
        Instantiate(spawnEntity, spawnPos[Random.Range(0, spawnPos.Length)].position, transform.rotation);
        numSpawned++;
        yield return new WaitForSeconds(spawnSpeed);
        isSpawning = false;
    }
   
}
