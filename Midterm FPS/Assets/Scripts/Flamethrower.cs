//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics.Contracts;
//using UnityEngine;

//public class Flamethrower : MonoBehaviour
//{
//    [SerializeField] GameObject flameObj;
//    [SerializeField] int damage;
//    [SerializeField] float dist;
//    [SerializeField] float rateOfFire;
//    [SerializeField][Range(3, 7)] float coolDownTimer;
//    [SerializeField] float timeTilCoolDown;
//    [SerializeField] Rigidbody rb;
//    [SerializeField] ParticleSystem flame;
//    [SerializeField] float flameForce;
//    [SerializeField] int speed;

//    bool isCoolingDown;
//    bool isHot;

//    // Start is called before the first frame update
//    void Start()
//    {
//        rb.velocity = transform.forward * speed;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0) && gameManager.instance.playerScript.hasWonderWeapon == true && !isHot && timeTilCoolDown > 0)
//        {
//            StartCoroutine(Shooting());
//        }
//        if (coolDownTimer <= 0)
//        {
//            StartCoroutine(CoolDown());
//        }
//    }

//    IEnumerator CoolDown()
//    {
//        isCoolingDown = true;
//        yield return new WaitForSeconds(coolDownTimer);
//        rateOfFire = 0;
//        isCoolingDown = false;
//    }

//    IEnumerator Shooting()
//    {
//        isHot = true;
//        timeTilCoolDown--;
//        yield return new WaitForSeconds(rateOfFire);
//        isHot = false;
//    }

//    public void ShootFlame()
//    {
//        GameObject obj = Instantiate(flameObj, transform.position, transform.rotation);
//        Rigidbody rb = obj.GetComponent<Rigidbody>();
//        if (rb != null) 
//        {
//            rb.AddForce(transform.forward * flameForce, ForceMode.Impulse);
//        }
//    }
//}