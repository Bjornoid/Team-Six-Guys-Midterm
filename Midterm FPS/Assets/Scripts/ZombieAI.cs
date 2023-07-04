using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour, IDamage, ISlow
{
    [Header("----- Zombie Components -----")]
    [SerializeField] Renderer model;
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] public Animator zombieAnim;
    [SerializeField] Transform headPos;
    [SerializeField] Collider handCollider1;
    [SerializeField] Collider handCollider2;

    [Header("----- Zombie Stats -----")]
    [Range(1, 100)][SerializeField] int HP;
    [Range(1, 10)][SerializeField] int playerFaceSpeed; // How fast the Zombie faces towards the player
    [Range(1, 360)][SerializeField] int viewConeAngle;
    [Range(1, 100)][SerializeField] int roamDist; // how far the enemy will roam
    [Range(1, 10)][SerializeField] int roamTimer; // amount of time the enemy takes to move from one positon to another while roaming
    // Start is called before the first frame update

    [Header("----- Damage Stats -----")]
    [SerializeField] float shootRate;

    [SerializeField] float timeBeforeDelete;

    Vector3 playerDirection; // Direction of the player
    Vector3 startingPosition; // Starting position of the enemy
    public bool playerInRange;
    float angleToPlayer;
    float stoppingDistOrig;
    bool isShooting;
    bool isStun;
    bool destinationChosen; // Checks to see if the enemy has chosen a location to roam

    void Start()
    {
        target = gameManager.instance.player.transform;

        startingPosition = transform.position;
        stoppingDistOrig = agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        zombieAnim.SetFloat("Speed", agent.velocity.normalized.magnitude);

        if (agent.isActiveAndEnabled)
        {
            //animation for walk

            CanSeePlayer();

            //if (playerInRange && !CanSeePlayer()) // if player is in range and Zombie CANNOT see player
            //{
            //    StartCoroutine(Roam());
            //}
            //else if (agent.destination != gameManager.instance.player.transform.position) // if enemy destination is NOT the player
            //{
            //    StartCoroutine(Roam());
            //}
        }
    }

    IEnumerator Roam()
    {
        if (!destinationChosen && agent.remainingDistance < 0.05f) // Checks if destinationChosen if false && if the enemy is almost at its destination
        {
            destinationChosen = true;

            agent.stoppingDistance = 0; // enemy will go exactly where we want him (accounts for stopping distance);

            yield return new WaitForSeconds(roamTimer);

            destinationChosen = false;

            Vector3 randomPos = Random.insideUnitCircle * roamDist; // Gives us how far we want the Zombie to roam

            randomPos += startingPosition; // So Zombie does not get too far from starting position

            NavMeshHit hit;

            NavMesh.SamplePosition(randomPos, out hit, roamDist, 1);

            agent.SetDestination(hit.position); // Sends Zombie to random position
        }
    }

  bool CanSeePlayer()
  {
      agent.stoppingDistance = stoppingDistOrig;
  
      playerDirection = gameManager.instance.player.transform.position - headPos.position; // two positions subtracted from one another gives direction
  
      angleToPlayer = Vector3.Angle(new Vector3(playerDirection.x, 0, playerDirection.z), transform.forward);
  
      Debug.DrawRay(headPos.position, playerDirection); // draws a straight line from Zombie to player (for debugging)
      Debug.Log(angleToPlayer); // spits a number into console
  
      RaycastHit hit;
  
      if (Physics.Raycast(headPos.position, playerDirection, out hit))
      {
              agent.SetDestination(gameManager.instance.player.transform.position); // Zombie goes towards player
  
              float distanceToTarget = Vector3.Distance(target.position, transform.position);
  
              if (agent.remainingDistance <= agent.stoppingDistance)
              {
                  FacePlayer();
              }
              if (!isShooting && distanceToTarget <= agent.stoppingDistance)
              {
  
                  StartCoroutine(Shoot());
              }
  
              return true;
      }
        agent.stoppingDistance = 0; // If Zombie cannot see player, set stopping distance to 0

        return false;
  }


    IEnumerator Shoot()
    {
        isShooting = true;

        zombieAnim.SetTrigger("Attack");

        yield return new WaitForSeconds(shootRate);

        isShooting = false;
    }
    public void HandColOn()
    {
        handCollider1.enabled = true;
        handCollider2.enabled = true;
    }

    public void HandColOff()
    {
        handCollider1.enabled = false;
        handCollider2.enabled = false;
    }

    void OnTriggerEnter(Collider other) //When player is in range of Zombie
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
   
    void OnTriggerExit(Collider other) // When the player is outside range of Zombie
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void FacePlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * playerFaceSpeed);
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;

        HandColOff();

        if (HP <= 0)
        {
            StopAllCoroutines(); // Stop flash and shoot

            gameManager.instance.UpdateGameGoal(-1); // Zombie dies

            zombieAnim.SetBool("Dead", true);

            agent.enabled = false; // Stops the enemy from moving

            zombieAnim.ResetTrigger("Attack"); // zombie stops attacking when dead

            GetComponent<Animator>().enabled = false;

            GetComponent<CapsuleCollider>().enabled = false; // Disables dmg collider

            StartCoroutine(TimeToDelete());
        }
        else
        {
            agent.SetDestination(gameManager.instance.player.transform.position); // Zombie goes towards player after taking damage

            StartCoroutine(FlashColor()); // Flash indicator that Zombie takes damage
        }
    }

    IEnumerator FlashColor()
    {
        model.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        model.material.color = Color.white;
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

   public void getStunned()
    {

    }
}
