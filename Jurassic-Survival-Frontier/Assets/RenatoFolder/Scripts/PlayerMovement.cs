using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;
    public bool isRunning;
    public bool hasAte;
    public bool hasDrank;
    Animator anim;

    #region STATES
    float velocityState;
    float idleState = 0f;
    float walkState = 0.5f;
    float runState = 1f;
    #endregion

    #region ATTRIBUTES
    public float maxHunger = 100f;
    private float currentHunger;
    public float CurrentHunger
    {
        get { return currentHunger; }
        set { currentHunger = value; }
    }

    public float maxThirst = 100f;
    private float currentThirst;
    public float CurrentThirst
    {
        get { return currentThirst; }
        set { currentThirst = value; }
    }

    public float maxHealth = 100f;
    private float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    #endregion

    private StaminaManager staminaManager;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        staminaManager = FindObjectOfType<StaminaManager>();
        anim = GetComponent<Animator>();

        velocityState = idleState;
        anim.SetFloat("walk", velocityState);
    }

    void Update()
    {
        HandleMovementAndRotation();
        HandleJump();
        ApplyGravity();
    }

    private void HandleMovementAndRotation()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (moveInput.magnitude > 0)
        {
            // Calculate the direction to face based on camera's forward and right vectors
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 direction = forward * moveInput.z + right * moveInput.x;

            // Only apply rotation if moving forward or if there's significant horizontal input
            if (moveInput.z > 0 || Mathf.Abs(moveInput.x) > 0.1f)
            {
                RotateTowards(direction);
            }

            // Apply movement in the direction the character is currently facing
            // This allows backward movement without changing the facing direction
            controller.Move(transform.forward * moveInput.z * playerSpeed * Time.deltaTime + transform.right * moveInput.x * playerSpeed * Time.deltaTime);

            // Set velocity state based on movement speed
            if (isRunning)
            {
                SetVelocityState(anim, "velocity", velocityState, runState);
            }
            else if (moveInput.magnitude < .5f)
            {
                SetVelocityState(anim, "velocity", velocityState, idleState); 
            }
            else
            {
                SetVelocityState(anim, "velocity", velocityState, walkState);
            }
        }

        isRunning = Input.GetKey(KeyCode.LeftShift) && staminaManager.CurrentStamina > 0;
        playerSpeed = isRunning ? 8.0f : 5.0f;
        if (isRunning) staminaManager.StaminaDecrease(0.001f);
    }

    private void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void HandleJump()
    {
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            anim.SetBool("jump", true);
        }
    }

    private void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
            anim.SetBool("jump", false);
        }
        /*else if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }*/
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void SetVelocityState(Animator anim, string parameter, float state, float newState)
    {
        state = newState;
        anim.SetFloat(parameter, state);
    }
}