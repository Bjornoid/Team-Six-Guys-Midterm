using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonkeyBomb : MonoBehaviour
{
    [SerializeField] GameObject landingEffect;
    [SerializeField] Vector3 effectOffset;

    [Header("Explosion settings")]
    [SerializeField] float explosionDelay = 6f;
    [SerializeField] float explosionRadius = 10f;

    [Header("Audio Effects")]

    float countdown;
    bool hasExploded;
    GameObject[] enemies;

    private void Start()
    {
        countdown = explosionDelay;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) 
        {
            IDistract distract = enemy.GetComponent<IDistract>();

            if (distract != null) 
            {
                distract.getDistracted();
                enemy.GetComponent<NavMeshAgent>().stoppingDistance = Random.Range(4, explosionRadius / 2);
            }
        }
    }

    private void Update()
    {
        if (!hasExploded)
        {
            countdown -= Time.deltaTime;
            
            if (countdown <= 0)
            {
                explode();
                hasExploded = true;
            }
            else
                distractEnemies();
        }
    }

    void distractEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<NavMeshAgent>() != null)
                enemy.GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
        }
    }

    void explode()
    {
        GameObject explosionEffect = Instantiate(landingEffect, transform.position + effectOffset, Quaternion.identity);

        Destroy(explosionEffect, 4f);

        //play sound

        //kill enemies close enough to monkey bomb
        kill();

        Destroy(gameObject);
    }

    void kill()
    {
        Collider[] cs = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider c in cs)
        {
            IDamage dmg = c.GetComponentInParent<IDamage>();

            if (dmg != null && c.GetComponentInParent<PlayerControls>() == null)
                dmg.takeDamage(100);
        }
    }



}
