using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerAnimatorManager playerAnimatorManager;
    InputManager inputManager;


    private void Awake(){
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        inputManager = GetComponent<InputManager>();
    }
    private void Update(){
        //Debug.Log(GameManager.instance.playerHealth);
    }

    private void FixedUpdate(){

    }

    public void HandleAllCombat(){
        HandleLightAttack();
        HandleStaminaRegen();
        HandlePotionDrink();
    }
    
    private void HandleLightAttack(){
        if(inputManager.lightAttackInput && GameManager.instance.playerStamina >= GameManager.instance.playerStaminaLightAttackCost
        && playerAnimatorManager.canAttack){
            playerAnimatorManager.LightAttackAnimation();
            GameManager.instance.playerStamina -= GameManager.instance.playerStaminaLightAttackCost;
            inputManager.lightAttackInput = false;
        }
    }

    private void HandleStaminaRegen(){
        if(GameManager.instance.playerStamina < 100){
            GameManager.instance.playerStamina += GameManager.instance.playerStaminaRegen * Time.deltaTime;
        }
        if(GameManager.instance.playerStamina >=  100) GameManager.instance.playerStamina = 100;
        // Debug.Log(GameManager.instance.playerStamina);
    }

    private void HandlePotionDrink(){
        Debug.Log("input" + inputManager.drinkPotionInput + "amount: " + GameManager.instance.playerPotions+ " canDrink: " + playerAnimatorManager.canDrinkPotion);
        if(inputManager.drinkPotionInput && GameManager.instance.playerPotions > 0 && playerAnimatorManager.canDrinkPotion){
            inputManager.drinkPotionInput = false;

            playerAnimatorManager.DrinkPotionAnimation();
            GameManager.instance.playerHealth += GameManager.instance.PotionHpRegenAmount;
            GameManager.instance.playerPotions -= 1;
        }
    }
}
