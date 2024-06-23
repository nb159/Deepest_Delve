using UnityEngine;

public class HighProjectilePrefabLogic : MonoBehaviour
{

    public float lifetime = 10f;
    public float showIndicatorThreshhold = 5f; 
    public GameObject groundIndicatorPrefab;
    public ParticleSystem groundVFX1;
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

        if (hitInfo.CompareTag("Boss") || hitInfo.CompareTag("highRangeProjectile") || hitInfo.CompareTag("lowRangeProjectile") || hitInfo.CompareTag("onPotionProjectile"))
        {
            return;
        }

        if (hitInfo.CompareTag("Player"))
        {
            CombatManager.instance.bossHighRangeAttackMethode();


        }

        Destroy(gameObject);
    }
}
