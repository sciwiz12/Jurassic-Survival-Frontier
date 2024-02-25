using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
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
        if(!hasAte && timer > timerPassed)
        {
            playerMovement.CurrentHunger -= Time.deltaTime * modifier;
        }
        else if(hasAte) 
        {
            FeedMe();
            timer = 0;
        }
    }

    private float FeedMe(float feed = 15) 
    {
        playerMovement.CurrentHunger += feed;
        return playerMovement.CurrentHunger;
    }
}
