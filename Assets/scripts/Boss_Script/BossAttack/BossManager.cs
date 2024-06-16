


using Unity.VisualScripting;
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
    public float bossHealth = 100f; // Assume some health value
    public float enragedHealthThreshold = 50f; // Threshold for enraged state

    private HighRangeAttack highRangeAttack;
    private LowRangeAttack lowRangeAttack;
    private ArmAttack armAttack;
    private BossAnimatorManager bossAnimatorManager;

    void Start()
    {
        highRangeAttack = GetComponent<HighRangeAttack>();
        lowRangeAttack = GetComponent<LowRangeAttack>();
        armAttack = GetComponent<ArmAttack>();
        bossAnimatorManager = GetComponent<BossAnimatorManager>();
        currentState = BossAttackState.Idle;

        InputManager.OnDrinkPotion += TryLowRangeAttackState;
    }

    void OnDestroy()
    {
        InputManager.OnDrinkPotion -= TryLowRangeAttackState;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (bossHealth <= enragedHealthThreshold)
        {
            currentState = BossAttackState.Enraged;
        }

        switch (currentState)
        {
            case BossAttackState.Idle:
                HandleIdleState(distanceToPlayer);
                break;
            case BossAttackState.HighAttack:
                HandleHighAttackState(distanceToPlayer);
                break;
            case BossAttackState.LowAttack:
                HandleLowAttackState(distanceToPlayer);
                break;
            case BossAttackState.ArmAttack:
                HandleArmAttackState(distanceToPlayer);
                break;
            case BossAttackState.Enraged:
                HandleEnragedState();
                break;
        }
    }

    private void HandleIdleState(float distanceToPlayer)
    {
        //bossAnimatorManager.SetIdle();

        if (distanceToPlayer < attackRange)
        {
            if (distanceToPlayer > lowAttackRange)
            {
                currentState = BossAttackState.HighAttack;
            }
            else if (distanceToPlayer < armAttackRange)
            {
                currentState = BossAttackState.ArmAttack;
            }
            else
            {
                currentState = BossAttackState.LowAttack;
            }
        }
    }

    private void HandleHighAttackState(float distanceToPlayer)
    {
       //bossAnimatorManager.TriggerLowAttack();

        if (distanceToPlayer > attackRange)
        {
            currentState = BossAttackState.Idle;
        }
        else
        {
            highRangeAttack.ExecuteAttack(player);
        }
    }

     private void HandleLowAttackState(float distanceToPlayer)
    {
      

        if (distanceToPlayer > attackRange)
        {
            currentState = BossAttackState.Idle;
        }
        else if (distanceToPlayer > lowAttackRange)
        {
            currentState = BossAttackState.HighAttack;
        }
        else
        {
            lowRangeAttack.ExecuteAttack(player);
        }
    }

    private void HandleArmAttackState(float distanceToPlayer)
    {
        

        if (distanceToPlayer > armAttackRange)
        {
            currentState = BossAttackState.Idle;
        }
        else
        {
            armAttack.ExecuteAttack(player);
        }
    }

    private void HandleEnragedState()
    {
       
    }

    private void TryLowRangeAttackState()
    {
       if (Random.value < potionAttackChance)
       {
           bossAnimatorManager.TriggerLowAttack();
            lowRangeAttack.ExecuteAttack(player);
           // Debug.Log("hiiii");
       }
    }
}
