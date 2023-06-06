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
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("----- Gun Settings -----")]
    [Range(0.1f, 3)][SerializeField] float shootRate;
    [Range(1, 10)][SerializeField] int shootDamage;
    [Range(25, 1000)][SerializeField] int shootDist;

    private float moveSpeed;
    private Vector3 playerVelocity; // gets player velocty
    private Vector3 move; // movement for fps 
    private int jumpTimes; // the amount of time the player has jumped
    private bool groundedPlayer; // checks if player is on ground
    bool isShooting; // Checks if you are shooting

    public MovementState movementState;

    public enum MovementState
    {
        walking, 
        sprinting,
        jumping
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;
    }

    private void stateHandler()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && Input.GetButton("LShift"))
        {
            movementState = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (groundedPlayer)
        {
            movementState = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else 
        {
            movementState = MovementState.jumping;
        }

    }

    void Start()
    {
        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        stateHandler();
        movement();

        if (Input.GetButton("Shoot") && !isShooting)
        {
            StartCoroutine(shoot()); // start shooting
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
        if (Input.GetButtonDown("LCTRL"))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            
        }
        if (Input.GetButtonUp("LCTRL"))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
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
}
