using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.Net.Mail;

public class TerroristAI : MonoBehaviour, IDamage, ISlow
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
    Vector3 startingPos;
    Vector3 playerDir;
    public bool inRange;
    float angleToPlayer;
    public bool isShooting;
    bool destinationChosen;
    float stoppingDistanceOrig;
    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        stoppingDistanceOrig = agent.stoppingDistance;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
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
        if (!destinationChosen & agent.remainingDistance < 0.05 && !isDead)
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

        Debug.DrawRay(headPos.position, playerDir);
        Debug.Log(angleToPlayer);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewConeAngle)
            {
                agent.SetDestination(gameManager.instance.player.transform.position);
                if (agent.remainingDistance <= agent.stoppingDistance && !isDead)
                {
                    facePlayer();
                }
                if (!isShooting && shootPos != null)
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
        Instantiate(bullet, shootPos.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
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
        StartCoroutine(flashColor());
        agent.SetDestination(gameManager.instance.player.transform.position);
        if (HP <= 0)
        {
            isDead = true;

            StopAllCoroutines(); // Stop flash and shoot

            gameManager.instance.UpdateGameGoal(-1); // Zombie dies

            animator.SetBool("Dead", true);

            agent.enabled = false; // Stops the enemy from moving

            animator.ResetTrigger("Attack"); // zombie stops attacking when dead

            GetComponent<Animator>().enabled = false;

            GetComponent<CapsuleCollider>().enabled = false; // Disables dmg collider

            gameObject.GetComponent<NavMeshAgent>().enabled = false;

            StartCoroutine(TimeToDelete());
             
        }
        IEnumerator flashColor()
        {
            model.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            model.material.color = Color.white;
        }
    }
    IEnumerator TimeToDelete()
    {
        yield return new WaitForSeconds(timeBeforeDelete);

        Destroy(gameObject);
    }

    public void slow(float percent)
    {
        agent.speed *= percent;
    }

    public IEnumerator getStunned()
    {
        float original = agent.stoppingDistance;
        agent.stoppingDistance = agent.remainingDistance;
        agent.speed *= .5f;

        yield return new WaitForSeconds(6);

        agent.speed *= 2;
        agent.stoppingDistance = original;
    }
}
