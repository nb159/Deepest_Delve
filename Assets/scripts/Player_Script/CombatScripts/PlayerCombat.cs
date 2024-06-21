using System.Collections;
using System.Collections.Generic;
using UIScripts;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerAnimatorManager playerAnimatorManager;
    InputManager inputManager;
    [SerializeField] public bool toggledeath = true;
    


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
        HandleComboAttack();
        HandleStaminaRegen();
        HandlePotionDrink();
        HandleDeath();   
    }
    
    private void HandleLightAttack(){
        if(inputManager.lightAttackInput && GameManager.instance.playerStamina >= GameManager.instance.playerStaminaLightAttackCost
        && playerAnimatorManager.canAttack){
            
            inputManager.lightAttackInput = false;
            GameManager.instance.playerStamina -= GameManager.instance.playerStaminaLightAttackCost;

            playerAnimatorManager.LightAttackAnimation();
        }
    }

    private void HandleComboAttack(){
        if(playerAnimatorManager.canInitateComboAttack && inputManager.comboAttackInput && GameManager.instance.playerStamina >= GameManager.instance.playerStaminaComboAttackCost ){
            playerAnimatorManager.canInitateComboAttack = false;
            playerAnimatorManager.comboAttackInput();
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
        //Debug.Log("input" + inputManager.drinkPotionInput +  " canDrink: " + playerAnimatorManager.canDrinkPotion + " has potions: "+  (GameManager.instance.playerPotions > 0) + " low health: " +  (GameManager.instance.playerHealth < GameManager.instance.playerMaxHealth));
        if(inputManager.drinkPotionInput && GameManager.instance.playerPotions > 0 && playerAnimatorManager.canDrinkPotion 
        && GameManager.instance.playerHealth < GameManager.instance.playerMaxHealth){
            inputManager.drinkPotionInput = false;

            playerAnimatorManager.DrinkPotionAnimation();
            GameManager.instance.playerHealth += GameManager.instance.PotionHpRegenAmount;
            GameManager.instance.playerPotions -= 1;
            
            if(GameManager.instance.playerHealth > GameManager.instance.playerMaxHealth){
                GameManager.instance.playerHealth = GameManager.instance.playerMaxHealth;
            }
            PotionsScript.instance.OnPotionDrink();
        }
    }

    private void HandleDeath(){
        if(GameManager.instance.playerHealth <= 0){
            playerAnimatorManager.PlayerDeathAnimation();
            //PlayerAnimatorManager.instance.playerDeath_Sound.Post(gameObject);
            //GameManager.instance.ChangeScene(GameScene.PlayerDeathScene);
        }
    }

}
