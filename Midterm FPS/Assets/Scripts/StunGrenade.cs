using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenade : MonoBehaviour
{
    [SerializeField] GameObject landingEffect;
    [SerializeField] Vector3 effectOffset;

    [Header("Explosion settings")]
    [SerializeField] float explosionDelay = 1.5f;
    [SerializeField] float explosionRadius = 10f;

    [Header("Audio Effects")]
    float countdown;
    bool hasExploded;

    private void Start()
    {
        countdown = explosionDelay;
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
        }
    }

    void explode()
    {
        GameObject explosionEffect = Instantiate(landingEffect, transform.position + effectOffset, Quaternion.identity);

        Destroy(explosionEffect, 4f);

        //play sound

        //affect nearby enemies/player if too close
        stun();


        Destroy(gameObject);
    }


    void stun()
    {
        Collider[] cs = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider c in cs) 
        { 
            IDamage dmg = c.GetComponentInParent<IDamage>();

            if (dmg != null)
                dmg.getStunned();
        }
    }
 
}
