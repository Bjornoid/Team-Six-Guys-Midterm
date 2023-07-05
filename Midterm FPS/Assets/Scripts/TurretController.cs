using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private Transform Player;
    private float dist;
    public float maxDist;
    public Transform Head, ShootPos;
    public float playerFaceSpeed;
    Vector3 playerDir;
    public GameObject projectile;
    public float projectileSpeed;
    public float fireRate;
    public float nextFire;
    private bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(Player.position, Head.position);

        if (dist <= maxDist)
        {
            playerDir = Player.transform.position - Head.position;
            Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
            Head.rotation = Quaternion.Lerp(Head.rotation, rot, Time.deltaTime * playerFaceSpeed);
            if (!isShooting)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        GameObject clone = Instantiate(projectile, ShootPos.position, Head.rotation);
        clone.GetComponent<Rigidbody>().AddForce(Head.forward * projectileSpeed);
        yield return new WaitForSeconds(fireRate);
        isShooting = false;

        Destroy(clone, 2);

    }
}

