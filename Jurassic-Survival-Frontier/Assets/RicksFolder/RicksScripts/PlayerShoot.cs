using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform shootingPoint; // The point from which bullets are shot
    public GameObject projectilePrefab; // Your bullet prefab
    public float projectileSpeed = 20f; // Speed at which the projectile will be shot

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Default fire button (usually left mouse click or Ctrl)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed; // Shoot forward from the player's perspective
    }
}
