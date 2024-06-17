using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    PlayerAnimatorManager playerAnimatorManager;

    public PlayerInput playerInput;
    public Vector2 movementInput;

    [Header("Movement Variables")]
    //TODO: ADD Inventory Button
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public bool lightAttackInput = false;
    public bool dashInput = false;
    public bool drinkPotionInput = false;
    public bool openInventoryInput = false;
    public bool[] comboAttackArr = new bool[3] {false, false, false};
    public bool comboAttackInput = false;
    public int currentComboState = 1;
    // Event to notify when the player is drinking a potion
    public delegate void DrinkPotionAction();
    public static event DrinkPotionAction OnDrinkPotion;

    private void Awake()
    {
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
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
            playerInput.Player.DrinkPotion.performed += i => {
                drinkPotionInput = true;
                OnDrinkPotion?.Invoke(); // Invoking event when drinking potion
            };
            playerInput.Player.Inventory.performed += i => openInventoryInput = !openInventoryInput; // Toggle inventory
            playerInput.Player.DrinkPotion.canceled += i => drinkPotionInput = false;
            playerInput.Player.ComboAttack.performed += context => {
                var tapCount = context.interaction is MultiTapInteraction ? ((MultiTapInteraction)context.interaction).tapCount : 1;
                
                switch (tapCount)
                {
                    case 1:
                        if(currentComboState == 1){
                            currentComboState ++;
                            comboAttackArr[0] = true;
                        }
                        break;
                    
                    case 2:
                        if(currentComboState == 2){
                            currentComboState ++;
                            comboAttackArr[1] = true;
                        }                    
                        break;
                    case 3:
                        if(currentComboState == 3){
                            currentComboState = 1;
                            comboAttackArr[2] = true;
                        }
                        break;
                   
                }


                
            };
            //playerInput.Player.ComboAttack.canceled += i => comboAttackInput = false;
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
        }
        else
        {
            verticalInput = 0;
            horizontalInput = 0;
        }
    }

   
}
