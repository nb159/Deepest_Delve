using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;
    public Transform target;
    public float homingSensitivity = 2f; 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            //Debug.LogError("No Rigidbody attached to the projectile.");
            return;
        }

        
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    void FixedUpdate()
    {
        if (target != null && rb != null)
        {
            
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 newVelocity = Vector3.Lerp(rb.velocity.normalized, direction, homingSensitivity * Time.deltaTime);
            rb.velocity = newVelocity * speed;
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

        // Apply damage to the player if hit
        if (hitInfo.CompareTag("Player"))
        {
          
        }

        // Destroy the projectile after impact
        Destroy(gameObject);
    }
}
