using System;
using UnityEngine;

public class BossAnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator[] golemAnimator;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerHighAttack()
    {
        animator.SetTrigger("isHighAttacking");
        if (golemAnimator != null) Array.ForEach(golemAnimator, ani => ani.SetTrigger("isHighAttacking"));
    }

    public void TriggerLowAttack()
    {
        animator.SetTrigger("isLowAttacking");
        if (golemAnimator != null) Array.ForEach(golemAnimator, ani => ani.SetTrigger("isLowAttacking"));
    }

    public void TriggerOnPotionAttack()
    {
        animator.SetTrigger("isOnPotion");
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
        animator.SetTrigger("isArmAttacking");
    }

    public void TriggerEnraged()
    {
        animator.SetTrigger("isEnraged");
    }

    public void SetDeathAnimation()
    {
        animator.SetBool("isDead", true);
    }

    public void SetIdle()
    {
        //animator.SetTrigger("isIdle");
        animator.SetBool("isIdle", true);
    }
}