using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // Amount of damage this projectile will deal

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
        Destroy(gameObject); // Destroy the projectile upon collision
    }

    // Optional: Use this if your projectile's collider is set as a trigger
    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
        Destroy(gameObject); // Destroy the projectile upon collision
    }
}
