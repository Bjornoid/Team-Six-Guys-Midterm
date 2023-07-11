using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonkeyBomb : MonoBehaviour
{
    [SerializeField] GameObject landingEffect;
    [SerializeField] Vector3 effectOffset;

    [Header("----- Explosion settings -----")]
    [SerializeField] float explosionDelay = 8f;
    [SerializeField] float explosionRadius = 10f;
    [SerializeField] Animator animator;

    [Header("----- Audio -----")]
    [SerializeField] AudioSource aud;

    float countdown;
    bool hasExploded;
    GameObject[] enemies;

    private void Start()
    {
        //animator.enabled = false;
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.monkeyBomb,aud);
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
            else if (countdown <= countdown / 2)
            {
                //animator.Play("anim_open");

            }
            else
                distractEnemies();
        }
    }

    void distractEnemies()
    {
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

    void explode()
    {
        GameObject explosionEffect = Instantiate(landingEffect, transform.position + effectOffset, Quaternion.identity);

        Destroy(explosionEffect, 4f);

        //play sound
        //kill enemies close enough to monkey bomb
        kill();

        Destroy(gameObject, .7f);
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
