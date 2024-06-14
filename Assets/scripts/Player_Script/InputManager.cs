using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
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
    public bool lightAttackInput =  false;
    public bool dashInput = false;
    public bool drinkPotionInput = false;
    [SerializeField] public bool comboAttackInput = false;

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
            playerInput.Player.ComboAttack.performed += i => {
                comboAttackInput = true;
                StartCoroutine(resetcomboAttackInput());
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
        playerAnimatorManager.updateAnimatorFreeRoamValues(0, moveAmount);
    }

    public void HandleAllInputs()
    {
        if(playerAnimatorManager.canAttack){
            HandleMovementInput();
        }else{
            verticalInput = 0;
            horizontalInput = 0;
        }

        //Debug.Log(dashInput+ " " + drinkPotionInput);       
    }

    public IEnumerator resetcomboAttackInput(){
        yield return new WaitForSeconds(0.1f);
        comboAttackInput = false;
    }
}
