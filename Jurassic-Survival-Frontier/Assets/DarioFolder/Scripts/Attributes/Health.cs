using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthImage;
    private PlayerMovement playerMovement;
    public float bulletDamage = 5;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.CurrentHealth = playerMovement.maxHealth;
    }

    private void Update()
    {
        healthImage.fillAmount = playerMovement.CurrentHealth / playerMovement.maxHealth;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TakeDamage(bulletDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(bulletDamage);
        }
    }

    private float TakeDamage(float damage)
    {
        playerMovement.CurrentHealth -= damage;
        return playerMovement.CurrentHealth;
    }
}
