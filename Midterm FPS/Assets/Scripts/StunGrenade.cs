using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenade : MonoBehaviour
{
    [SerializeField] GameObject landingEffect;
    [SerializeField] Vector3 effectOffset;

    [Header("Explosion settings")]
    [SerializeField] float explosionDelay = .1f;
    [SerializeField] float explosionRadius = 10f;

    [Header("----- Audio -----")]
    [SerializeField] AudioSource aud;

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
        gameManager.instance.audioManager.PlaySFX(gameManager.instance.audioManager.stunGrenade);
        //affect nearby enemies/player if too close
        stun();


        Destroy(gameObject, 1);
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
