using UnityEngine;

public class BossManager : MonoBehaviour
{
    private enum BossAttackState { Idle, HighAttack, LowAttack, ArmAttack, Enraged }

    private BossAttackState currentState;

    public Transform player;
    public float attackRange = 20f;
    public float lowAttackRange = 10f;
    public float armAttackRange = 5f;
    public float potionAttackChance = 0.5f;
    public float bossHealth = 100f;
    public float enragedHealthThreshold = 50f;

    private HighRangeAttack highRangeAttack;
    private LowRangeAttack lowRangeAttack;
    private BossAnimatorManager bossAnimatorManager;

    void Start()
    {
        highRangeAttack = GetComponent<HighRangeAttack>();
        lowRangeAttack = GetComponent<LowRangeAttack>();
        bossAnimatorManager = GetComponent<BossAnimatorManager>();

        if (highRangeAttack == null || lowRangeAttack == null || bossAnimatorManager == null)
        {
            Debug.LogError("Essential components are missing.");
            enabled = false;
            return;
        }

        currentState = BossAttackState.Idle;
        InputManager.OnDrinkPotion += TryLowRangeAttackState;
    }

    void OnDestroy()
    {
        InputManager.OnDrinkPotion -= TryLowRangeAttackState;
    }

    void Update()
    {
        if (player == null) return; 

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //Debug.Log($"Current State: {currentState}, Distance to Player: {distanceToPlayer}");

        if (bossHealth <= enragedHealthThreshold)
        {
            currentState = BossAttackState.Enraged;
        }
        else
        {
            DetermineAttackState(distanceToPlayer);
        }

        ExecuteCurrentState(distanceToPlayer);
    }

    private void DetermineAttackState(float distanceToPlayer)
    {
        if (distanceToPlayer <= armAttackRange)
        {
            currentState = BossAttackState.ArmAttack;
        }
        else if (distanceToPlayer <= lowAttackRange)
        {
            currentState = BossAttackState.LowAttack;
        }
        else if (distanceToPlayer <= attackRange)
        {
            currentState = BossAttackState.HighAttack;
        }
        else
        {
            currentState = BossAttackState.Idle;
        }
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
        }
    }

    private void ExecuteHighAttackState(float distanceToPlayer)
    {
        bossAnimatorManager.TriggerHighAttack();
        if (distanceToPlayer > attackRange)
        {
            currentState = BossAttackState.Idle;
        }
        else
        {
            highRangeAttack.ExecuteAttack(player);
        }
    }

    private void ExecuteLowAttackState()
    {
        bossAnimatorManager.TriggerLowAttack();
        lowRangeAttack.ExecuteAttack(player);
    }

    private void ExecuteArmAttackState()
    {
        bossAnimatorManager.TriggerArmAttack();
        if (CombatManager.instance != null)
        {
            CombatManager.instance.bossArmAttackMethode();
        }
    }

    private void ExecuteEnragedState()
    {
        bossAnimatorManager.TriggerEnraged();
        // i will add logic for enraged state attacks
    }

    private void TryLowRangeAttackState()
    {
        if (Random.value < potionAttackChance)
        {
            ExecuteLowAttackState();
           // Debug.Log("Executing Low Range Attack due to potion drink");
        }
    }
}