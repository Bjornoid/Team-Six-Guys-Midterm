using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [Header("----- Camera Settings -----")]
    [SerializeField] int sensitivity;
    [SerializeField] int lockVerMin;
    [SerializeField] int lockVerMax;
    [SerializeField] bool invertY; 

    float xRotation; // Rotation of X Axis

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; // Makes cursor not visable

        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor in the window
    }

    // Update is called once per frame
    void Update()
    {
        // Get Input
       float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity; // Mouse when moving on Y axis (up and down)

       float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity; // Mouse when moving on X axis (left and right)

        if (invertY)
        {
            xRotation += mouseY;
        }
        else
        {
            xRotation -= mouseY;
        }

        //Clamp the Camera Rotation on the X-Axis (for strafing)

        xRotation = Mathf.Clamp(xRotation, lockVerMin, lockVerMax);

        // Rotate the Camera on X-Axis

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // Rotate the Player on Y-Axis

        transform.parent.Rotate(Vector3.up * mouseX);

    }
}
