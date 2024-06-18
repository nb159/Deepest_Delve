using UnityEngine;

public class HighRangeAttack : MonoBehaviour, IBossAttack
{
    public GameObject projectilePrefab;
    public float spawnHeight = 200f;
    public float HeightOffset = 100f;
    public float attackCooldown = 10f;
    public int projectileCount = 30;
    public float attackDuration = 20;
    public float attackRadius = 10f;
    public GameObject groundIndicatorPrefab;
    private float lastAttackTime;

    public void ExecuteAttack(Transform player)
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            Vector3 playerPosition = player.position; 
            for (int i = 0; i < projectileCount; i++)
            {
              
                float offsetX = Random.Range(-attackRadius, attackRadius);
                float offsetZ = Random.Range(-attackRadius, attackRadius);// trying to make the position of projectiles relative to the player
                Vector3 spawnPosition = new Vector3(playerPosition.x + offsetX, Random.Range(spawnHeight - HeightOffset, spawnHeight), playerPosition.z + offsetZ);

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
