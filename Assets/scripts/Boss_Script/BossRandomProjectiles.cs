using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRandomProjectiles : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float fireRate = 2f;
    public float projectileSpeed = 10f;
    public int projectileDamage = 20;

    private float nextFireTime = 0f;

    public Vector3 spawnAreaMin; 
    public Vector3 spawnAreaMax; 

    public void SpawnProjectileAtRandomPosition()
    {
  
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        float randomZ = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

  
        GameObject projectile = Instantiate(projectilePrefab, randomPosition, Quaternion.identity);

        RandomProjectile projectileScript = projectile.GetComponent<RandomProjectile>();
        if (projectileScript != null)
        {
            projectileScript.Initialize( projectileDamage);
        }
        else
        {
            //Debug.LogError("Projectile script missing on projectile prefab.");
        }
    }

    void FixedUpdate()
    {
        if (Time.time >= nextFireTime)
        {
            SpawnProjectileAtRandomPosition();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}

