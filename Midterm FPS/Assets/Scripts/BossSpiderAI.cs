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
    [SerializeField] GameObject spiderNest;
    [SerializeField] Vector3 spiderNestDestination;
    [SerializeField] Vector3 phase2Destination;

    [Header("----- Enemy Stats -----")]
    [SerializeField] int HP;
    [SerializeField] int playerLookSpeed;
    [SerializeField] int viewConeAngle;
    [SerializeField] int roamDistance;
    [SerializeField] int roamTimer;
    [SerializeField] float timeBeforeDelete;


    [Header("----- Weapon Stats -----")]
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    Vector3 startingPos;
    Vector3 playerDir;
    public bool inRange;
    float angleToPlayer;
    public bool isShooting;
    bool destinationChosen;
    float stoppingDistanceOrig;
    public GameObject[] droppedItem;
    bool phase2;
    // Start is called before the first frame update
    void Start()
    {
        phase2 = false;
        startingPos = transform.position;
        stoppingDistanceOrig = agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isActiveAndEnabled)
        {
            animator.SetFloat("Speed", agent.velocity.normalized.magnitude);
            if (inRange && !canSeePlayer())
            {
                StartCoroutine(roam());

            }
            else if (agent.destination != gameManager.instance.player.transform.position)
            {
                StartCoroutine(roam());
            }
        }
    }

    IEnumerator roam()
    {
        if (!destinationChosen & agent.remainingDistance < 0.05)
        {
            destinationChosen = true;

            agent.stoppingDistance = 0;

            yield return new WaitForSeconds(roamTimer);
            destinationChosen = false;

            Vector3 randomPos = Random.insideUnitSphere * roamDistance;
            randomPos += startingPos;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomPos, out hit, roamDistance, 1);
            agent.SetDestination(hit.position);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    bool canSeePlayer()
    {
        if (!phase2)
        {
            agent.stoppingDistance = stoppingDistanceOrig;
            playerDir = gameManager.instance.player.transform.position - headPos.position;
            angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

            Debug.DrawRay(headPos.position, playerDir);
            Debug.Log(angleToPlayer);

            RaycastHit hit;
            if (Physics.Raycast(headPos.position, playerDir, out hit))
            {
                if (hit.collider.CompareTag("Player") && angleToPlayer <= viewConeAngle)
                {
                    agent.SetDestination(gameManager.instance.player.transform.position);
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        facePlayer();
                    }
                    if (!isShooting)
                    {
                        StartCoroutine(shoot());
                    }
                    return true;
                }
            }
            agent.stoppingDistance = 0;
            return false;
        }
        else
        {
            agent.SetDestination(phase2Destination);

            return false;
        }
    }

    void PhaseTwo()
    {
        StopAllCoroutines();
        phase2 = true;
       
        Instantiate(spiderNest, spiderNestDestination, transform.rotation);
    }


    IEnumerator shoot()
    {
        isShooting = true;
        animator.SetTrigger("Attack");
        CreateBullet();
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }


    public void CreateBullet()
    {
        Instantiate(bullet, shootPos.position, transform.rotation);
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

        if (HP == 2)
        {
            PhaseTwo();
        }

        if (HP <= 0)
        {
            StopAllCoroutines();
            gameManager.instance.UpdateGameGoal(-1);
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

    public IEnumerator getStunned()
    {
        //nothing lol
        //maybe play a laughing noise like haha bitch you can't stun my big ass
        yield return null;
    }
}
