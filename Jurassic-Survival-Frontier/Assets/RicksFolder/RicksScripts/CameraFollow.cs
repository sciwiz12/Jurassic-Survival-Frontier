using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // The player's transform
    public Vector3 offset; // Offset from the player
    public float smoothSpeed = 0.125f; // How smoothly the camera catches up to its target position
    //public float rotationSpeed = 5.0f; // Speed at which the camera rotates to match the player's rotation

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(player); // Always look at the player

        // Optional: Rotate the camera based on the player's rotation for more dynamic camera movement
        // This can be adjusted or removed based on specific game needs
        //Quaternion playerRotation = Quaternion.LookRotation(player.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
    }
}
