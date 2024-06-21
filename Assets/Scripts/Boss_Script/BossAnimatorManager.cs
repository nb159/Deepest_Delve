using System;
using UnityEngine;

public class BossAnimatorManager : MonoBehaviour
{
    // Static singleton property
    public static BossAnimatorManager Instance { get; private set; }

    [SerializeField] private Animator[] golemAnimator;
    private Animator animator;
    public AK.Wwise.Event lowRangeAttackSound;
    
    public AK.Wwise.Event bossDeathSound;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the same GameObject.");
        }
    }

    public bool canRotate = true;

    public void TriggerHighAttack()
    {
        animator.SetTrigger("isHighAttacking");
        if (golemAnimator != null)
            Array.ForEach(golemAnimator, ani => ani.SetTrigger("isHighAttacking"));
    }

    public void TriggerLowAttack()
    {
        animator.SetTrigger("isLowAttacking");
        if (golemAnimator != null)
            Array.ForEach(golemAnimator, ani => ani.SetTrigger("isLowAttacking"));
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

    public void TriggerDeath()
    {
        canRotate = false;
        animator.CrossFade("Death", 0.1f);
        animator.SetTrigger("isDead");
    }

    public void SetIdle()
    {
        canRotate = true;
        animator.CrossFade("idle", 0.1f);
        animator.SetBool("isIdle", true);
    }
}
