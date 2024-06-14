using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    public static PlayerAnimatorManager instance;
    Animator animator;
    PlayerLocomotion playerLocomotion;
    InputManager inputManager;
    int horzintall;
    int verticle;
    [Header("Can do's")]
    public bool canAttack = true;
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
    public void Update(){
        changeParameterOnAnimationEnd();
        
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

    public void changeParameterOnAnimationEnd(){

        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("LightAttack")){
            //animation ends on 0.98f. So for safety the threshhold will be 0.95f
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f ) {
                canAttack = true;
                canDrinkPotion = true;
                canMove = true;
            }
        }

        //TODO: normailized time ends at 0.9 for some reasion..
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("DrinkPotion")){
            // Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.90f && canAttack == false && !canDrinkPotion){
                
                GameManager.instance.playerSpeed *= 2;
                canDrinkPotion = true;
                canAttack = true;
            }
        }
    }

    //FUNCTION called through the animator
    private void comboAttack(int currentAttack){

        //Debug.Log(inputManager.comboAttackInput);
        //if(!inputManager.comboAttackInput) animator.CrossFade("Locomotion", 0.1f);

        if(currentAttack == 3) canInitateComboAttack = true;
        
    }



}
