using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSpiderAI : MonoBehaviour, IDamage
{
    [Header("----- Components -----")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform headPos;
    [SerializeField] Transform shootPos;
    [SerializeField] Animator animator;
    [SerializeField] Collider biteCol;
    [SerializeField] GameObject spiderNest;

    [Header("----- Enemy Stats -----")]
    [SerializeField] int HP;
    [SerializeField] int playerLookSpeed;
    [SerializeField] int viewConeAngle;
    [SerializeField] int roamDistance;
    [SerializeField] int roamTimer;
    [SerializeField] float timeBeforeDelete;


    [Header("----- Attack Stats -----")]
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    int startingHP;
    Transform startingPos;
    Vector3 playerDir;
    public bool inRange;
    public bool isAttacking;
    bool destinationChosen;
    bool phase2;
    bool isStun;
    bool nestSpawned;
    public GameObject[] droppedItem;
    float angleToPlayer;
    float stoppingDistanceOrig;
    // Start is called before the first frame update
    void Start()
    {
        startingHP = HP;
        startingPos = transform;
        phase2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isActiveAndEnabled)
        {
            animator.SetFloat("Speed", agent.velocity.normalized.magnitude);
            attack();
        }
    }


    void attack()
    {
        if (!phase2)
        {
            playerDir = gameManager.instance.player.transform.position - headPos.position;
            angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);
            Debug.DrawRay(headPos.position, playerDir);
            Debug.Log(angleToPlayer);
            facePlayer();

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.SetDestination(gameManager.instance.player.transform.position);
            }


            if (!isAttacking && gameManager.instance.player.transform.position.y - transform.position.y > 5)
            {
                StartCoroutine(shoot());
            }
            else if (!isAttacking && gameManager.instance.player.transform.position.y - transform.position.y < 5 && agent.stoppingDistance > Vector3.Distance(gameManager.instance.player.transform.position, headPos.position))
                StartCoroutine(bite());
        }
        else
        {
            animator.SetTrigger("Spawn");
            agent.SetDestination(startingPos.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, startingPos.rotation, Time.deltaTime);
            if (!nestSpawned)
            {
                spiderNest.SetActive(true);
                nestSpawned = true;
            }
        }
    }


    IEnumerator shoot()
    {
        isAttacking = true;
        animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(shootRate);
        isAttacking = false;
    }

    IEnumerator bite()
    {
        isAttacking = true;
        animator.SetTrigger("Bite");
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
    }


    public void CreateBullet()
    {
        Instantiate(bullet, shootPos.position, transform.rotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    void facePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerLookSpeed);
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;

        if (HP <= startingHP / 2 && HP >= startingHP / 4)
        {
            phase2 = true;
        }
        else
            phase2 = false;

        if (HP <= 0)
        {
            StopAllCoroutines();
            animator.SetBool("Dead", true);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            ItemDrop();
            spiderNest.SetActive(false);
            StartCoroutine(TimeToDelete());
        }
        else
        {
            agent.SetDestination(gameManager.instance.player.transform.position);
            StartCoroutine(flashColor());
        }
    }

    IEnumerator flashColor()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }

    private void ItemDrop()
    {
        for (int i = 0; i < droppedItem.Length; i++)
        {
            Instantiate(droppedItem[i], transform.position, Quaternion.identity);
        }
    }
    IEnumerator TimeToDelete()
    {
        yield return new WaitForSeconds(timeBeforeDelete);

        Destroy(gameObject);
    }

    public void getStunned()
    {
        //could play a sound effect laughing cuz you can't stun this bag mf
    }

    void biteColOn()
    {
        biteCol.enabled = true;
    }

    void biteColOff()
    {
        biteCol.enabled = false;
    }
}
