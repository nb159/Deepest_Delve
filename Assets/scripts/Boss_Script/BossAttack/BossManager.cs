using UnityEngine;

public class BossManager : MonoBehaviour
{
    private enum BossAttackState { Idle, HighAttack, LowAttack }
    private BossAttackState currentState;

    public Transform player;
    public float attackRange = 50f;
    public float lowAttackRange = 10f;
    public float potionAttackChance = 0.5f; 

    private HighRangeAttack highRangeAttack;
    private LowRangeAttack lowRangeAttack;

    void Start()
    {
        highRangeAttack = GetComponent<HighRangeAttack>();
        lowRangeAttack = GetComponent<LowRangeAttack>();
        currentState = BossAttackState.Idle;

        InputManager.OnDrinkPotion += TryLowRangeAttack; 
    }

    void OnDestroy()
    {
        InputManager.OnDrinkPotion -= TryLowRangeAttack; 
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case BossAttackState.Idle:
                HandleIdleState(distanceToPlayer);
                break;
            case BossAttackState.HighAttack:
                HandleHighAttackState(distanceToPlayer);
                break;
            case BossAttackState.LowAttack:
                TryLowRangeAttack();
                break;
        }
    }

    private void HandleIdleState(float distanceToPlayer)
    {
        if (distanceToPlayer < attackRange)
        {
            if (distanceToPlayer > lowAttackRange)
            {
                currentState = BossAttackState.HighAttack;
            }
            else
            {
                currentState = BossAttackState.LowAttack;
            }
        }
    }

    private void HandleHighAttackState(float distanceToPlayer)
    {
        if (distanceToPlayer > attackRange)
        {
            currentState = BossAttackState.Idle;
        }
        else if (distanceToPlayer <= lowAttackRange)
        {
            currentState = BossAttackState.LowAttack;
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


//random low attack on drinking potion ... but maybe i change it to a different attack later 
    private void TryLowRangeAttack()
    {
        if (Random.value< potionAttackChance)
        {
            
           
                lowRangeAttack.ExecuteAttack(player);
            
        }
    }
}
