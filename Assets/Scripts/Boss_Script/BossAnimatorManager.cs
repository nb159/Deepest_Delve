using System;
using UnityEngine;

public class BossAnimatorManager : MonoBehaviour
{
    // Static singleton property
    public static BossAnimatorManager Instance { get; private set; }
   public ParticleSystem groundVFX1;
   public GameObject armPosForVfx;
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
public void ArmAttackVFX(){
    if (groundVFX1 != null)
{
  
    ParticleSystem vfxInstance = Instantiate(groundVFX1, armPosForVfx.transform.position, Quaternion.identity);

    Vector3 adjustedPosition = vfxInstance.transform.position;
    adjustedPosition.y -= 1.9f;
    vfxInstance.transform.position = adjustedPosition;

   
    Debug.Log(vfxInstance);
    
   
    vfxInstance.Play();

    Destroy(vfxInstance.gameObject, vfxInstance.main.duration + vfxInstance.main.startLifetime.constantMax);
}
        else
        {
            Debug.LogError("Particle System (groundVFX1) is not assigned.");
        }
}
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
