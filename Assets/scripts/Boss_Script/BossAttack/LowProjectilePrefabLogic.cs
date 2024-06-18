using UnityEngine;

public class LowProjectilePrefabLogic : MonoBehaviour
{
    public float speed = 4f;
    public float lifetime = 20f;
    public float minYPosition = 1f;
    public float switchDistance = 2f; // stop homing at this

    private Transform target;
    private Vector3 travelDirection;
    private bool isHoming = true; 

    public void Initialize(Transform target)
    {
        this.target = target;
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        if (target != null && isHoming)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float currentDistance = Vector3.Distance(transform.position, target.position);

            if (currentDistance <= switchDistance)
            {
                isHoming = false; 
                travelDirection = direction; // keeping current direction
            }

     
            MoveProjectile(direction);
        }
        else if (!isHoming)
        {
           
            MoveProjectile(travelDirection);
        }
    }

    private void MoveProjectile(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
        if (newPosition.y < minYPosition)
        {
            newPosition.y = minYPosition; 
        }
        transform.position = newPosition;
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.CompareTag("Boss"))
        {
           // Debug.Log("boss hit low");
            return;
        }

        if (hitInfo.CompareTag("Player"))
        {
            CombatManager.instance.bossLowRangeAttackMethode();
            //Debug.Log("player hit low");
        }

        Destroy(gameObject); 
    }
}
