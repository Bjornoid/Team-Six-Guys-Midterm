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
            if (isPoison)
            {
                StartCoroutine(poisoned(damageable));
            }
        }
    }

    IEnumerator poisoned(IDamage damagable)
    {
        for (int i = 0; i < 5; i++)
        {
            damagable.takeDamage(1);
            yield return new WaitForSeconds(0.7f);
        }
    }
}
