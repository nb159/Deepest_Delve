using Unity.VisualScripting;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private enum BossAttackState { Idle, HighAttack, LowAttack, ArmAttack, Enraged }

    //enraged gets enabled when boss reaches approx half hp 
    private BossAttackState currentState;

    public Transform player;
    public float attackRange = 20f;
    public float lowAttackRange = 10f;
    public float ArmAttackRange = 5f;
    public float potionAttackChance = 0.5f;

    private HighRangeAttack highRangeAttack;
    private LowRangeAttack lowRangeAttack;
    private ArmAttack armAttack;
   // public float bossHp = GameManager.instance.bossHealth;



    //private EnragedAttack enragedAttack;


    void Start()
    {
        highRangeAttack = GetComponent<HighRangeAttack>();
        lowRangeAttack = GetComponent<LowRangeAttack>();
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
       // Debug.Log(distanceToPlayer);

        switch (currentState)
        {
            case BossAttackState.Idle:
                HandleIdleState(distanceToPlayer);
                break;
            case BossAttackState.HighAttack:
                HandleHighAttackState(distanceToPlayer);
                break;
            case BossAttackState.LowAttack:
                TryLowRangeAttackState();
                break;

            case BossAttackState.ArmAttack:
                HandelArmAttackState(distanceToPlayer);
                break;
                // case BossAttackState.EnragedAttack:
                //     HandelEnragedAttackState(bossHp);
                //     break;
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
            // if (distanceToPlayer < ArmAttackRange)
            // {
            //     currentState = BossAttackState.ArmAttack;
            // }
            // else
            // {
            //     currentState = BossAttackState.LowAttack;
            // }
        }
    }

    private void HandelArmAttackState(float distanceToPlayer)
    {

        if (distanceToPlayer < ArmAttackRange)
        {
            currentState = BossAttackState.ArmAttack;
            armAttack.ExecuteAttack(player);

        }
    }


    private void HandleHighAttackState(float distanceToPlayer)
    {
        if (distanceToPlayer > attackRange)
        {
            currentState = BossAttackState.Idle;
        }
        // else if (distanceToPlayer <= lowAttackRange)
        // {
        //     currentState = BossAttackState.LowAttack;
        // }
        else
        {
            highRangeAttack.ExecuteAttack(player);
        }
    }



    // private void HandeleEnragedAttackState(float bossHp) // pass boss hp as param. 
    // {


    // }



    // private void HandleLowAttackState(float distanceToPlayer)
    // {
    //     if (distanceToPlayer > attackRange)
    //     {
    //         currentState = BossAttackState.Idle;
    //     }
    //     else if (distanceToPlayer > lowAttackRange)
    //     {
    //         currentState = BossAttackState.HighAttack;
    //     }
    //     else
    //     {
    //         lowRangeAttack.ExecuteAttack(player);
    //     }
    // }


    //random low attack on drinking potion ... but maybe i change it to a different attack later 
    private void TryLowRangeAttackState()
    {
        if (Random.value < potionAttackChance)
        {


            lowRangeAttack.ExecuteAttack(player);

        }
    }
}





