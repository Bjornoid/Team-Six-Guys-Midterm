using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControls
    : MonoBehaviour, IDamage, IAmmo
{
    [Header("----- Player Settings -----")]
    [SerializeField] CharacterController controller;
    [SerializeField] AudioSource aud;
    [Range(3, 100)][SerializeField] float walkSpeed; // player walk speed
    [Range(3, 100)][SerializeField] float sprintSpeed; // player sprint speed
    [Range(1, 100)][SerializeField] float jumpHeight; // jump height for player
    [Range(-10, 100)][SerializeField] float gravityValue; // gravity value for player
    [SerializeField] int jumpMax; // max amount of jump a player can have
    [SerializeField] int HP;
    [SerializeField] float jetpackDuration;
    [SerializeField] GameObject medPack;
    public float crouchHeight;
    public bool hasJetpack;

    [Header("----- Gun Settings -----")]
    [Range(0.1f, 3)][SerializeField] float shootRate;
    [Range(1, 10)][SerializeField] int shootDamage;
    [Range(25, 1000)][SerializeField] int shootDist;
    [SerializeField] GameObject pistolModel;
    [SerializeField] GameObject akModel;
    [SerializeField] GameObject shottyModel;
    [SerializeField] GameObject voidModel;
    [SerializeField] GameObject waveModel;
    [SerializeField] ParticleSystem waveBlast;
    [SerializeField] Transform wavePos;
    [SerializeField] List<GunStats> gunList = new List<GunStats>();
    [SerializeField] GunStats startingPistol;

    [Header("----- Audio -----")]
    [SerializeField] AudioClip[] jumpSounds;
    [SerializeField] [Range(0, 1)] float jumpVol;
    [SerializeField] AudioClip[] stepSounds;
    [SerializeField][Range(0, 1)] float stepVol;
    [SerializeField] AudioClip[] damageSounds;
    [SerializeField][Range(0, 1)] float damageVol;
    [SerializeField] AudioClip[] jetpackSounds;
    [SerializeField][Range(0, 1)] float jetpackVol;
    [SerializeField] AudioClip[] medkitSounds;
    [SerializeField][Range(0, 1)] float medkitVol;
    [SerializeField] AudioClip[] ammoPickupSounds;
    [SerializeField][Range(0, 1)] float ammoVol;
    [SerializeField] AudioClip[] keySounds;
    [SerializeField][Range(0, 1)] float keyVol;

    [Header("----- Bomb -----")]
    [SerializeField][Range(0, 1000)] float throwForce;
    [SerializeField] GameObject projectile;

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
    bool isReloading;
    bool stepsIsPlaying; 
    public bool ammoPickedUp;
    int selectedGun;
    GameObject gunModel;
    public bool hasWonderWeapon;
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
        gunPickup(startingPistol);
        gunList[0].magAmmoCurr = gunList[0].magAmmoMax;
        gunList[0].reserveAmmoCurr = gunList[0].reserveAmmoMax;
    }

    // Update is called once per frame
    void Update()
    {
        stateHandler();
        if (gameManager.instance.activeMenu == null)
        {
            movement();
            crouch();

            if (gunList.Count > 0)
            {
                changeGun();
                if (Input.GetButtonDown("Reload") && !isReloading && gunList[selectedGun].magAmmoCurr < gunList[selectedGun].magAmmoMax && gunList[selectedGun].reserveAmmoCurr > 0)
                {
                    StartCoroutine(reload());
                }
                if (Input.GetButton("Shoot") && !isShooting && !isReloading)
                {
                    StartCoroutine(shoot()); // start shooting
                }
            }
        }
        if (Input.GetButtonDown("F"))
        {
            Throw();
        }
    }

    public void takeDamage(int dmg)
    {
        aud.PlayOneShot(damageSounds[UnityEngine.Random.Range(0, damageSounds.Length)], damageVol);
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

        if (gunList.Count > 0)
        {
            gameManager.instance.ammoCurText.text = gunList[selectedGun].magAmmoCurr.ToString("F0");
            gameManager.instance.ammoMaxText.text = gunList[selectedGun].reserveAmmoCurr.ToString("F0");
        }
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
            if (playerVelocity.y > 0)
                playerVelocity.y = 0f;

            if (jetpackTime > 0)
            {
                jetpackTime -= Time.deltaTime / 2;
                gameManager.instance.fuelBar.fillAmount = 1 - jetpackTime/ jetpackDuration;
                canJetpack = true;
            }


            movementState = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (groundedPlayer)
        {
            if (playerVelocity.y > 0)
                playerVelocity.y = 0f;

            if (jetpackTime > 0)
            {
                jetpackTime -= Time.deltaTime / 2;
                gameManager.instance.fuelBar.fillAmount = 1 - jetpackTime / jetpackDuration;
                canJetpack = true;
            }
            

            movementState = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else if (!groundedPlayer && hasJetpack && canJetpack && Input.GetButton("Jump"))
        {
            jetpackTime += Time.deltaTime;
            gameManager.instance.fuelBar.fillAmount = 1 - jetpackTime / jetpackDuration;
            if (jetpackTime >= jetpackDuration)
            {
                canJetpack = false;
                
            }
            movementState = MovementState.jetpacking;
            if (playerVelocity.y < 0)
                playerVelocity.y = 3f;

            playerVelocity.y += .1f;

        }
        else 
        {
            if (playerVelocity.y > 0)
            {
                if (playerVelocity.y < .2f)
                    playerVelocity.y = 0;
                else
                    playerVelocity.y -= .2f;

            }

            movementState = MovementState.jumping;
        }
    

    }

    void movement()
    {
        if (groundedPlayer) // if we are on the ground
        {
            if (!stepsIsPlaying && move.normalized.magnitude > 0.5f)
            {
                StartCoroutine(playSteps());
            }

            jumpTimes = 0;
            if (playerVelocity.y < 0)
                playerVelocity.y = 0f;
        }

        move = (transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"));

        controller.Move(move * Time.deltaTime * moveSpeed);

        // Changes the height position of the player
        if (Input.GetButtonDown("Jump") && jumpTimes < jumpMax)
        {
            if (!hasJetpack)
                aud.PlayOneShot(jumpSounds[UnityEngine.Random.Range(0, jumpSounds.Length)], jumpVol);
            else
            {
                aud.PlayOneShot(jetpackSounds[0], jetpackVol);
            }

            jumpTimes++;
            playerVelocity.y = jumpHeight;
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        
    }

    IEnumerator playSteps()
    {
        stepsIsPlaying = true;

        aud.PlayOneShot(stepSounds[UnityEngine.Random.Range(0, stepSounds.Length)], stepVol);

        if (movementState == MovementState.walking)
            yield return new WaitForSeconds(.5f);
        else if (movementState == MovementState.sprinting)
            yield return new WaitForSeconds(.3f);

        stepsIsPlaying = false;
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
        if (gunList[selectedGun].magAmmoCurr > 0)
        {
            isShooting = true;
            aud.PlayOneShot(gunList[selectedGun].shotSound, gunList[selectedGun].soundVol);
            gunList[selectedGun].magAmmoCurr--;
            UpdatePlayerUI();

            if (!hasWonderWeapon)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
                {
                    IDamage damageable = hit.collider.GetComponent<IDamage>();

                    if (damageable != null)
                    {
                        damageable.takeDamage(shootDamage);
                    }
                    else if (hit.collider.gameObject.CompareTag("Target"))
                    {
                        Destroy(hit.collider.gameObject);
                        gameManager.instance.updateTargetCount(-1);
                    }
                    Instantiate(gunList[selectedGun].hitEffect, hit.point, Quaternion.identity);
                }
            }
            else
            {
                if (gunList[selectedGun].name.Equals("Wave Blast"))
                {
                    Instantiate(waveBlast, wavePos.position, transform.rotation);
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject enemy in enemies)
                    {
                        Vector3 enemyDir = enemy.transform.position - transform.position;
                        float angleToEnemy = Vector3.Angle(new Vector3(enemyDir.x, 0, enemyDir.z), transform.forward);
                        if (Vector3.Distance(enemy.transform.position, transform.position) < shootDist && angleToEnemy <= 100)
                        {
                            

                            enemy.GetComponent<IDamage>().takeDamage(shootDamage);
                        }
                    }
                }
            }

            yield return new WaitForSeconds(shootRate);

            isShooting = false;
        } 
    }

    public void SpawnPlayer()
    {
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawnPosition.transform.position;
        controller.enabled = true;
        HP = playerHPOrig;
        UpdatePlayerUI();
    }
  
    public void setGunModel(string name)
    {
        if (name.Equals("Starting Pistol"))
        {
            hasWonderWeapon = false;

            pistolModel.SetActive(true);
            gunModel = pistolModel;
            akModel.SetActive(false);
            shottyModel.SetActive(false);
            voidModel.SetActive(false);
            waveModel.SetActive(false);
        }
        else if (name.Equals("Ak"))
        {
            hasWonderWeapon = false;

            akModel.SetActive(true);
            gunModel = akModel;
            pistolModel.SetActive(false);
            shottyModel.SetActive(false);
            voidModel.SetActive(false);
            waveModel.SetActive(false);
        }
        else if (name.Equals("Shotty"))
        {
            hasWonderWeapon = false;

            shottyModel.SetActive(true);
            gunModel = shottyModel;
            akModel.SetActive(false);
            pistolModel.SetActive(false);
            voidModel.SetActive(false);
            waveModel.SetActive(false);
        }   
        else if (name.Equals("VoidGun"))
        {
            hasWonderWeapon = true;

            voidModel.SetActive(true);
            gunModel = voidModel;
            akModel.SetActive(false);
            pistolModel.SetActive(false);
            shottyModel.SetActive(false);
            waveModel.SetActive(false);
        }
        else if (name.Equals("Wave Blast"))
        {
            hasWonderWeapon = true;

            waveModel.SetActive(true);
            gunModel = waveModel;
            akModel.SetActive(false);
            pistolModel.SetActive(false);
            shottyModel.SetActive(false);
            voidModel.SetActive (false);
        }
    }

    public void gunPickup(GunStats gunStat)
    {
        gunList.Add(gunStat);
        selectedGun = gunList.Count - 1;
        shootDamage = gunStat.shootDmg;
        shootDist = gunStat.shootDist;
        shootRate = gunStat.shootRate;
        setGunModel(gunStat.name);
        setGunModel(gunStat.name);
        gunModel.GetComponent<MeshFilter>().mesh = gunStat.model.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().material = gunStat.model.GetComponent<MeshRenderer>().sharedMaterial;

        MeshFilter[] filters = gunStat.model.GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] rndrs = gunStat.model.GetComponentsInChildren<MeshRenderer>();

        MeshFilter[] myfilters = gunModel.GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] myRndrs = gunModel.GetComponentsInChildren<MeshRenderer>();

        for(int i = 0; i < filters.Length; i++)
        {
            myfilters[i].mesh = filters[i].sharedMesh;
            myRndrs[i].material = rndrs[i].sharedMaterial;
        }
        for(int i = filters.Length; i < myfilters.Length; i++)
        {
            myfilters[i].mesh = null;
            myRndrs[i].material = null;
        }
        UpdatePlayerUI();
    }

    void changeGun()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedGun < gunList.Count - 1)
        {
            selectedGun++;
            setGunModel(gunList[selectedGun].name);
            changeGunStats();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedGun > 0)
        {
            selectedGun--;
            setGunModel(gunList[selectedGun].name);
            changeGunStats();
        }
        UpdatePlayerUI();
    }

    void changeGunStats()
    {
        shootDamage = gunList[selectedGun].shootDmg;
        shootDist = gunList[selectedGun].shootDist;
        shootRate = gunList[selectedGun].shootRate;

        gunModel.GetComponent<MeshFilter>().mesh = gunList[selectedGun].model.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().material = gunList[selectedGun].model.GetComponent<MeshRenderer>().sharedMaterial;

        MeshFilter[] filters = gunList[selectedGun].model.GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] rndrs = gunList[selectedGun].model.GetComponentsInChildren<MeshRenderer>();

        MeshFilter[] myfilters = gunModel.GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] myRndrs = gunModel.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < filters.Length; i++)
        {
            myfilters[i].mesh = filters[i].sharedMesh;
            myRndrs[i].material = rndrs[i].sharedMaterial;
        }
        for (int i = filters.Length; i < myfilters.Length; i++)
        {
            myfilters[i].mesh = null;
            myRndrs[i].material = null;
        }
    }

    IEnumerator reload()
    {
       
        isReloading = true;
        aud.PlayOneShot(gunList[selectedGun].reloadSound, .7f);

        yield return new WaitForSeconds(gunList[selectedGun].reloadTime);

        int ammoMissing = gunList[selectedGun].magAmmoMax - gunList[selectedGun].magAmmoCurr;
        if (ammoMissing < gunList[selectedGun].reserveAmmoCurr)
        {
            gunList[selectedGun].reserveAmmoCurr -= ammoMissing;
            gunList[selectedGun].magAmmoCurr += ammoMissing;
        }
        else
        {
            gunList[selectedGun].magAmmoCurr += gunList[selectedGun].reserveAmmoCurr;
            gunList[selectedGun].reserveAmmoCurr = 0;
        }

        
        isReloading = false;
        UpdatePlayerUI();
    }

    public void pickupAmmo()
    {
        ammoPickedUp = true;
        aud.PlayOneShot(ammoPickupSounds[0], ammoVol);
        foreach(GunStats gun in gunList)
        {
            gun.magAmmoCurr = gun.magAmmoMax;
            gun.reserveAmmoCurr = gun.reserveAmmoMax;
        }
        UpdatePlayerUI();
    }
    public void medKit()
    {
        if(HP != playerHPOrig)
        {
            aud.PlayOneShot(medkitSounds[0], medkitVol);
            Destroy(medPack);
            HP = playerHPOrig;
        }
    }

    public void pickupKey()
    {
        aud.PlayOneShot(keySounds[0], keyVol);
    }

    public void pickupFuel()
    {
        jetpackTime = 0;
        gameManager.instance.fuelBar.fillAmount = 1;
        canJetpack = true;
    }

    void Throw()
    {
        GameObject bomb = Instantiate(projectile, transform.position, transform.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
