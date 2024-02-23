using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    public float rotationSpeed = 5.0f; // Speed of rotation

    void Update()
    {
        // Get input from mouse or joystick
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        // Rotate the pivot GameObject around the player's position
        // Adjust these lines based on your desired axis of rotation
        transform.RotateAround(transform.position, Vector3.up, horizontalInput * rotationSpeed);
        transform.RotateAround(transform.position, transform.right, -verticalInput * rotationSpeed);
    }
}
