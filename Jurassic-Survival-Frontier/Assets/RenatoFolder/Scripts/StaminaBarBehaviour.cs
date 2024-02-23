using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class StaminaBarBehaviour : MonoBehaviour
{
    // Components
    private PlayerMovement playerMovement;

    // Interface
    [SerializeField] private Image StaminaRunBar;
    //[SerializeField] private Image HungryBar;
    [SerializeField] private float StaminaBarValue = 100;
    [SerializeField] private float MaxStamina;

    // Cost
    [SerializeField] private float RunCost = 5;
    

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.isRunning)
        {
            StaminaBarValue -= RunCost;
            
        }
    }
}
