using UnityEngine;

public class LowProjectilePrefabLogic : MonoBehaviour
{
    public float speed = 40f;
    public float lifetime = 10f;
    public float minYPosition = 2f; // Set your desired minimum y position

    private Transform target;

    public void Initialize(Transform target)
    {
        this.target = target;
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
            if (newPosition.y < minYPosition)
            {
                newPosition.y = minYPosition;
            }
            transform.position = newPosition;
        }
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.CompareTag("Boss"))
        {
            return;
        }

        if (hitInfo.CompareTag("Player"))
        {
            CombatManager.instance.bossLowRangeAttackMethode();
            Debug.Log("player hit low");
        }

        Destroy(gameObject);
    }
}
