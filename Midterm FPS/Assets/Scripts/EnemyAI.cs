using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IDamage
{
    [Header("----- Enemy Components -----")]
    // Start is called before the first frame update
    [SerializeField] Renderer model; // model of enemy
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform headPos;
    [SerializeField] Transform shootPos;
    [SerializeField] Animator animator;
    [SerializeField] Collider weaponCol;

    [Header("----- Enemy Stats -----")]
    [SerializeField] int HP;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] int viewConeAngle;
    [SerializeField] int roamDist;
    [SerializeField] int roamTimer;
    [SerializeField] bool isMelee;

    [Header("----- Weapon Stats -----")]
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;


    Vector3 playerDirection; // direction of the player
    public bool playerInRange; // checks to see if player is in range
    float angleToPlayer;
    bool isShooting; // checks to see if enemy is shooting
    bool chosenDestination;
    Vector3 startingPos;
    float stoppingDistOrig;

    void Start()
    { 
        //animator.Play("DS_onehand_idle_A");
        gameManager.instance.UpdateGameGoal(1); // enemy exists
        stoppingDistOrig = agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.normalized.magnitude);
        if (playerInRange && CanSeePlayer()) 
        {
            StartCoroutine(Roam());
        }
        else if (agent.destination != gameManager.instance.player.transform.position) 
        {
            StartCoroutine(Roam());
        } 
    }

    // Allows enemy to roam
    IEnumerator Roam()
    {
        if (!chosenDestination && agent.remainingDistance < 0.05f)
        {
            chosenDestination = true;

            agent.stoppingDistance = 0;

            Vector3 randomPos = Random.insideUnitSphere * roamDist;
            randomPos += startingPos;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomPos, out hit, roamDist, 1);
            yield return new WaitForSeconds(roamTimer);
            chosenDestination = false;

            agent.SetDestination(hit.position);
        }
    }

    bool CanSeePlayer()
    {
        agent.stoppingDistance = stoppingDistOrig;

        playerDirection = gameManager.instance.player.transform.position - headPos.position; // two positions subtracted from one another gives direction
        angleToPlayer = Vector3.Angle(new Vector3(playerDirection.x, 0, playerDirection.z), transform.forward);

        Debug.DrawRay(headPos.position, playerDirection);
        Debug.Log(angleToPlayer); // spits a number into console (debug purposes)

        RaycastHit hit;

        if (Physics.Raycast(headPos.position, playerDirection, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewConeAngle) // if player gets in range of enemy
            {
                
                agent.SetDestination(gameManager.instance.player.transform.position); // go towards the player

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    //animator.Play("DS_onehand_walk");
                }
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    facePlayer();
                    if (isMelee)
                    {
                        animator.SetTrigger("Swing");
                    }
                }
                if (!isShooting && shootPos != null)
                {
                    if (!isMelee)
                    {
                        StartCoroutine(shoot());
                    }
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

        yield return new WaitForSeconds(shootRate); // bullet production rate

        isShooting = false;
    }

    void OnTriggerEnter(Collider other) // when player is in range of enemy
    {
        if (other.CompareTag("Player")) 
        {
            playerInRange = true;
        }
    }
    
    void OnTriggerExit(Collider other) // when player is outside range of enemy
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    
    void facePlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * playerFaceSpeed);
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;

        StartCoroutine(flashColor());

        if (HP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1); // enemy dies

            Destroy(gameObject); // destroy the object 
        }
    }

    IEnumerator flashColor()
    {
        model.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        model.material.color = Color.white;
    }

    void weaponColOff()
    {
        weaponCol.enabled = false;
    }

    void weaponColOn()
    {
        weaponCol.enabled = true;
    }
}
