//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics.Contracts;
//using UnityEngine;

//public class Flamethrower : MonoBehaviour
//{
//    [SerializeField] int damage;
//    [SerializeField] float dist;
//    [SerializeField] float rateOfFire;
//    [SerializeField] [Range(3,7)] float coolDownTimer;
//    [SerializeField] float timeTilCoolDown;

//    bool isCoolingDown;
//    bool isHot;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
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
//}
