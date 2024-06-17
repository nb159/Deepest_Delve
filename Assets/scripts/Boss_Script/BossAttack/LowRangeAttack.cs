using System.Collections;
using UnityEngine;

public class LowRangeAttack : MonoBehaviour, IBossAttack
{
    public GameObject trackingProjectilePrefab;
    public float attackCooldown = 5f;
    public float fireDelayFactor = 0.4f; 

    private BossAnimatorManager bossAnimatorManager;
    private float lastAttackTime;

    void Start()
    {
        bossAnimatorManager = GetComponent<BossAnimatorManager>();
    }

    public void ExecuteAttack(Transform player)
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            bossAnimatorManager.TriggerLowAttack();
            StartCoroutine(WaitAndShoot(player));
            lastAttackTime = Time.time;
        }
    }

    private IEnumerator WaitAndShoot(Transform player)
    {
       
        yield return new WaitUntil(() => bossAnimatorManager.IsLowAttackPlaying());

      
        float totalAnimationTime = bossAnimatorManager.GetAnimationLength("fireBall");
        Debug.Log("firball"+totalAnimationTime);
        float delay = totalAnimationTime * fireDelayFactor;
        yield return new WaitForSeconds(delay);

        GameObject projectile = Instantiate(trackingProjectilePrefab, transform.position, Quaternion.identity);
        LowProjectilePrefabLogic trackingProjectile = projectile.GetComponent<LowProjectilePrefabLogic>();
        if (trackingProjectile != null)
        {
            trackingProjectile.Initialize(player);
        }
    }
}
