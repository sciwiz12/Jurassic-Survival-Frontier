using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTheShoulderCamera : MonoBehaviour
{
    public Transform cameraTransform; // Assign this in the inspector
    public Vector3 offset; // The desired offset from the player
    public float smoothSpeed = 0.125f; // Adjust for smoother camera movement

    void Start()
    {
        // If not set in the inspector, find the camera as a child of this GameObject
        if (cameraTransform == null)
        {
            cameraTransform = GetComponentInChildren<Camera>().transform;
        }

        // Calculate offset if you want it to be dynamic or just set it manually in the inspector
        offset = cameraTransform.position - transform.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(cameraTransform.position, desiredPosition, smoothSpeed);
        cameraTransform.position = smoothedPosition;

        // Optionally, make the camera always look at the player
        cameraTransform.LookAt(transform.position);
    }
}
