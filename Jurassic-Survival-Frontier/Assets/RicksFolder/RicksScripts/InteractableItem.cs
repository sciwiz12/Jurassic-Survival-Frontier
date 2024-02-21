using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public Item item; // Assign this in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player is triggering the interaction
        {
            Inventory playerInventory = other.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(item);
                Destroy(gameObject); // Optionally destroy the item after pickup
            }
        }
    }
}
