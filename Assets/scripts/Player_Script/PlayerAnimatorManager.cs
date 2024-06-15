using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    public static PlayerAnimatorManager instance;
    Animator animator;
    PlayerLocomotion playerLocomotion;
    int horzintall;
    int verticle;
    public bool canAttack = true;
    public bool canDrinkPotion = true;
    public bool isDashing = false;
    public bool canMove = true;
    public bool canInitateComboAttack = true;
    

    private void Awake(){
        if(instance == null){
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        horzintall = Animator.StringToHash("Horizontal");
        verticle = Animator.StringToHash("Verticle");
    }
    public void Update()
    {
        changeParameterOnAnimationEnd();
    }
    public void updateAnimatorFreeRoamValues(float horizontalMovement, float verticleMovement)
    {

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
        if (verticleMovement > 0 && inputManager.verticalInput > 0)
        {
            snappedVerticle = 1;
        }
        else if (verticleMovement > 0 && inputManager.verticalInput < 0)
        {
            snappedVerticle = -1;
        }
        else
        {
            snappedVerticle = 0;
        }
        return snappedVerticle;
    }

    public void LightAttackAnimation(){
        if(canAttack){
            canAttack = false;
            animator.SetTrigger("LightAttack");
          
        }        
    }

    public void comboAttackInput(){
        animator.SetTrigger("ComboAttack");
    }


    public void DrinkPotionAnimation(){
        GameManager.instance.playerSpeed /=2;
        canDrinkPotion = false;
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
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("DrinkPotion"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && !canDrinkPotion)
            {
                animator.ResetTrigger("DrinkPotion");
                Debug.Log("DrinkPotion");
                GameManager.instance.playerSpeed *= 2;
                canDrinkPotion = true;
            }
        }
    }

    //FUNCTION called through the animator
    private void comboAttack(int currentAttack){

        Debug.Log("bfr: " + inputManager.comboAttackArr[currentAttack] + " " +currentAttack );
        switch(currentAttack){
            case 0:
                if(inputManager.comboAttackArr[currentAttack]){
                    //inputManager.comboAttackArr[currentAttack] = false;
                }else{
                    animator.CrossFade("Locomotion", 0.1f);
                    canInitateComboAttack = true;
                    inputManager.comboAttackArr = new bool[3] {false, false, false};
                }
                break;
            case 1:
                if(inputManager.comboAttackArr[currentAttack]){
                    //inputManager.comboAttackArr[currentAttack] = false;
                }else{
                    animator.CrossFade("Locomotion", 0.1f);
                    canInitateComboAttack = true;
                    inputManager.comboAttackArr = new bool[3] {false, false, false};

                }
                break;
            case 2:
                if(inputManager.comboAttackArr[currentAttack]){
                    //inputManager.comboAttackArr[currentAttack] = false;
                }else{
                    animator.CrossFade("Locomotion", 0.1f);
                    canInitateComboAttack = true;
                    inputManager.comboAttackArr = new bool[3] {false, false, false};

                }
                break;

        }
        Debug.Log("afr: " + inputManager.comboAttackArr[currentAttack] + " " +currentAttack );

        
        
    }



}
