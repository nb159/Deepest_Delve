using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotation : MonoBehaviour
{
    public Transform player;
    private BossAnimatorManager bossAnimatorManager;
 public void Initialize(BossAnimatorManager animatorManager)
    {
        bossAnimatorManager = animatorManager;
    }
    void Start()
    {
        bossAnimatorManager = GetComponent<BossAnimatorManager>();
        if (bossAnimatorManager == null)
        {
            Debug.LogError("BossAnimatorManager not found on the same GameObject.");
        }
    }

    void FixedUpdate()
    {
        FacePlayer();
    }

    void FacePlayer()
    {
        if (player != null && bossAnimatorManager != null && bossAnimatorManager.canRotate)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
      
    }
}
