using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // If player enters the trigger, get the player's health from the playerMovement script and reduce it by 10 each time interval.
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("RaptorAttack: OnTriggerStay");
        if (other.tag == "Player")
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.TakeDamage(0.5f);
        }
    }
}
