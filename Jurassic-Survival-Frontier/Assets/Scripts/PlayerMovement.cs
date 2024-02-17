using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float rotationSpeed = 100.0f; // Adjusted for degrees per second
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * vertical;

        // Rotate the player based on horizontal input
        Quaternion rotation = Quaternion.AngleAxis(horizontal * rotationSpeed * Time.deltaTime, Vector3.up);
        transform.rotation = transform.rotation * rotation;

        // Move the player
        controller.Move(move * playerSpeed * Time.deltaTime);

        // Jumping logic
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}

