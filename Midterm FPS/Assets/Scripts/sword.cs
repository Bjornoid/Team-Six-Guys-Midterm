using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    [SerializeField] int damage;
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        IDamage damageable = other.GetComponent<IDamage>();

        if (damageable != null)
        {
            damageable.takeDamage(damage);
        }

    }
}
