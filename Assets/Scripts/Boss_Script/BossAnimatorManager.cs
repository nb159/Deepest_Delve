using System;
using UnityEngine;

public class BossAnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator[] golemAnimator;



    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
         if (animator == null)
        {
            Debug.LogError("Animator component not found on the same GameObject.");
        }
    }

    public bool canRotate = true;
    // [Header("What Boss can do")]
    // public bool canHighRangeAttack = true;
    // public bool canLowRangeAttack = true;
    // public bool canOnPotionAttack = true;
    // public bool canArmAttack = true;





    public void TriggerHighAttack()
    {
        // if (canHighRangeAttack)
        // {

        //     canArmAttack=false;
        //     canLowRangeAttack=false;
        //     canOnPotionAttack=false;
        // canRotate = false;


        animator.SetTrigger("isHighAttacking");
        if (golemAnimator != null) Array.ForEach(golemAnimator, ani => ani.SetTrigger("isHighAttacking"));
        // }

    }

    public void TriggerLowAttack()
    {
        //   if (canLowRangeAttack)
        // {

        //     canArmAttack=false;
        //     canHighRangeAttack=false;
        //     canOnPotionAttack=false;

        
        animator.SetTrigger("isLowAttacking");
        if (golemAnimator != null) Array.ForEach(golemAnimator, ani => ani.SetTrigger("isLowAttacking"));
        // }
    }

    public void TriggerOnPotionAttack()
    {
        //  if (canOnPotionAttack)
        // {

        //     canArmAttack=false;
        //     canHighRangeAttack=false;
        //     canLowRangeAttack=false;

        
        animator.SetTrigger("isOnPotion");
        //}
    }

    public bool IsOnPotionPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("fireBall");
    }

    public float GetAnimationLength(string animationName)
    {
        var ac = animator.runtimeAnimatorController;
        foreach (var clip in ac.animationClips)
            if (clip.name == animationName)
                return clip.length;
        return 0f;
    }

    public void TriggerArmAttack()
    {
        //  if (canArmAttack)
        // {

        //     canOnPotionAttack=false;
        //     canHighRangeAttack=false;
        //     canLowRangeAttack=false;

         canRotate = false;
        animator.SetTrigger("isArmAttacking");
        //}
    }

    public void TriggerEnraged()
    {
        animator.SetTrigger("isEnraged");
    }

    public void TriggerDeath()
    {
        // canHighRangeAttack=false;
        //     canArmAttack=false;
        //     canLowRangeAttack=false;
        //     canOnPotionAttack=false;
         canRotate = false;
        animator.SetTrigger("isDead");

    }

    public void SetIdle()
    {
        //animator.SetTrigger("isIdle");
         canRotate = true;
        animator.SetBool("isIdle", true);
    }
}