using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class prefabOnpotion : MonoBehaviour
{
    public float speed = 40f;
    public float lifetime = 3f;
    public float minYPosition = 4f; 

    private Transform target;

    public void Initialize(Transform target)
    {
        this.target = target;
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
            if (newPosition.y < minYPosition)
            {
                newPosition.y = minYPosition;
            }
            transform.position = newPosition;
        }
    }

    void OnTriggerEnter(Collider hitInfo)
    {
       if (hitInfo.CompareTag("Boss") || hitInfo.CompareTag("highRangeProjectile")|| hitInfo.CompareTag("lowRangeProjectile") || hitInfo.CompareTag("onPotionProjectile"))
        {
            return;
        }

        if (hitInfo.CompareTag("Player"))
        {
            CombatManager.instance.bossLowRangeAttackMethode();
           
        }

        Destroy(gameObject);
    }
}

