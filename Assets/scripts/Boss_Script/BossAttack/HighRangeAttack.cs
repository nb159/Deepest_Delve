using UnityEngine;

public class HighRangeAttack : MonoBehaviour, IBossAttack
{
    public GameObject projectilePrefab;
    public float spawnHeight = 200f;
    public float attackCooldown = 10f;
    public int projectileCount = 30;
    public float attackDuration = 20;
    public float attackRadius = 17f;
    public GameObject groundIndicatorPrefab;
    private float lastAttackTime;

    public void ExecuteAttack(Transform player)
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-attackRadius, attackRadius), Random.Range(spawnHeight - 100, spawnHeight), Random.Range(-attackRadius, attackRadius));
                GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

              
                HighProjectilePrefabLogic projectileLogic = projectile.GetComponent<HighProjectilePrefabLogic>();
                if (projectileLogic != null)
                {
                    projectileLogic.groundIndicatorPrefab = groundIndicatorPrefab;
                }
            }
            lastAttackTime = Time.time;
        }
    }

    public float GetAttackDuration()
    {
        return attackDuration;
    }
}
