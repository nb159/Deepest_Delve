using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    public static PlayerAnimatorManager instance;
    Animator animator;
    PlayerLocomotion playerLocomotion;
    InputManager inputManager;
    [SerializeField] private Collider swordCollider;
    int horzintall;
    int verticle;
    [Header("Can do's")]
    public bool canAttack = true;
    public bool hasHit = false;
    public bool canDrinkPotion = true;
    public bool isDashing = false;
    public bool canMove = true;
    public bool canInitateComboAttack = true;
    

    private void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }

        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        horzintall = Animator.StringToHash("Horizontal");
        verticle = Animator.StringToHash("Verticle");
    }
    
    public void updateAnimatorFreeRoamValues(float horizontalMovement, float verticleMovement){

        //animation Snapping
        float snappedHorizontal;
        float snappedVerticle;

        snappedHorizontal = SnapHorizontalMovement(horizontalMovement);
        snappedVerticle = SnapVerticleMovement(verticleMovement);

        animator.SetFloat(horzintall, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(verticle, snappedVerticle, 0.01f, Time.deltaTime);
    }

    private float SnapHorizontalMovement(float horizontalMovement)
    {
        float snappedHorizontal;
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        return snappedHorizontal;
    }
    private float SnapVerticleMovement(float verticleMovement)
    {
        float snappedVerticle;

        //the passed in parameter is an abs value of both X and Y inputs. for animation purposes we will need to know if the raw VerticleInput was  
        if(verticleMovement >0 && inputManager.verticalInput > 0){
            snappedVerticle = 1;
        }else if(verticleMovement > 0 && inputManager.verticalInput < 0){
            snappedVerticle = -1;
        }else{
            snappedVerticle = 0;
        }
        return snappedVerticle;
    }
    public void LightAttackAnimation(){
        if(canAttack){
            hasHit = false;
            canAttack = false;
            canDrinkPotion = false;
            canMove = false;
            animator.SetTrigger("LightAttack");    
        }        
    }
    public void comboAttackInput(){
        
        animator.SetTrigger("ComboAttack");
    }
    public void DrinkPotionAnimation(){
        GameManager.instance.playerSpeed /=2;
        canDrinkPotion = false;
        canAttack = false;
        animator.SetTrigger("DrinkPotion");
    }
    public void DashAnimation(){
        animator.SetTrigger("DashTrigger");
    }

    public void PlayerDeathAnimation(){
        animator.SetTrigger("DeathTrigger");
        canAttack = false;
        canDrinkPotion = false;
        canMove = false;
        
        StartCoroutine(afterPlayerDeathLogic());
    }

    private IEnumerator afterPlayerDeathLogic(){
        yield return new WaitForSeconds(3f);
        GameManager.instance.ChangeScene(GameScene.PlayerDeathScene);
    }

    private void endComboAttack(){
        animator.CrossFade("Locomotion", 0.1f);
        canInitateComboAttack = true;
        inputManager.comboAttackInput = false;
        inputManager.currentComboState = 1;
        inputManager.comboAttackArr = new bool[3] {false, false, false};
    }

    //-----------FUNCTIONs called through the animator-------------

    public void endLightAttack(){
        canAttack = true;
        canDrinkPotion = true;
        canMove = true;
        Debug.Log("can attack now" +  canAttack);
        return;
    }
    
    public void endPotionDrinking(){
        GameManager.instance.playerSpeed *= 2;
        Debug.Log("can drink now" +  canDrinkPotion);

        canDrinkPotion = true;
        canAttack = true;
    }
    public void comboAttack(int currentAttack){

        Debug.Log("bfr: " + inputManager.comboAttackArr[currentAttack] + " " +currentAttack );
        switch(currentAttack){
            case 0:
                if(inputManager.comboAttackArr[currentAttack]){
                    inputManager.comboAttackArr[currentAttack] = false;
                    hasHit = false;
                }else{
                    endComboAttack();
                    Debug.Log("i tried to end on the 1st");
                    return;
                }
                break;
            case 1:
                if(inputManager.comboAttackArr[currentAttack]){
                    inputManager.comboAttackArr[currentAttack] = false;
                    hasHit = false;
                }else{
                    endComboAttack();
                    Debug.Log("i tried to end on the 2nd");

                    return;
                }
                break;
            case 2:
                if(inputManager.comboAttackArr[currentAttack]){
                    inputManager.comboAttackArr[currentAttack] = false;
                    hasHit = false;
                }else{
                   endComboAttack();
                    Debug.Log("i tried to end on the 3rd");
                    return;
                }
                break;
        }
        Debug.Log("afr: " + inputManager.comboAttackArr[currentAttack] + " " +currentAttack );

        
        
    }
    public void SwordCollider(int ColliderActivation){

        // Debug.Log("ColliderActivation: " + ColliderActivation);
        if(ColliderActivation == 1){
            swordCollider.enabled = true;
            
        }else{
            swordCollider.enabled = false;
        }
    }

    
}
