using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Transform player;
    public float fireRate = 2f;
    public float projectileSpeed = 10f;
    public int projectileDamage = 20;

    private float nextFireTime = 0f;

    void FixedUpdate()
    {
        if (Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null || player == null)
        {
            //Debug.LogError("Missing references in BossAttack script.");
            return;
        }

        Vector3 direction = (player.position - firePoint.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(player, projectileSpeed, projectileDamage);
        }
        else
        {
            //Debug.LogError("Projectile script missing on projectile prefab.");
        }
    }
}
