using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerAnimatorManager playerAnimatorManager;

    public PlayerInput playerInput;
    public Vector2 movementInput;

    [Header("Movement Variables")]
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public bool lightAttackInput =  false;
    public bool dashInput = false;
    public bool drinkPotionInput = false;

    private void Awake()
    {
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
    }   
    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();

            playerInput.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            
            playerInput.Player.Attacks.performed += i => lightAttackInput = true;
            playerInput.Player.Attacks.canceled += i => lightAttackInput = false;
            playerInput.Player.Dash.performed += i => dashInput = true;
            playerInput.Player.Dash.canceled += i => dashInput = false;
            playerInput.Player.DrinkPotion.performed += i => drinkPotionInput = true;
            playerInput.Player.DrinkPotion.canceled += i => drinkPotionInput = false;
        }

        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void HandleMovementInput()
    {
        
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        playerAnimatorManager.updateAnimatorFreeRoamValues(horizontalInput, moveAmount);
    }

    public void HandleAllInputs()
    {
        if(playerAnimatorManager.canMove){
            HandleMovementInput();
        }else{
            verticalInput = 0;
            horizontalInput = 0;
        }

        //Debug.Log(dashInput+ " " + drinkPotionInput);       
    }
}
