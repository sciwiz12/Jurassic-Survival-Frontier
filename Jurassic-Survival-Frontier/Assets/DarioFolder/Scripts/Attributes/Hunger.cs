using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public float timer;
    public float timerPassed = 5f;
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
        if(!playerMovement.hasAte && timer > timerPassed)
        {
            playerMovement.CurrentHunger -= Time.deltaTime * modifier;
        }
        else if(playerMovement.hasAte) 
        {
            FeedMe(15);
            timer = 0;
        }
    }

    public float FeedMe(float feed) 
    {
        playerMovement.CurrentHunger += feed;
        return playerMovement.CurrentHunger;
    }
}
