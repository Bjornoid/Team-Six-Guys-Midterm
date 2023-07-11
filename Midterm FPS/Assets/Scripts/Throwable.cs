using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [Header("----- Throw Force -----")]
    [SerializeField] float throwForce = 10f;
    [SerializeField] float maxForce = 20f;

    [Header("----- Throw Settings -----")]
    [SerializeField] Transform throwPos;
    [SerializeField] Vector3 throwDir = new Vector3(0, 1, 0);
    [SerializeField] LineRenderer trajectoryLine;


    Camera mainCamera;
    GameObject objToThrow;

    int stunGrenadeCurr;
    int stunGrenadeMax = 5;

    bool isCharging = false;
    float chargeTime = 0f;
    public int throwTimes;
    public int thrown;

    void Start()
    {
        stunGrenadeCurr = stunGrenadeMax;
  
        thrown = 0;
        throwTimes = 5;
        objToThrow = gameManager.instance.stunGrenade;
        mainCamera = Camera.main;
    }

    public void setObjToThrow(GameObject obj)
    {
        objToThrow = obj;
    }

    void Update()
    {
        if (objToThrow != null && thrown < throwTimes)
        {
            if (Input.GetButtonDown("Throw"))
            {
                //start throwing
                startThrowing();
            }
            if (isCharging)
            {
                //charge throw
                chargeThrow();
            }
            if (Input.GetButtonUp("Throw"))
            {
                //release throw
                releaseThrow();
                thrown++;
            }
        }
    }

    void startThrowing()
    {
        //pull pin sound

        isCharging = true;
        chargeTime = 0f;

        //Trajectory line
        trajectoryLine.enabled = true;
    }

    void chargeThrow()
    {
        chargeTime += Time.deltaTime;

        //trajectory line velocity
        Vector3 throwVelocity = (mainCamera.transform.forward + throwDir).normalized * Mathf.Min(chargeTime * throwForce, maxForce);
        showTrajectory(throwPos.position + throwPos.forward, throwVelocity);
    }

    void releaseThrow()
    {
        throwGrenade(Mathf.Min(chargeTime * throwForce, maxForce));
        isCharging = false;

        //hide line
        trajectoryLine.enabled = false;
    }

    void throwGrenade(float force)
    {
        Vector3 spawnPos = throwPos.position + mainCamera.transform.forward;

        GameObject obj = Instantiate(objToThrow, spawnPos, mainCamera.transform.rotation);
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        Vector3 finalThrowDir = (mainCamera.transform.forward + throwDir).normalized;
        rb.AddForce(finalThrowDir * force, ForceMode.VelocityChange);

        //throw sound
    }

    void showTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100];
        trajectoryLine.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + speed * time + 0.5f * Physics.gravity * time * time;
        }
        trajectoryLine.SetPositions(points);
    }
}
