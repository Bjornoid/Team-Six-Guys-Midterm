using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls
    : MonoBehaviour
{
    [Header("----- Player Settings -----")]
    [SerializeField] CharacterController controller;
    [Range(3, 100)][SerializeField] float playerSpeed; // player speed
    [Range(1, 100)][SerializeField] float jumpHeight; // jump height for player
    [Range(-10, 100)][SerializeField] float gravityValue; // gravity value for player
    [SerializeField] int jumpMax; // max amount of jump a player can have

    [Header("----- Gun Settings -----")]
    [Range(0.1f, 3)][SerializeField] float shootRate;
    [Range(1, 10)][SerializeField] int shootDamage;
    [Range(25, 1000)][SerializeField] int shootDist;

    private Vector3 playerVelocity; // gets player velocty
    private Vector3 move; // movement for fps 
    private int jumpTimes; // the amount of time the player has jumped
    private bool groundedPlayer; // checks if player is on ground
    bool isShooting; // Checks if you are shooting

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();

        if (Input.GetButton("Shoot") && !isShooting)
        {
            StartCoroutine(shoot()); // start shooting
        }
    }

    void movement()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0) // if we are on the ground
        {
            playerVelocity.y = 0f;

            jumpTimes = 0;
        }

        move = (transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"));

        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player
        if (Input.GetButtonDown("Jump") && jumpTimes < jumpMax)
        {
            jumpTimes++;

            playerVelocity.y = jumpHeight;
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
        }

        yield return new WaitForSeconds(shootRate);

        isShooting = false;
    }
}
