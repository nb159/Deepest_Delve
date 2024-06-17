using UnityEngine;

public class HighProjectilePrefabLogic : MonoBehaviour
{
    public float lifetime = 10f; 
    public float showIndicatorThreshhold = 5f;  /// this is the thr
    public GameObject groundIndicatorPrefab; 

    private GameObject indicator; 
    private bool indicatorInstantiated = false;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (!indicatorInstantiated && transform.position.y <= showIndicatorThreshhold)
        {
            InstantiateIndicator();
        }
    }

    void InstantiateIndicator()
    {
        Vector3 groundPosition = new Vector3(transform.position.x, -0.1f, transform.position.z);
        indicator = Instantiate(groundIndicatorPrefab, groundPosition, Quaternion.identity);
        indicatorInstantiated = true;
    }

    void OnDestroy()
    {
        if (indicator != null)
        {
            Destroy(indicator);
        }
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.CompareTag("Boss") || hitInfo.CompareTag("Projectile"))
        {
            return;
        }

        if (hitInfo.CompareTag("Player"))
        {
            CombatManager.instance.bossHighRangeAttackMethode();
            Debug.Log("Player hit by highrange attack");
        }

        Destroy(gameObject);
    }
}
