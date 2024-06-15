using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CombatManager combatManager;

    [Header("Game Stats")]
    public float GameSpeedtime;

    [Header("Boss Stats")]
    [SerializeField] public float bossHealth = 100f;
    [SerializeField] public float bossAttackDelay = 1f;

    [Header("Camera Settings")]
    [SerializeField] public int cameraRotationSpeed = 15;


    [Header("Player Stats")]
    
    [SerializeField] public float playerHealth = 100;
    [SerializeField] public int playerPotions = 4;
    [SerializeField] public int PotionHpRegenAmount = 30;
    //Stamina
    [SerializeField] public float playerStamina = 100;
    [SerializeField] public float playerStaminaRegen = 1f;
    [SerializeField] public float playerStaminaDashCost = 20f;
    [SerializeField] public float playerStaminaLightAttackCost = 10f;
    [SerializeField] public float playerSltaminaComboAttackCost = 10f;

    //movement
    [SerializeField] public float playerSpeed = 5f;
    [SerializeField] public float playerDashMultiplier = 10f;
    [SerializeField] public float playerRotationSpeed = 15f;

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
            LoadGameSettings();
        }
    }

    private void LoadGameSettings()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("GameSettings");
        if (jsonFile != null)
        {
            //Debug.Log("GameSettings.json file found in Resources.");
            string json = jsonFile.text;
            // Debug.Log($"GameSettings.json content: {json}");

            GameSettings settings = JsonUtility.FromJson<GameSettings>(json);

            if (settings != null)
            {
                // Debug.Log("Game settings loaded successfully.");
                ApplyGameSettings(settings);
            }
            else
            {
                //Debug.LogError("Failed to parse game settings from JSON.");
            }
        }
        else
        {
            //Debug.LogError("Cannot find GameSettings.json file in Resources.");
        }
    }


    void Start()
    {
        ChangeScene(GameScene.MainMenuScene);
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
                LoadScene("MainMenu", true);
                break;
            case GameScene.InGameScene:
                LoadScene("InGameScene", true);
                break;
            case GameScene.PlayerDeathScene:
                LoadScene("DeathScene", true);
                break;
            case GameScene.WinScene:
                LoadScene("WinScene", true);
                break;
        }
    }

    private void LoadScene(string sceneName, bool showCursor)
    {
        StartCoroutine(LoadSceneAsync(sceneName, showCursor));
    }

    private IEnumerator LoadSceneAsync(string sceneName, bool showCursor)
    {
        AsyncOperation asyncLoad =  SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        ShowCursor(showCursor);
    }

    private void ShowCursor(bool isVisible)
    {
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }


    [System.Serializable]
    public class GameSettings
    {
        public float GameSpeedtime;
        public float bossHealth;
        public float bossAttackDelay;
        public float playerHealth;
        public int playerPotions;
        public int PotionHpRegenAmount;
        public float playerStamina;
        public float playerStaminaRegen;
        public float playerStaminaDashCost;
        public float playerStaminaLightAttackCost;
        public float playerSpeed;
        public float playerDashMultiplier;
        public float bossHighRangeAttack;
        public float bossLowRangeAttack;

    }

    private void ApplyGameSettings(GameSettings settings)
    {
        // Debug.Log("Applying game settings...");
        GameSpeedtime = settings.GameSpeedtime;
        bossHealth = settings.bossHealth;
        bossAttackDelay = settings.bossAttackDelay;
        playerHealth = settings.playerHealth;
        playerPotions = settings.playerPotions;
        PotionHpRegenAmount = settings.PotionHpRegenAmount;
        playerStamina = settings.playerStamina;
        playerStaminaRegen = settings.playerStaminaRegen;
        playerStaminaDashCost = settings.playerStaminaDashCost;
        playerStaminaLightAttackCost = settings.playerStaminaLightAttackCost;
        playerSpeed = settings.playerSpeed;
        playerDashMultiplier = settings.playerDashMultiplier;
        // CombatManager.instance.pl = settings.playerSpeed;
        // heavyAttackDamage = settings.playerSpeed;
        // playerDefense = settings.playerSpeed;
        // playerCritDamage = settings.playerSpeed;
        Debug.Log(settings.bossHighRangeAttack);
        combatManager.bossHighRangeAttack = settings.bossHighRangeAttack;
        combatManager.bossLowRangeAttack = settings.bossLowRangeAttack;
        combatManager.tesy1();
        // bossHealing = settings.playerSpeed;
        // bossHealingDuration = settings.playerSpeed;

    }



}
