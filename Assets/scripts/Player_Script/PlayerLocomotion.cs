using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    Vector3 moveDirection;
    [SerializeField] Transform cameraObject;
    Rigidbody playerRigidbody;
    Collider playerCollider;

    Animator animator;
    public bool isAttacking;
    private bool isWalking = true; 

    [SerializeField] Transform bossenemy;

    [SerializeField] public bool isDashing = false;
    private Vector3 dashVelocity = Vector3.zero; 
    private Vector3 targetVelocity;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<Collider>();
    }

    public void HandleAllMovement(){
        HandleMovement();
        HandleDash();
        HandleRotation();
    }

    private void HandleMovement(){
        if(PlayerAnimatorManager.instance.canMove){
            moveDirection = cameraObject.forward * inputManager.verticalInput;
            moveDirection += cameraObject.right * inputManager.horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;
        
            moveDirection *= GameManager.instance.playerSpeed;
            playerRigidbody.velocity = moveDirection;
        }
    }

    private void HandleRotation(){
        Vector3 targetDirection = bossenemy.position - transform.position;

        // Zero out the y-component of the direction to prevent the player from tipping over
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero){
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, GameManager.instance.playerRotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleDash(){
        if(inputManager.dashInput && !PlayerAnimatorManager.instance.isDashing && GameManager.instance.playerStamina >= GameManager.instance.playerStaminaDashCost ){
            StartCoroutine(dashRoutine()); 
        }
    }

    // private IEnumerator dashRoutine(){
    //     inputManager.dashInput = false;

    //     Vector3 dashDirection = moveDirection.normalized;

    //     if(dashDirection == Vector3.zero) yield break;
        
    //     playerCollider.enabled = false;
               
    //     float originalDrag = playerRigidbody.drag;

    //     playerRigidbody.drag = 0;
    //     float time = 0;

    //     PlayerAnimatorManager.instance.canMove = false;
    //     PlayerAnimatorManager.instance.isDashing = true;
    //     PlayerAnimatorManager.instance.canAttack = false;
    //     PlayerAnimatorManager.instance.canDrinkPotion = false;
    //     PlayerAnimatorManager.instance.DashAnimation();
    
    //     while (1f > time)
    //     {
    //         time += Time.deltaTime;

    //         // Dash in the direction of movement, not the direction the character is facing
    //         playerRigidbody.AddForce(dashDirection * GameManager.instance.playerDashMultiplier, ForceMode.VelocityChange);

    //         yield return null;
    //     }
    //     GameManager.instance.playerStamina -= GameManager.instance.playerStaminaDashCost;

    //     playerRigidbody.drag = originalDrag;

    //     yield return new WaitForSeconds(0.3f);

    //     PlayerAnimatorManager.instance.canMove = true;
    //     PlayerAnimatorManager.instance.isDashing = false;
    //     PlayerAnimatorManager.instance.canAttack = true;
    //     PlayerAnimatorManager.instance.canDrinkPotion = true;

    //     playerCollider.enabled = true;
    // }

    IEnumerator dashRoutine(){
        inputManager.dashInput = false;

        float time = 0;
        float originalDrag = playerRigidbody.drag;
        playerRigidbody.drag = 0;
        playerCollider.enabled = false;

        // Use the current movement direction for the dash
        Vector3 dashDirection = moveDirection.normalized;
                 

        PlayerAnimatorManager.instance.canMove = false;
        PlayerAnimatorManager.instance.isDashing = true;
        PlayerAnimatorManager.instance.canAttack = false;
        PlayerAnimatorManager.instance.canDrinkPotion = false;
        PlayerAnimatorManager.instance.DashAnimation();

        while (GameManager.instance.dashTime > time)
        {
            time += Time.deltaTime;

            playerRigidbody.AddForce(dashDirection * GameManager.instance.playerDashMultiplier, ForceMode.VelocityChange);

            yield return null;
        }

        GameManager.instance.playerStamina -= GameManager.instance.playerStaminaDashCost;
        playerRigidbody.drag = originalDrag;


        //yield return new WaitForSeconds(0.5f);
        
        PlayerAnimatorManager.instance.canMove = true;
        PlayerAnimatorManager.instance.isDashing = false;
        PlayerAnimatorManager.instance.canAttack = true;
        PlayerAnimatorManager.instance.canDrinkPotion = true;
        

        playerCollider.enabled = true;
    }

}


