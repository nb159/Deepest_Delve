using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OnPotionUseProjectile : MonoBehaviour, IBossAttack
{
    public GameObject trackingProjectilePrefab;
    public float fireDelayFactor = 0.7f; 

    private BossAnimatorManager bossAnimatorManager;
    private Vector3 projectileSpawnOffset = new Vector3(0f, 0f, 4f); 

  

    public void ExecuteAttack(Transform player)
    {
        

        Vector3 spawnPosition = transform.position + transform.TransformDirection(projectileSpawnOffset);

       
        GameObject projectile = Instantiate(trackingProjectilePrefab, spawnPosition, Quaternion.identity);
        prefabOnpotion trackingProjectile = projectile.GetComponent<prefabOnpotion>();
        if (trackingProjectile != null)
        {
            trackingProjectile.Initialize(player);
        }
      
    }

}
