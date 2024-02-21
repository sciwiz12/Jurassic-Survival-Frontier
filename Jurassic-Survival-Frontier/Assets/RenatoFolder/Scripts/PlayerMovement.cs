using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public bool isRuning;

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
            staminaManager.StaminaReduceAction(staminaManager.staminaCost = 10);
            staminaManager.isActionPerformed = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            staminaManager.StaminaReduceAction(staminaManager.staminaCost = 5);     
            staminaManager.isActionPerformed = false;
        }
        #endregion
    }
}