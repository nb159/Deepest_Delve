using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;
    PlayerCombat playerCombat;
    public float deltaTime;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerCombat = GetComponent<PlayerCombat>();

        // PlayerCamera.instance.player = this;
        
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        // deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		// float fps = 1.0f / deltaTime;
        // Debug.Log("FPS: " + fps);
        // Debug.Log("playerHP: " + GameManager.instance.playerHealth + " Stamina: " + GameManager.instance.playerStamina 
        // + "Potions: " + GameManager.instance.playerPotions + " BossHp: " + GameManager.instance.bossHealth);

        Time.timeScale = GameManager.instance.GameSpeedtime;
    }

    //Working with rigidbody physics and movements, its preferable to use fixedUpdate since it runs regardless of the frame rate
    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
        playerCombat.HandleAllCombat();

    }

    //calle donce per frame after all the updates are done
    private void LateUpdate()
    {
        PlayerCamera.instance.HandleAllCameraActions();
    }

    
}
