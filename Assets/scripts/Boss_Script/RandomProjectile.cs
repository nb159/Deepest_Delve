using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            //Debug.LogError("No Rigidbody attached to the projectile.");
            return;
        }

        if (!rb.isKinematic)
        {
            rb.velocity = transform.forward * speed;
        }
        else
        {
            //Debug.LogWarning("Cannot set velocity on a kinematic Rigidbody.");
        }
    }

    public void Initialize(  int damage)
    {
       
        this.damage = damage;
        rb = GetComponent<Rigidbody>();

       
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        //Debug.Log($"Projectile hit: {hitInfo.name}");

        // Prevent the projectile from damaging the boss
        if (hitInfo.CompareTag("Boss"))
        {
            return;
        }

        // Apply damage to the player if hit
        if (hitInfo.CompareTag("Player"))
        {
           //player health reduction
           //Debug.Log("hit");
        }

        // Destroy the projectile after impact
        Destroy(gameObject);
    }
}

