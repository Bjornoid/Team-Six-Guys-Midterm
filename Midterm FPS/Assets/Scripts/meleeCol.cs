using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeCol : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] bool isPoison;
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
