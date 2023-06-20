using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoidBullet : MonoBehaviour
{
    [SerializeField] GameObject voidObj;
    [SerializeField] float bulletForce;
    [SerializeField] int speed;
    [Range(0f, 10f)][SerializeField] float voidDuration;
    [SerializeField] Rigidbody rb;
    [SerializeField] ParticleSystem particles;
    [SerializeField] float shootRate;

    bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameManager.instance.playerScript.hasWonderWeapon == true && !isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    void ShootVoid()
    {

        GameObject voidObject = Instantiate(voidObj, transform.position, transform.rotation);
        Rigidbody voidRb = voidObject.GetComponent<Rigidbody>();

       if (voidRb != null)
       {
           voidRb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
       }

        StartCoroutine(DestroyVoid(voidObject));
    }

    IEnumerator DestroyVoid(GameObject _void)
    {
        yield return new WaitForSeconds(voidDuration);

        Destroy(_void);
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        ShootVoid();

        yield return new WaitForSeconds(shootRate);

        isShooting = false;
    }
}

