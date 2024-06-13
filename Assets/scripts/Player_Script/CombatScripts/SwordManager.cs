using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{

    [SerializeField] InputManager inputManager;


    private void OnCollisionEnter(Collision collision)
    {
       //Debug.Log(collision.gameObject.tag +" "+ "Boss" + " " + PlayerAnimatorManager.instance.canAttack);
        if (collision.gameObject.CompareTag("Boss") && !PlayerAnimatorManager.instance.canAttack)
        {
            //Debug.Log(collision.gameObject.tag + " Hit");

            //TODO: play enemy damage sound
            //TODO: play Enemey damage animation 
            
            CombatManager.instance.playerLightAttack(); 
        }
        
    }

}
