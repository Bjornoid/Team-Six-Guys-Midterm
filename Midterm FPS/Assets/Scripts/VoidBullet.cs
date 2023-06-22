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
    [SerializeField] float reloadSpeed;

    int voidAmmo = 4;
    int magCap = 24;
    int magOrig;
    int ammoOrig;
    bool isShooting;
    bool isReloading;

    // Start is called before the first frame update
    void Start()
    {
        ammoOrig = voidAmmo;
        magOrig = magCap;

        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && gameManager.instance.playerScript.hasWonderWeapon == true && !isShooting && voidAmmo > 0 && magCap > 0)
        {
            StartCoroutine(Shoot());
        }
        if(Input.GetButtonDown("Reload") && !isReloading && voidAmmo <= 3)
        {
            StartCoroutine(ReloadVoid());
        }
        if (gameManager.instance.playerScript.ammoPickedUp == true)
        {
            magCap = magOrig;
        }
    }

    public void ShootVoid()
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

        voidAmmo--;
        magCap--;

        ShootVoid();

        yield return new WaitForSeconds(shootRate);

        isShooting = false;
    }

    IEnumerator ReloadVoid()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadSpeed);

        voidAmmo = ammoOrig;

        isReloading = false;
    }    
}

