using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{

    [SerializeField] InputManager inputManager;
    private Collider SwordCollider;
    private void Awake()
    {
        SwordCollider = GetComponent<Collider>();
    }

    // Call this method when you start a new attack
    

    private void OnCollisionEnter(Collision collision)
    {
        if (!PlayerAnimatorManager.instance.hasHit && collision.gameObject.CompareTag("Boss")) // Only deal damage if the sword hasn't hit the boss in the current attack
        {
            Debug.Log(collision.gameObject.tag + " Hit");

            //TODO: play enemy damage sound
            //TODO: play Enemey damage animation 
            
            CombatManager.instance.playerLightAttack();
            PlayerAnimatorManager.instance.hasHit = true; // Set the flag to true because damage has been dealt
        }
    }
}