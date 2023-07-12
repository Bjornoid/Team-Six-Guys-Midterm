using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.Net.Mail;

public class SpiderAI : MonoBehaviour, IDamage, IDistract
{
    [Header("----- Components -----")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform headPos;
    [SerializeField] Transform shootPos;
    [SerializeField] Animator animator;
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

    [Header("----- Audio -----")]
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] hissSounds;
    [SerializeField][Range(0, 1)] float hissVol;
    [SerializeField] AudioClip[] deathSounds;
    [SerializeField][Range(0, 1)] float deathVol;

    Vector3 startingPos;
    Vector3 playerDir;
    public bool inRange;
    float angleToPlayer;
    bool isStun;
    bool isDistracted;
    bool isShrunk;
    bool isDead;
    public bool isShooting;
    bool destinationChosen;
    float stoppingDistanceOrig;
    public GameObject[] droppedItem;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        stoppingDistanceOrig = agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isActiveAndEnabled && !isDistracted)
        {
            if (isShrunk)
            {
                if (Vector3.Distance(transform.position, gameManager.instance.player.transform.position) < 2.3)
                    takeDamage(20);
            }
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
        if (!isShrunk && !destinationChosen & agent.remainingDistance < 0.05)
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
        agent.stoppingDistance = stoppingDistanceOrig;
        playerDir = gameManager.instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

        //Debug.DrawRay(headPos.position, playerDir);
        //Debug.Log(angleToPlayer);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewConeAngle && !isDead)
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



    IEnumerator shoot()
    {
        isShooting = true;
        animator.SetTrigger("Shoot");
        gameManager.instance.audioManager.PlaySFXArray(gameManager.instance.audioManager.spiderHiss, aud);
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
        gameManager.instance.audioManager.PlaySFXArray(gameManager.instance.audioManager.spiderHiss, aud);

        if (HP <= 0)
        {
            isDead = true;
            StopAllCoroutines();
            gameManager.instance.UpdateGameGoal(-1);
            animator.SetBool("Dead", true);
            gameManager.instance.audioManager.PlaySFXArray(gameManager.instance.audioManager.spiderDeath, aud);
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            ItemDrop();
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
        if (!isStun)
            StartCoroutine(stunFor(3.5f));
    }

    IEnumerator stunFor(float time)
    {
        isStun = true;

        agent.enabled = false;
        yield return new WaitForSeconds(time);
        agent.enabled = true;
        isStun = false;
    }

    public void getDistracted()
    {
        isDistracted = true;
    }

    public void getShrunk()
    {
        if (!isShrunk)
            StartCoroutine(shrinkFor(8));
    }

    IEnumerator shrinkFor(float time)
    {
        isShrunk = true;
        int hpBefore = HP;
        HP = 1;

        transform.localScale *= .5f;
        yield return new WaitForSeconds(time);
        transform.localScale *= 2;
        HP = hpBefore / 2;

        isShrunk = false;
    }
}
