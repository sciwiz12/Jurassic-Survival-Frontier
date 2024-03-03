using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject pickupItem;

    void Start()
    {
        currentHealth = maxHealth; // Initialize enemy health
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health by the damage amount

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Instantiate pickup item upon death here
        Instantiate(pickupItem, transform.position, transform.rotation);
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
