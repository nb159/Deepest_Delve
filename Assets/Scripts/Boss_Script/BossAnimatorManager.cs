


using UnityEngine;

public class BossAnimatorManager : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerHighAttack()
    {
        animator.SetTrigger("isHighAttacking");
    }

    public void TriggerLowAttack()
    {
        animator.SetTrigger("isLowAttacking");
    }

    public void TriggerArmAttack()
    {
        animator.SetTrigger("isArmAttacking");
    }

    public void TriggerEnraged()
    {
        animator.SetTrigger("isEnraged");
    }
     public void SetIdle()
    {
        animator.SetTrigger("isIdle");
    }
}
