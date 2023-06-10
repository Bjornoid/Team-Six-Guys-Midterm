using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls
    : MonoBehaviour, IDamage
{
    [Header("----- Player Settings -----")]
    [SerializeField] CharacterController controller;
    [Range(3, 100)][SerializeField] float walkSpeed; // player walk speed
    [Range(3, 100)][SerializeField] float sprintSpeed; // player sprint speed
    [Range(1, 100)][SerializeField] float jumpHeight; // jump height for player
    [Range(-10, 100)][SerializeField] float gravityValue; // gravity value for player
    [SerializeField] int jumpMax; // max amount of jump a player can have
    [SerializeField] int HP;
    [SerializeField] float jetpackDuration;
    public float crouchHeight;
    public float jetpackRecharge;
    public bool hasJetpack;

    [Header("----- Gun Settings -----")]
    [Range(0.1f, 3)][SerializeField] float shootRate;
    [Range(1, 10)][SerializeField] int shootDamage;
    [Range(25, 1000)][SerializeField] int shootDist;

    

    private float playerHeight;
    private float startingGravity;
    private float jetpackTime;
    private bool canJetpack;
    private float moveSpeed;
    private Vector3 playerVelocity; // gets player velocty
    private Vector3 move; // movement for fps 
    private int jumpTimes; // the amount of time the player has jumped
    int playerHPOrig; // Original HP of player
    private bool groundedPlayer; // checks if player is on ground
    bool isShooting; // Checks if you are shooting

    public MovementState movementState;

    public enum MovementState
    {
        walking, 
        sprinting,
        jumping,
        jetpacking
    }
    void Start()
    {
        playerHPOrig = HP; // Resets player's HP
        controller.minMoveDistance = 0;
        playerHeight = controller.height;
        startingGravity = gravityValue;
        hasJetpack = false;
        canJetpack = true;
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        stateHandler();
        movement();
        crouch();

        if (Input.GetButton("Shoot") && !isShooting)
        {
            StartCoroutine(shoot()); // start shooting
        }
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;
        UpdatePlayerUI();

        StartCoroutine(PlayerFlashDamage());

        if(HP<=0)
        {
            gameManager.instance.YouLose();

            gameManager.instance.playerScript.SpawnPlayer();
        }
    }

    public void UpdatePlayerUI()
    {
        gameManager.instance.playerHPBar.fillAmount = (float)HP / playerHPOrig; // Divide Curr by Original to get player HP
    }

    IEnumerator PlayerFlashDamage()
    {
        gameManager.instance.playerFlashUI.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        gameManager.instance.playerFlashUI.SetActive(false);
    }

    private void stateHandler()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && Input.GetButton("LShift"))
        {
            if (gravityValue != startingGravity )
                gravityValue = startingGravity;
            if (hasJetpack && !canJetpack)
            {
                jetpackTime += Time.deltaTime;
                if (jetpackTime >= jetpackRecharge)
                {
                    canJetpack = true;
                    jetpackTime = 0;
                }
            }
            movementState = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (groundedPlayer)
        {
            if (gravityValue != startingGravity)
                gravityValue = startingGravity;
            if (hasJetpack && !canJetpack)
            {
                jetpackTime += Time.deltaTime;
                if (jetpackTime >= jetpackRecharge)
                {
                    canJetpack = true;
                    jetpackTime = 0;
                }
            }
            movementState = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else if (!groundedPlayer && hasJetpack && canJetpack && Input.GetButton("Jump"))
        {
            jetpackTime += Time.deltaTime;
            if (jetpackTime >= jetpackDuration)
            {
                canJetpack = false;
                jetpackTime = 0;
            }
            movementState = MovementState.jetpacking;
            gravityValue = -10;
        }
        else 
        {
            if (gravityValue != startingGravity)
                gravityValue = startingGravity;
            if (hasJetpack && !canJetpack)
            {
                jetpackTime += Time.deltaTime;
                if (jetpackTime >= jetpackRecharge)
                {
                    canJetpack = true;
                    jetpackTime = 0;
                }
            }
            movementState = MovementState.jumping;
        }
    

    }

    void movement()
    {
        if (groundedPlayer && playerVelocity.y < 0) // if we are on the ground
        {
            playerVelocity.y = 0f;

            jumpTimes = 0;
        }

        move = (transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"));

        controller.Move(move * Time.deltaTime * moveSpeed);

        // Changes the height position of the player
        if (Input.GetButtonDown("Jump") && jumpTimes < jumpMax)
        {
            jumpTimes++;

            playerVelocity.y = jumpHeight;
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void crouch()
    {
        if (Input.GetButtonDown("LCTRL"))
        {
            controller.height = crouchHeight;
        }
        else if (Input.GetButtonUp("LCTRL"))
        {
            controller.height = playerHeight;
        }
    }

    IEnumerator shoot()
    {
        isShooting = true;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
        {
            IDamage damageable = hit.collider.GetComponent<IDamage>();

            if (damageable != null)
            {
                damageable.takeDamage(shootDamage);
            }
            else if(hit.collider.gameObject.CompareTag("Target"))
            {
                Destroy(hit.collider.gameObject);
                gameManager.instance.updateTargetCount(-1);
            }
        }

        yield return new WaitForSeconds(shootRate);

        isShooting = false;
    }

    public void SpawnPlayer()
    {
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawnPosition.transform.position;
        controller.enabled = true;
        HP = playerHPOrig;
        UpdatePlayerUI();
    }
  
}
