using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    Vector3 moveDirection;
    [SerializeField] Transform cameraObject;
    Rigidbody playerRigidbody;

    Animator animator;
    public bool isAttacking;
    private bool isWalking = true;

    [SerializeField] Transform bossenemy;

    [SerializeField] public float movementSpeed = 5;
    [SerializeField] public float rotationSpeed = 15;
    [SerializeField] private float dashSpeed = 10f; // Set this to the desired dash speed
    [SerializeField] public bool isDashing = false;
    [SerializeField]private float dashSmoothTime = 0.2f; // Set this to the desired smoothing time
    private Vector3 dashVelocity = Vector3.zero; // This will store the current velocity of the dash
    private Vector3 targetVelocity;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    public void HandleAllMovement(){
        HandleMovement();
        HandleDash();
        HandleRotation();
    }

    private void HandleMovement()
    {

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= GameManager.instance.playerSpeed;
        if(PlayerAnimatorManager.instance.canMove){
            //ebug.Log(moveDirection);
            playerRigidbody.velocity = moveDirection;
        }
    }

    // private void HandleRotation(){
    //     Vector3 targetDirection = Vector3.zero;

    //     targetDirection = cameraObject.forward * inputManager.verticalInput;
    //     targetDirection += cameraObject.right * inputManager.horizontalInput;
    //     targetDirection.Normalize();
    //     targetDirection.y = 0;

    //     if(targetDirection == Vector3.zero){
    //         targetDirection = transform.forward;
    //     }

    //     Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
    //     Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    //     // transform.rotation = playerRotation;
    //     transform.LookAt(bossenemy);
        
    // }

    private void HandleRotation(){
        Vector3 targetDirection = bossenemy.position - transform.position;

        // Zero out the y-component of the direction to prevent the player from tipping over
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
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

    private IEnumerator dashRoutine()
    {
        inputManager.dashInput = false;

        if(moveDirection == Vector3.zero) yield break;
        PlayerAnimatorManager.instance.canMove = false;
        playerRigidbody.drag  = 0;
        PlayerAnimatorManager.instance.isDashing = true;
        PlayerAnimatorManager.instance.DashAnimation();
        
        Vector3 targetDashPos = moveDirection + transform.position;
        targetDashPos *= dashSpeed;

        //Debug.Log(Vector3.Distance(transform.position, targetDashPos));
        while(Vector3.Distance(transform.position, targetDashPos) > 3){
            playerRigidbody.velocity += moveDirection;
            Debug.DrawLine(transform.position + new Vector3(0,3,0), targetDashPos, Color.red);
            yield return new WaitForSeconds(0.1f); 
        }

        PlayerAnimatorManager.instance.canMove = true;
        PlayerAnimatorManager.instance.isDashing = false;
        playerRigidbody.drag  = 5;
        GameManager.instance.playerStamina -= GameManager.instance.playerStaminaDashCost;
    }

}


