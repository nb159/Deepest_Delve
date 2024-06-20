using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public GameObject itemSelectionUI;
    
    public Collider closeProximityCollider;
    public Collider farProximityCollider;

    [Header("Player Stats")]
    [SerializeField]
    public float lightAttackDamage = 10f;

    [SerializeField] public float heavyAttackDamage = 20f;
    [SerializeField] public float playerDefense = 10f;
    [SerializeField] public float playerCritDamage = 1f;
    [SerializeField] public float playerCritChance = 0.2f;

    [Header("Boss Stats")]
    [SerializeField] public float bossHighRangeAttack = 10f;
    [SerializeField] public float bossLowRangeAttack = 20f;
    [SerializeField] public float bossArmAttack = 20f;
    [SerializeField] public float bossHealing;
    [SerializeField] public float bossHealingDuration;

    private BossAnimatorManager bossAnimatorManager;
    private GameObject Boss;
    private GameObject Player;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        bossAnimatorManager = GetComponent<BossAnimatorManager>();
        Boss = GameObject.FindWithTag("Boss");
        Player = GameObject.FindWithTag("Player");

        if (Boss == null)
        {
            Debug.LogError("Boss object not found in the scene.");
        }

        if (Player == null)
        {
            Debug.LogError("Player object not found in the scene.");
        }
    }

    public void playerLightAttack()
    {
        float damage = lightAttackDamage;
        float rand = UnityEngine.Random.value;

        if (rand <= playerCritChance)
        {
            damage += playerCritDamage;
        }

        applyDamageToBoss(damage);

        if (GameManager.instance.bossHealth <= 0)
        {
            BossAnimatorManager.Instance.TriggerDeath();
            PortalManager.instance.togglePortal(true);
        }
    }

    public void playerComboAttack()
    {
        float damage = heavyAttackDamage;
        float rand = UnityEngine.Random.value;

        if (rand <= playerCritChance)
        {
            damage += playerCritDamage;
        }

        applyDamageToBoss(damage);
    }

    public void applyDamageToBoss(float damage)
    {
        Debug.Log(damage);
        GameManager.instance.bossHealth -= damage;
    }

    public void bossHighRangeAttackMethode()
    {
        applyDamageToPlayer(bossHighRangeAttack);
    }

    public void bossLowRangeAttackMethode()
    {
        applyDamageToPlayer(bossLowRangeAttack);
    }

    public void bossArmAttackMethode()
    {
        applyDamageToPlayer(bossArmAttack);
    }

    private void applyDamageToPlayer(float damage)
    {
        if (!PlayerLocomotion.instance.isInvulnerable)
        {
            float finalDamage = Mathf.Max(damage - playerDefense, 0);
            GameManager.instance.playerHealth -= finalDamage;
        }
    }

    public void tesy1()
    {
        Debug.Log("test1");
    }
}
