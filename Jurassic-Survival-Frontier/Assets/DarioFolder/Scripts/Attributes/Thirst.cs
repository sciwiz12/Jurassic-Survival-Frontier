using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thirst : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public float timer;
    public float timerPassed = 5f;
    public Image thirstBar;
    public float modifier = 0.5f;

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.CurrentThirst = playerMovement.maxThirst;
    }

    void Update()
    {
        thirstBar.fillAmount = playerMovement.CurrentThirst / playerMovement.maxThirst;
        timer += Time.deltaTime * modifier;
        if (!playerMovement.hasDrank && timer > timerPassed)
        {
            playerMovement.CurrentThirst -= Time.deltaTime * modifier;
        }
        else if (playerMovement.hasDrank)
        {
            DrinkUp();
            timer = 0;
        }
    }

    private float DrinkUp(float watering = 15)
    {
        playerMovement.CurrentThirst += watering;
        return playerMovement.CurrentThirst;
    }
}