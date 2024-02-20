using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float rotationSpeed = 100.0f; // Adjusted for degrees per second
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    //States
    [SerializeField] private bool isGrounded = false;
    public bool isRuning = false;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        GroundedPlayer();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * vertical;

        Rotation(horizontal);

        Movement(move, this.speed);

        Jump();
    }

    void GroundedPlayer()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y <= 0)
        {
            velocity.y = gravityValue;
        }
        else
        {
            velocity.y += gravityValue * Time.deltaTime;
        }
    }

    void Movement(Vector3 move, float speed)
    {
        // Run Logic
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 20;
            isRuning = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 20;
            isRuning = false;
        }    
 
        controller.Move(move * speed * Time.deltaTime);
    }

    void Rotation(float horizontal)
    {
        // Rotate the player based on horizontal input
        Quaternion rotation = Quaternion.AngleAxis(horizontal * rotationSpeed * Time.deltaTime, Vector3.up);
        transform.rotation = transform.rotation * rotation;
    }

    void Jump()
    {
        // Jumping logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        controller.Move(velocity * Time.deltaTime);
    }
}

