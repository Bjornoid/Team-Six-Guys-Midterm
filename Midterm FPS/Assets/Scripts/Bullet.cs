using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] int destroyTimer;

    [SerializeField] Rigidbody rb;

    void Start()
    {
        Destroy(gameObject, destroyTimer);
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        IDamage damageable = other.GetComponent<IDamage>();

        if (damageable != null)
        {
            damageable.takeDamage(damage);
        }

        Destroy(gameObject);
    }
}
