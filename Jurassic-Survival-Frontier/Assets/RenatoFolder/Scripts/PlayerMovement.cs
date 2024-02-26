using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioData bark;
    [SerializeField] private float playerSpeed = 2.0f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float jumpHeight   = 1.0f;
    private float gravityValue = -9.81f;
    private bool groundedPlayer;
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
        #region Movement
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        #endregion

        #region Stamina
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("premuto Q");
            AudioManager.Instance.PlayEffect(bark);
            staminaManager.StaminaDecrease(staminaManager.StaminaCost = 10);
            staminaManager.isActionPerformed = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Premuto E");
            staminaManager.StaminaDecrease(staminaManager.StaminaCost = 5);     
            staminaManager.isActionPerformed = false;
        }
        #endregion
    }
}
