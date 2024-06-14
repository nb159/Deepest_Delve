using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerAnimatorManager playerAnimatorManager;

    public PlayerInput playerInput;
    Vector2 movementInput;

    [Header("Movement Variables")]
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public bool lightAttackInput = false;
    public bool dashInput = false;
    public bool drinkPotionInput = false;

    // Event to notify when the player is drinking a potion
    public delegate void DrinkPotionAction();
    public static event DrinkPotionAction OnDrinkPotion;

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
            playerInput.Player.DrinkPotion.performed += i => {
                drinkPotionInput = true;
                OnDrinkPotion?.Invoke(); // Invoking event when drinking potion
            };
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
        playerAnimatorManager.updateAnimatorFreeRoamValues(0, moveAmount);
    }

    public void HandleAllInputs()
    {
        if (playerAnimatorManager.canAttack)
        {
            HandleMovementInput();
        }
        else
        {
            verticalInput = 0;
            horizontalInput = 0;
        }
    }
}
