using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] int destroyTimer;
    [SerializeField] bool isTurret;

    [SerializeField] Rigidbody rb;

    void Start()
    {
        Destroy(gameObject, destroyTimer);
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isTurret)
        {
            IDamage damageable = other.GetComponent<IDamage>();

            if (damageable != null)
            {
                damageable.takeDamage(damage);
            }
            
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                gameManager.instance.playerScript.emptyFuel();
            }
        }

        Destroy(gameObject);
    }
}
