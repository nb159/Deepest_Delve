using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            //TODO: play enemy damage sound
            //TODO: play Enemey damage animation 
            if(PlayerAnimatorManager.instance.attackType ==0){
                CombatManager.instance.playerLightAttack();
            }else if(PlayerAnimatorManager.instance.attackType ==1){
                CombatManager.instance.playerComboAttack();
            }

            PlayerAnimatorManager.instance.attackType = -1;
            
            PlayerAnimatorManager.instance.hasHit = true;

        }
    }

   
}