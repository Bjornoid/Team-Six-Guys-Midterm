using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOOM : MonoBehaviour
{
    [Range(0, 6.9f)] public float blastRadius;
    [Range(0, 6.9f)] public float delay;
    [Range(0, 420)] public float explosionForce;
    float countDown;

    public GameObject explosionEffect;

    bool hasExploded = false;

    private void Start()
    {
        countDown = delay;
    }

    private void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }
    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObject in collidersToDestroy)
        { 
           Destroy destroy = nearbyObject.GetComponent<Destroy>();

            if (destroy != null)
                destroy.DestroyObject();
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, blastRadius);

        foreach(Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }
        }

        Destroy(gameObject);
    }
}
 