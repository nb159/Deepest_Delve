using UnityEngine;

public class HighRangeAttack : MonoBehaviour, IBossAttack
{
    public GameObject projectilePrefab;
    public float spawnHeight = 20f;
    public float attackCooldown = 5f;
    public int projectileCount = 30;

   
    public float attackRadius = 20f;
      public GameObject groundIndicatorPrefab;

    public float indicatorLifetime = 2f; 
    private float lastAttackTime;

    public void ExecuteAttack(Transform player)
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                Vector3 spawnPosition =  new Vector3(Random.Range(-attackRadius, attackRadius), Random.Range(spawnHeight-12,spawnHeight), Random.Range(-attackRadius, attackRadius));
                 Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
               //enhance the x and z position of the indicator and create a better prefab. 
               GameObject indicator = Instantiate(groundIndicatorPrefab, new Vector3(spawnPosition.x, -0.1f, spawnPosition.z), Quaternion.identity);
                Destroy(indicator, indicatorLifetime);
            }
            lastAttackTime = Time.time;
        }
    }
}
