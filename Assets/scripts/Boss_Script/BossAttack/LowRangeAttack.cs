using Unity.VisualScripting;
using UnityEngine;

public class LowRangeAttack : MonoBehaviour, IBossAttack
{
    public GameObject trackingProjectilePrefab;
    public float attackCooldown = 3f;

    public ParticleSystem groundVFX1;
    private BossAnimatorManager bossAnimatorManager;

    void Start()
    {
        bossAnimatorManager = GetComponent<BossAnimatorManager>();

    }

    private float lastAttackTime;

    public void ExecuteAttack(Transform player)
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            bossAnimatorManager.TriggerLowAttack();
            
            if (groundVFX1 != null)
            {
               
                groundVFX1.Play();
        

            }
            else
            {
                Debug.LogError("Particle System (groundVFX1) is not assigned.");
            }

            GameObject projectile = Instantiate(trackingProjectilePrefab, transform.position, Quaternion.identity);
            LowProjectilePrefabLogic trackingProjectile = projectile.GetComponent<LowProjectilePrefabLogic>();
            if (trackingProjectile != null)
            {
                trackingProjectile.Initialize(player);
            }
            lastAttackTime = Time.time;
        }
    }
}