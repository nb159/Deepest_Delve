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
    [SerializeField] public float playerCritDamge = 1f;

    [Header("Boss Stats")]
    // [SerializeField]
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
        GameManager.instance.bossHealth -= lightAttackDamage;

      if (GameManager.instance.bossHealth <= 0)
        {
            PortalManager.instance.togglePortal(true);

        }

    }




    public void bossHighRangeAttackMethode()
    {

        if (!PlayerLocomotion.instance.isInvulnerable) GameManager.instance.playerHealth -= bossHighRangeAttack;

    }

    public void bossLowRangeAttackMethode()
    {
        if (!PlayerLocomotion.instance.isInvulnerable) GameManager.instance.playerHealth -= bossLowRangeAttack;

    }


    public void bossArmAttackMethode()
    {

        if (!PlayerLocomotion.instance.isInvulnerable) GameManager.instance.playerHealth -= bossArmAttack;
  
    }


    public void tesy1()
    {
        Debug.Log("test1");
    }
}