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
    [SerializeField]    private bool isDashing = false;
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
        Debug.Log(isDashing);
    }

    private void HandleMovement(){
        
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= GameManager.instance.playerSpeed;
     
        if(isWalking){
            playerRigidbody.velocity = moveDirection;
        }
        

        // if(isWalking){
            
        // }else{
        //     targetVelocity = moveDirection * dashSpeed;

        // }
    }

    private void HandleRotation(){
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection += cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero){
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // transform.rotation = playerRotation;
        transform.LookAt(bossenemy);
    }

    private void HandleDash(){
        if(inputManager.dashInput && !isDashing && GameManager.instance.playerStamina >= GameManager.instance.playerStaminaDashCost ){
            StartCoroutine(dashRoutine()); 
            //playerRigidbody.AddForce(moveDirection * dashSpeed, ForceMode.VelocityChange);
            // playerRigidbody.velocity = moveDirection * dashSpeed;
            //GameManager.instance.playerStamina -= GameManager.instance.playerStaminaDashCost;
            // playerRigidbody.drag = 5;  
        }
    }

    private IEnumerator dashRoutine(){
        inputManager.dashInput = false;

        if(moveDirection == Vector3.zero) yield break;
        isWalking = false;
        playerRigidbody.drag  = 0;
        isDashing = true;
        //to cancel out the move Speed from the direction of our player
        //moveDirection /= 2;   
        // Time.timeScale = 0.5f;
        
        Vector3 targetDashPos = moveDirection + transform.position;
        targetDashPos *= dashSpeed;
    

        Debug.Log(Vector3.Distance(transform.position, targetDashPos));
        while(Vector3.Distance(transform.position, targetDashPos) > 4){
            playerRigidbody.velocity += moveDirection;
            //Debug.Log("pos: " + transform.position +" targetPos" + targetDashPos + " dist:" +Vector3.Distance(transform.position, targetDashPos) + " " +moveDirection);
            Debug.DrawLine(transform.position + new Vector3(0,3,0), targetDashPos, Color.red);

            yield return new WaitForSeconds(0.1f); 
        }

        // Time.timeScale = 1f;
        isWalking = true;
        isDashing = false;
        playerRigidbody.drag  = 5;
        GameManager.instance.playerStamina -= GameManager.instance.playerStaminaDashCost;
    }

}


