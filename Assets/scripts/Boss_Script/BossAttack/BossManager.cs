using UnityEngine;
public class BossManager : MonoBehaviour
{
    public Transform player;
    public float attackRange = 20f;
    public float lowAttackRange = 10f;
    public float armAttackRange = 5f;
    public float potionAttackChance = 0.5f;
    public float bossHealth = 100f;
    public float enragedHealthThreshold = 50f;
    public Collider closeProximityCollider;
    public Collider farProximityCollider;

    private BossAnimatorManager bossAnimatorManager;

    private BossAttackState currentState;

    private HighRangeAttack highRangeAttack;
    private LowRangeAttack lowRangeAttack;

    private OnPotionUseProjectile onPotionUseProjectile;

    private BossRotation bossRotation;
    void Start()
    {
        highRangeAttack = GetComponent<HighRangeAttack>();

        lowRangeAttack = GetComponent<LowRangeAttack>();
        onPotionUseProjectile = GetComponent<OnPotionUseProjectile>();

        bossAnimatorManager = GetComponent<BossAnimatorManager>();
        bossRotation = GetComponent<BossRotation>();

        // if (highRangeAttack == null || lowRangeAttack == null || bossAnimatorManager == null ||
        //     onPotionUseProjectile == null)
       if (bossAnimatorManager == null)
        {
            Debug.LogError("BossAnimatorManager not found on the same GameObject.");
        }

        if (bossRotation == null)
        {
            Debug.LogError("BossRotation not found on the same GameObject.");
        }
        else
        {
            bossRotation.Initialize(bossAnimatorManager);
        }
        if (highRangeAttack == null || lowRangeAttack == null || bossAnimatorManager == null || onPotionUseProjectile == null)
        {
            // Debug.LogError("Essential components are missing.");
            enabled = false;
            return;
        }

        closeProximityCollider.enabled = false;
        farProximityCollider.enabled = false;
        currentState = BossAttackState.Idle;
        InputManager.OnDrinkPotion += TryOnPotionUseAttack;

          if (bossRotation.player == null)
        {
            bossRotation.player = player;
        }
    }

    private void Update()
    {
        if (player == null) return;

        var distanceToPlayer = Vector3.Distance(transform.position, player.position);
        // Debug.Log($"Current State: {currentState}, Distance to Player: {distanceToPlayer}");

        ///    Debug.Log(GameManager.instance.bossHealth + "deaddddddddddd");
        if (GameManager.instance.bossHealth <= 0){
            currentState = BossAttackState.Dead;
        }
        else{
            DetermineAttackState(distanceToPlayer);
        }

        if (GameManager.instance.bossHealth > 0) { ExecuteCurrentState(distanceToPlayer); }
    }

    private void OnDestroy()
    {
        InputManager.OnDrinkPotion -= TryOnPotionUseAttack;
    }

    private void DetermineAttackState(float distanceToPlayer)
    {
        if (distanceToPlayer <= armAttackRange)
            currentState = BossAttackState.ArmAttack;

        else if (distanceToPlayer <= lowAttackRange)
            currentState = BossAttackState.LowAttack;
        else if (distanceToPlayer <= attackRange)
            currentState = BossAttackState.HighAttack;
        else{
            currentState = BossAttackState.Idle;
            bossAnimatorManager.canRotate = true;
        }
    }

    public void rotateBoss()
    {
        bossAnimatorManager.canRotate = true;

    }

  public void StopRotateBoss()
    {
        bossAnimatorManager.canRotate = false;
      

    }
    private void ExecuteCurrentState(float distanceToPlayer)
    {
        switch (currentState)
        {
            case BossAttackState.Idle:
                bossAnimatorManager.SetIdle();
                break;
            case BossAttackState.HighAttack:
                ExecuteHighAttackState(distanceToPlayer);
                break;
            case BossAttackState.LowAttack:
                ExecuteLowAttackState();
                break;

            case BossAttackState.ArmAttack:
                ExecuteArmAttackState();
                break;
            case BossAttackState.Enraged:
                ExecuteEnragedState();
                break;
            case BossAttackState.Dead:
                ExecuteDeath();
                break;
        }
    }

    private void ExecuteHighAttackState(float distanceToPlayer)
    {
        bossAnimatorManager.TriggerHighAttack();
        if (distanceToPlayer > attackRange)
            currentState = BossAttackState.Idle;
        else
            highRangeAttack.ExecuteAttack(player);
    }

    private void ExecuteLowAttackState()
    {

        lowRangeAttack.ExecuteAttack(player);
    }

    //  


    private void ExecuteArmAttackState()
    {
        bossAnimatorManager.TriggerArmAttack();
    }

    private void ExecuteDeath()
    {
        bossAnimatorManager.TriggerDeath();
    }

    public void CloseProximityCollision(int conditionCollider)
    {
        if (conditionCollider == 1)
            closeProximityCollider.enabled = true;
        else
            closeProximityCollider.enabled = false;


        Debug.Log("hello toggle arm collider");
    }

    public void FarProximityCollision(int conditionCollider)
    {
        if (conditionCollider == 1)
            farProximityCollider.enabled = true;
        else
            farProximityCollider.enabled = false;


        Debug.Log("hello toggle arm collider");
    }

    private void ExecuteEnragedState()
    {
        bossAnimatorManager.TriggerEnraged();
        // i will add logic for enraged state attacks
    }


    private void TryOnPotionUseAttack()
    {
        if (Random.value < potionAttackChance)
            // onPotionUseProjectile.ExecuteAttack(player);
            bossAnimatorManager.TriggerOnPotionAttack();
    }


    //used this in fireBall event
    public void fireOnPotionEvent()
    {
        if (Random.value < potionAttackChance)
            //Debug.Log("hello from keyframe");
            onPotionUseProjectile.ExecuteAttack(player);
        //   bossAnimatorManager.TriggerOnPotionAttack();
    }

    private enum BossAttackState
    {
        Idle,
        HighAttack,
        LowAttack,
        ArmAttack,
        Enraged,
        Dead
    }
}