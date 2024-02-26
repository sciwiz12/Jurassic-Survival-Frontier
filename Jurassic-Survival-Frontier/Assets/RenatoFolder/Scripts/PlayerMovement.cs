using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 100f;
    private Vector3 move;
    private Quaternion rotation;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public bool isRunning;
    public bool hasAte;
    public bool hasDrank;

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
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveInput.magnitude > 0)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0; // Ensure the movement is only influenced on the XZ plane
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward) * Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Convert the moveInput vector from local space to world space
            move = transform.TransformDirection(moveInput.normalized);
        }
        else
        {
            move = Vector3.zero;
        }

        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Stamina management code remains unchanged
        Running();
    }

    // Create a method for running. 
    private void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift) && staminaManager.CurrentStamina > 0)
        {
            isRunning = true;
            playerSpeed = 8.0f;
            staminaManager.StaminaDecrease(0.001f);
        }
        else
        {
            isRunning = false;
            playerSpeed = 5.0f;
        }
    }

}
