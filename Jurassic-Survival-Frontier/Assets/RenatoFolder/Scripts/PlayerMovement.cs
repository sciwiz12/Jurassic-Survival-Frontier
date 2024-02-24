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
    Animator anim;

    // States
    float velocityState;
    float idleState = 0f;
    float walkState = 0.5f;
    float runState = 1f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        staminaManager = GetComponent<StaminaManager>();
        anim = GetComponent<Animator>();

        velocityState = idleState;
        anim.SetFloat("walk", velocityState);
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
        int multiplier = 1;
        string parameter = "velocity";

        // Idle
        if (move == Vector3.zero)
        {
            multiplier = 0;
            SetVelocityState(this.anim, parameter, velocityState, idleState);
        }

        // Walk
        if (move != Vector3.zero)
        {
            multiplier = 1;
            SetVelocityState(this.anim, parameter, velocityState, walkState);
        }

        // Run
        if (Input.GetKey(KeyCode.LeftShift) && move != Vector3.zero)
        {
            multiplier = 5;

            SetVelocityState(this.anim, parameter, velocityState, runState);
        }

        // Move the player
        controller.Move(move * speed * multiplier * Time.deltaTime);
    }

    void SetVelocityState(Animator anim, string parameter, float state, float newState)
    {
        state = newState;
        anim.SetFloat(parameter, state);
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
