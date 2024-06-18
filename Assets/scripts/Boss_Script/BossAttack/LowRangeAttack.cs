using System.Collections;
using UnityEngine;

public class LowRangeAttack : MonoBehaviour, IBossAttack
{
    public GameObject trackingProjectilePrefab;
    public float fireDelayFactor = 0.7f; 

    private BossAnimatorManager bossAnimatorManager;
    private Vector3 projectileSpawnOffset = new Vector3(0f, 0f, 2f); //to controll where projectiles are

    void Start()
    {
        bossAnimatorManager = GetComponent<BossAnimatorManager>();
    }

    public void ExecuteAttack(Transform player)
    {
        bossAnimatorManager.TriggerLowAttack();
        StartCoroutine(WaitAndShoot(player));
    }

    private IEnumerator WaitAndShoot(Transform player)
    {
       
        yield return new WaitUntil(() => !bossAnimatorManager.IsLowAttackPlaying());

       
        float totalAnimationTime = bossAnimatorManager.GetAnimationLength("fireBall");
        Debug.Log("Fireball Animation Time: " + totalAnimationTime);
        float delay = totalAnimationTime * fireDelayFactor;
        yield return new WaitForSeconds(delay);

     
        Vector3 spawnPosition = transform.position + transform.TransformDirection(projectileSpawnOffset);

       
        GameObject projectile = Instantiate(trackingProjectilePrefab, spawnPosition, Quaternion.identity);
        LowProjectilePrefabLogic trackingProjectile = projectile.GetComponent<LowProjectilePrefabLogic>();
        if (trackingProjectile != null)
        {
            trackingProjectile.Initialize(player);
        }
    }
}
