using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float rotationSpeed = 100.0f; // Adjusted for degrees per second
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    public bool isRunning;
    private StaminaManager staminaManager;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        staminaManager = GetComponent<StaminaManager>();
        //staminaManager.MaxStamina = 100;
        //Debug.Log(staminaManager.MaxStamina);
    }

    void Update()
    {
        GroundedPlayer();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * vertical;

        Rotation(horizontal);

        Movement(move);

        Stamina();

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

    void Movement(Vector3 move)
    {
        int multiplier;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            multiplier = 5;
        }
        else
        {
            multiplier = 1;
        }
        // Move the player
        controller.Move(move * speed * multiplier * Time.deltaTime);
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
            Debug.Log("Jump");
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void Stamina()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            staminaManager.StaminaReduceAction(staminaManager.staminaCost = 10);
            staminaManager.isActionPerformed = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            staminaManager.StaminaReduceAction(staminaManager.staminaCost = 5);
            staminaManager.isActionPerformed = false;
        }
    }
}
