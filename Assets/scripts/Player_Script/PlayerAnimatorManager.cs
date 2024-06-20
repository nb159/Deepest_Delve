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
    public int attackType;
    [Header("Can do's")]
    public bool canAttack = true;
    public bool hasHit = false;
    public bool canDrinkPotion = true;
    public bool isDashing = false;
    public bool canDash = true;
    public bool canMove = true;
    public bool canInitateComboAttack = true;

    private float lastFootstepTime;
    public float footstepCooldown = 0.25f; // ideal to avoid double footstep sound due to blendtree with animation keyframes
    public bool deathSoundPlayed = false;


    [Header("Wise Sound Defs")]
    //wwise defs
    public AK.Wwise.Event swordSwing_Sound;
    public AK.Wwise.Event singleFootstep_Sound;
    public AK.Wwise.Event playerDeath_Sound;
        

    [Header("VFX vals")]
    public ParticleSystem healingVFX;
    

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

        healingVFX.Stop();
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
            canDash = false;
            attackType = 0;
            animator.SetTrigger("LightAttack");
            swordSwing_Sound.Post(gameObject);

        }        
    }
    public void comboAttackInput(){
        animator.SetTrigger("ComboAttack");
    }
    public void DrinkPotionAnimation(){
        GameManager.instance.playerSpeed /=2;
        canDrinkPotion = false;
        canAttack = false;
        canDash = false;
        animator.SetTrigger("DrinkPotion");
        healingVFX.Play();
    }
    public void DashAnimation(){
        animator.SetTrigger("DashTrigger");
    }

    public void PlayerDeathAnimation(){
        animator.SetTrigger("DeathTrigger");
        canAttack = false;
        canDrinkPotion = false;
        canMove = false;
        canDash = false;
        
        StartCoroutine(afterPlayerDeathLogic());
    }

    private IEnumerator afterPlayerDeathLogic(){
        if (!deathSoundPlayed)
            {
                Debug.Log("Death sound played");
                //playerDeath_Sound.Post(gameObject);
                deathSoundPlayed = true;
            }        
        yield return new WaitForSeconds(3f);

        GameManager.instance.ChangeScene(GameScene.PlayerDeathScene);
    }

  

    //-----------FUNCTIONs called through the animator-------------

    public void endLightAttack(){
        canAttack = true;
        canDrinkPotion = true;
        canMove = true;
        canDash = true;
        Debug.Log("can attack now" +  canAttack);
        return;
    }
    
    public void endPotionDrinking(){
        GameManager.instance.playerSpeed *= 2;
        canDash = true;
        canDrinkPotion = true;
        canAttack = true;
        healingVFX.Stop();
    }
    
    public void comboAttack(int currentAttack){
        hasHit = false;
        switch(currentAttack){
            case 0:
                if(inputManager.comboAttackArr[currentAttack]){
                    inputManager.comboAttackArr[currentAttack] = false;
                    attackType = 1;
                }else{
                    endComboAttack();
                    return;
                }
                break;
            case 1:
                if(inputManager.comboAttackArr[currentAttack]){
                    inputManager.comboAttackArr[currentAttack] = false;
                    attackType = 1;
                }else{
                    endComboAttack();
                    return;
                }
                break;
            case 2:
                if(inputManager.comboAttackArr[currentAttack]){
                    inputManager.comboAttackArr[currentAttack] = false;
                    attackType = 1;
                }else{
                   endComboAttack();
                    return;
                }
                break;
        }
    }
    public void endComboAttack(){
        SwordCollider(0);


        animator.CrossFade("Locomotion", 0.1f);
        canInitateComboAttack = true;
        inputManager.comboAttackInput = false;
        inputManager.currentComboState = 1;
        inputManager.comboAttackArr = new bool[3] {false, false, false};
        Debug.Log("i ended the combo");
    }

    public void playFootStepSound(){
        if (Time.time - lastFootstepTime >= footstepCooldown)
        {
            singleFootstep_Sound.Post(gameObject);
            lastFootstepTime = Time.time;
        }
    }
    public void SwordCollider(int ColliderActivation){

        if(ColliderActivation == 1){
            swordCollider.enabled = true;
            
        }else{
            swordCollider.enabled = false;
        }
    }

    
}
