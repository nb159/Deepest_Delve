using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public static PlayerCamera instance;
     [SerializeField] private Camera cameraObject;
    //this parameter is fed from the playerManager Awake function

    public PlayerManager player;

    [Header("Camera Target")]
    [SerializeField] Transform bossEnemeyTransform;
    [SerializeField] Transform portalTransform;
    Transform targetToLockOn;
    [SerializeField] Transform cameraPivotTransform;

    [Header("Camera Settings")]
    [SerializeField] private float cameraSmoothSpeed = 0.5f;
    [SerializeField] float cameraCollisionRadius = 0.2f;
    [SerializeField] LayerMask collideWithLayers;


    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition; //used for collision
    private float cameraZPosition;
    private float targetCameraZPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else{
            Destroy(gameObject);
        }

        toggleTargetToLockOn(false);

    }

    private void Start(){
        //DontDestroyOnLoad(gameObject);
        cameraZPosition = cameraObject.transform.localPosition.z;
    }

    public void HandleAllCameraActions(){
        FollowPlayer();
        HandleRotations();
        HandleColisions();
    }
    private void FollowPlayer(){
        //always make the camera go from its position to the player's position
        //ref is used to pass the value by reference and the function activly updates the value of the variable
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, 
            ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }

    // private void HandleRotations(){
    //     //todo: fix rotation
    //     //when close to the to the target, container X rotation changes to be able to look at the camera
    //     transform.LookAt(targetToLookAt);
        
    // }
    private void HandleRotations()
    {
        
        //when close to the to the target, container X rotation changes to be able to look at the camera
        Vector3 targetRotation = targetToLockOn.position - transform.position;
        targetRotation.y = 0f; // Lock rotation on the Y axis
        Quaternion targetQuaternion = Quaternion.LookRotation(targetRotation);
        Quaternion clampedRotation = Quaternion.Slerp(transform.rotation, targetQuaternion, GameManager.instance.cameraRotationSpeed * Time.deltaTime);
        // clampedRotation.eulerAngles = new Vector3(
        //     Mathf.Clamp(clampedRotation.eulerAngles.x, 0    , 25),
        //     clampedRotation.eulerAngles.y,
        //     clampedRotation.eulerAngles.z
        // );
        transform.rotation = clampedRotation;
        
    }
    public void toggleTargetToLockOn(bool isPortalActive = false){
    if(isPortalActive){
        targetToLockOn = portalTransform;
    }else{
        targetToLockOn = bossEnemeyTransform;
    }
    }

    private void HandleColisions(){
        targetCameraZPosition = cameraZPosition;
        RaycastHit hit;
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
        direction.Normalize();  
        // Debug.DrawRay(cameraPivotTransform.position, direction, Color.red);
        
        if(Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers)){
            float distnaceFromtHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetCameraZPosition = -(distnaceFromtHitObject - cameraCollisionRadius);
        }

        if(Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius){
            targetCameraZPosition = -cameraCollisionRadius;
        } 


        //Debug.Log(hit.collider.gameObject.name);
        if(hit.collider !=null){
            if(hit.collider.gameObject.CompareTag("Projectile")) return;
        }

        
        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.5f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }
}
