using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed ;
    public int damage;
    public Transform target;
    public float homingSensitivity = 5f; 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            
            return;
        }

        
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

   

    public void Initialize(Transform target, float projectileSpeed, int projectileDamage)
    {
        this.target = target;
        this.speed = projectileSpeed;
        this.damage = projectileDamage;

        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        
        if (hitInfo.CompareTag("Boss"))
        {
            return;
        }

       
        if (hitInfo.CompareTag("Player"))
        {
          
        }

     
        Destroy(gameObject);
    }
}
