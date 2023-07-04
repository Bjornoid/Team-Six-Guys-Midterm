using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour, IDamage
{
    [SerializeField] Renderer model;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] float spawnSpeed;
    [SerializeField] int numSpawned;
    [SerializeField] GameObject spawnEntity;
    [SerializeField] int hp;
    bool isSpawning;
    static bool playerInRange;
    bool isStun;

    void Start()
    {
        gameManager.instance.UpdateGameGoal(numSpawned);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange= true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange= false;
        }

    }

    // Update is called once per frame
    void Update()
    {
    if(playerInRange && !isSpawning) 
        {
            StartCoroutine(spawnContinous());
        }
    }
    IEnumerator spawnContinous()
    {
        isSpawning= true;
        gameManager.instance.UpdateGameGoal(1);
        Instantiate(spawnEntity, spawnPos[Random.Range(0,spawnPos.Length)].position, transform.rotation);
        numSpawned++;
        yield return new WaitForSeconds(spawnSpeed);
        isSpawning= false;
    }
    public void takeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(flashColor());
        }
        IEnumerator flashColor()
        {
            model.material.color = Color.blue;
            yield return new WaitForSeconds(0.1f);
            model.material.color = Color.white;
        }
    }

    public static void playerNotInRange()
    {
        playerInRange = false;
    }

    public void getStunned()
    {

    }
}
