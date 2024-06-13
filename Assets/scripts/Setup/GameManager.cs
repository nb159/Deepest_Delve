using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;

    [Header("Game Stats")]
    [SerializeField] public float  GameSpeedtime = 1;

    [Header("Boss Stats")]
    [SerializeField] public float bossHealth = 100f;
    [SerializeField] public float bossAttackDelay = 1f;


    [Header("Player Stats")]
    
    [SerializeField] public float playerHealth = 100;
    [SerializeField] public int playerPotions = 4;
    [SerializeField] public int PotionHpRegenAmount = 30;
    //Stamina
    [SerializeField] public float playerStamina = 100;
    [SerializeField] public float playerStaminaRegen = 1f;
    [SerializeField] public float playerStaminaDashCost = 20f;
    [SerializeField] public float playerStaminaLightAttackCost = 10f;
    //movement
    [SerializeField] public float playerSpeed = 5f;
    [SerializeField] public float playerDashMultiplier = 10f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        ChangeScene(GameScene.InGameScene);
    }



    public enum GameScene { MainMenuScene, InGameScene, PlayerDeathScene, WinScene }
    public GameScene currentScene;
 

    public void ChangeScene(GameScene newScene)
    {
        currentScene = newScene;
        HandleSceneChange();
    }

    private void HandleSceneChange()
    {
        switch (currentScene)
        {
            case GameScene.MainMenuScene:
                LoadMainMenu();
                ShowCursor(true);
                break;
            case GameScene.InGameScene:
                StartGame();
                ShowCursor(true);
                break;


            case GameScene.PlayerDeathScene:
                PlayerDeath();
                ShowCursor(true);
                break;

            case GameScene.WinScene:
                PlayerWin();
                ShowCursor(true);
                break;
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
 
    private void StartGame()
    {
        SceneManager.LoadScene("InGameScene");
    }
  private void PlayerWin()
    {
        SceneManager.LoadScene("WinScene");
    }



    public void PlayerDeath()
    {
        SceneManager.LoadScene("DeathScene");
    }



    private void ShowCursor(bool isVisible)
    {
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}