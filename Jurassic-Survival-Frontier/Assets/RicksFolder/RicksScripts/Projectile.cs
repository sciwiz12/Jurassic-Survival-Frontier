using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // Amount of damage this projectile will deal
    private int timeToDestroy = 1000;

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
        if (enemyHealth != null && enemyHealth.getCurrentHealth() <= 0)
        {
            Destroy(gameObject); // Destroy the projectile upon collision
        }
    }

    // Optional: Use this if your projectile's collider is set as a trigger
    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
        if (enemyHealth != null && enemyHealth.getCurrentHealth() <= 0)
        {
            Destroy(gameObject); // Destroy the projectile upon collision
        }
    }

    void Update()
    {
        timeToDestroy--;
        Debug.Log(timeToDestroy);
        if(timeToDestroy <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
