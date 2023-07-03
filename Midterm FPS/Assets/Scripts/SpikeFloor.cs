using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFloor : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] float dmgInterval;
    bool canDmg = true;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        IDamage damageable = other.GetComponent<IDamage>();
        if(damageable != null && canDmg)
        {
            damageable.takeDamage(dmg);
            StartCoroutine(damageDelay());
        }
    }
    private IEnumerator damageDelay()
    {
        canDmg = false;
        yield return new WaitForSeconds(dmgInterval);
        canDmg = true;
    }
    // Update is called once per frame

}
