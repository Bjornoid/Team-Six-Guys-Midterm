using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.Net.Mail;

public class SpiderAI : MonoBehaviour, IDamage
{
    [Header("----- Components -----")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform headPos;
    [SerializeField] Transform shootPos;
    [Header("----- Enemy Stats -----")]
    [SerializeField] int HP;
    [SerializeField] int playerLookSpeed;
    [SerializeField] int viewConeAngle;
    [SerializeField] int roamDistance;
    [SerializeField] int roamTimer;
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
    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.UpdateGameGoal(1);
        startingPos = transform.position;
        stoppingDistanceOrig = agent.stoppingDistance;

    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && !canSeePlayer())
        {
            StartCoroutine(roam());

        }
        else if (agent.destination != gameManager.instance.player.transform.position)
        {
            StartCoroutine(roam());
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



    IEnumerator shoot()
    {
        isShooting = true;
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
        agent.SetDestination(gameManager.instance.player.transform.position);
        StartCoroutine(flashColor());
        if (HP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
            Destroy(gameObject);
        }
    }
    IEnumerator flashColor()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }
}