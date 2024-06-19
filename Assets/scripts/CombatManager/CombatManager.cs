using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public GameObject itemSelectionUI;

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
        //Debug.Log(GameManager.instance.bossHealth + "  " + lightAttackDamage);
        GameManager.instance.bossHealth -= lightAttackDamage;

// from here the porta should be toggled

        if (GameManager.instance.bossHealth <= 0)
        {
             
            // GameManager.instance.BossDefeated();
         
            //   bossAnimatorManager.SetDeathAnimation();
               // Destroy(Boss);
            PortalManager.instance.togglePortal(true);
         
             
        }

        // if (GameManager.instance.bossHealth <= 0)
        // TODO: itemSelectionUI.GetComponent<UIItemSelection>().ShowRandomItems();
        // GameManager.instance.nextLevel();
    }


    // public void bossHighRangeAttackMethode(){
    //     //Debug.Log(GameManager.instance.bossHealth+" " + lightAttackDamage);
    //     GameManager.instance.playerHealth -= bossHighRangeAttack;
    //           Debug.Log(  GameManager.instance.playerHealth);

    // }
    //   public void bossLowRangeAttackMethode(){
    //     //Debug.Log(GameManager.instance.bossHealth+" " + lightAttackDamage);
    //     GameManager.instance.playerHealth -= bossLowRangeAttack;
    //           Debug.Log(  GameManager.instance.playerHealth);

    // }

    public void bossHighRangeAttackMethode()
    {
        //Debug.Log(bossHighRangeAttack + " " + GameManager.instance.playerHealth);

        // Debug.Log(GameManager.instance.bossHealth+" " + lightAttackDamage);
        // Debug.Log(PlayerLocomotion.instance.isInvulnerable+"this should be false");

        // if (!PlayerLocomotion.instance.isInvulnerable) GameManager.instance.playerHealth -= bossHighRangeAttack;

        // // Debug.Log(GameManager.instance.playerHealth);

        // if (GameManager.instance.playerHealth <= 0)
        // {
        //     //GameManager.instance.ChangeScene(GameScene.PlayerDeathScene);
        // }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        //----- this is just for debugging cause i cannot deal damage to the boss with players sword----//


          GameManager.instance.playerHealth -= bossHighRangeAttack;

        if (GameManager.instance.bossHealth <= 0)
        {
           // GameManager.instance.BossDefeated();
            PortalManager.instance.togglePortal(true);

        }



    }

    public void bossLowRangeAttackMethode()
    {
        //Debug.Log("low: " + bossLowRangeAttack + " " + GameManager.instance.playerHealth);

        if (!PlayerLocomotion.instance.isInvulnerable) GameManager.instance.playerHealth -= bossLowRangeAttack;
        // Debug.Log(GameManager.instance.playerHealth);


        if (GameManager.instance.playerHealth <= 0)
        {

            //GameManager.instance.ChangeScene(GameScene.PlayerDeathScene);
        }


    }


    public void bossArmAttackMethode()
    {
        // Debug.Log("low: " + bossArmAttack + " " + GameManager.instance.playerHealth);

        if (!PlayerLocomotion.instance.isInvulnerable) GameManager.instance.playerHealth -= bossArmAttack;
        Debug.Log(bossArmAttack+"hit by arm");
        //Debug.Log("arm: " + bossArmAttack + " " + GameManager.instance.playerHealth);

        //Debug.Log("testing if armattack methode works");
        if (GameManager.instance.playerHealth <= 0)
        {
            GameManager.instance.ChangeScene(GameScene.PlayerDeathScene);

        }


    }
    public void tesy1()
    {
        Debug.Log("test1");
    }
}