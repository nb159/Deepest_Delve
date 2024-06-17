


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
        animator.SetTrigger("HighAttackTrigger");
    }

    public void TriggerLowAttack()
    {
        animator.SetTrigger("low");
    }

    public void TriggerArmAttack()
    {
        animator.SetTrigger("ArmAttackTrigger");
    }

    public void TriggerEnraged()
    {
        animator.SetTrigger("EnragedTrigger");
    }
}
