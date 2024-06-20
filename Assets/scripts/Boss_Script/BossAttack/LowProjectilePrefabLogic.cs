using UnityEngine;

public class LowProjectilePrefabLogic : MonoBehaviour
{
    public float speed = 4f;
    public float lifetime = 20f;
    public float minYPosition = 1.8f;
    public float switchDistance = 2f;

    private Transform target;
    private Vector3 travelDirection;
    private bool isHoming = true;
    public ParticleSystem groundVFX1;

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
                travelDirection = direction;
            }

            if (groundVFX1 != null)
            {
              
                ParticleSystem vfxInstance = Instantiate(groundVFX1, transform.position, Quaternion.identity);
                vfxInstance.Play();

              
                Destroy(vfxInstance.gameObject, vfxInstance.main.duration + vfxInstance.main.startLifetime.constantMax);
            }
            else
            {
                Debug.LogError("Particle System (groundVFX1) is not assigned.");
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


        if (hitInfo.CompareTag("Boss") || hitInfo.CompareTag("highRangeProjectile") || hitInfo.CompareTag("lowRangeProjectile") || hitInfo.CompareTag("onPotionProjectile"))
        {
            return;
        }

        if (hitInfo.CompareTag("Player"))
        {
           CombatManager.instance.bossLowRangeAttackMethode();

             BossAnimatorManager.Instance.lowRangeAttackSound.Post(gameObject);


        
        }

        Destroy(gameObject);
    }
}
