using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thirst : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public float timer;
    public float timerPassed = 5f;
    public bool hasAte;
    public Image hungerBar;
    public float modifier = 0.5f;

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.CurrentHunger = playerMovement.maxHunger;
    }

    void Update()
    {
        hungerBar.fillAmount = playerMovement.CurrentHunger / playerMovement.maxHunger;
        timer += Time.deltaTime * modifier;
        if (!hasAte && timer > timerPassed)
        {
            playerMovement.CurrentHunger -= Time.deltaTime * modifier;
        }
        else if (hasAte)
        {
            DrinkUp();
            timer = 0;
        }
    }

    private float DrinkUp(float watering = 15)
    {
        playerMovement.CurrentHunger += watering;
        return playerMovement.CurrentHunger;
    }
}