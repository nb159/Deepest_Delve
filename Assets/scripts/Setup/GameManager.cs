using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    MainMenuScene,
    InGameScene,
    PlayerDeathScene,
    WinScene,
    SettingsScene
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CombatManager combatManager;

    [Header("Game Stats")]
    public float bossHealth = 500; // TODO: CHECK THE JSON --> this value will get passed to BossHealthScript

    public float GameSpeedtime;
    [SerializeField] public float bossAttackDelay = 1f;

    [Header("Camera Settings")] [SerializeField]
    public int cameraRotationSpeed = 15;

    [Space(10)]
    [SerializeField] public int playerPotions = 3;
    [SerializeField] public int PotionHpRegenAmount = 30;

    [Space(10)] [Header("Player Stats")] [SerializeField]
    public float playerMaxHealth = 100; //TODO: add this to the JSON

    [SerializeField] public float playerHealth = 100;
    [SerializeField] public float playerStamina = 100;
    [SerializeField] public float playerStaminaRegen = 2f;
    [SerializeField] public float playerStaminaDashCost = 20f;
    [SerializeField] public float playerStaminaLightAttackCost = 8f;
    [SerializeField] public float playerStaminaComboAttackCost = 10f;
    [SerializeField] public float playerSpeed = 5f;
    [SerializeField] public float playerDashMultiplier = 0.5f; //ideal number is 1.3
    [SerializeField] public float dashTime = 0.6f; //TODO: add this to the JSON
    [SerializeField] public float playerRotationSpeed = 15f;

    [Space(10)]
    [SerializeField] public List<PowerUp> playerSelectedBuffs;
    public GameScene currentScene;

    [Header("Level Management")]
    public int currentLevel = 1;
    private int maxLevel = 2;
    public string[] levelFiles = { "Level1Settings", "Level2Settings" };

    private int playCount;
    private const string PlayCountKey = "PlayCount";

    private void Awake()
    {
        if (instance != null )
        {
            refreshGameManagerBossStats();
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadPlayCount();
            LoadGameSettings(levelFiles[currentLevel - 1]);
        }
    }


    private void Start()
    {
        // TODO: figure out what the fuck you want. This will relocate to MainMenuScene from the start
        // ChangeScene(GameScene.MainMenuScene);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void refreshGameManagerBossStats()
    {
        instance.bossHealth = bossHealth;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InGameScene")
        {
            combatManager = FindObjectOfType<CombatManager>();
            if (combatManager == null)
                Debug.LogError("CombatManager not found in the InGameScene!");
            else
            {
                LoadGameSettings(levelFiles[currentLevel - 1]);
            }
        }
        else
        {
            combatManager = null;
        }
    }

    private void LoadPlayCount()
    {
        playCount = PlayerPrefs.GetInt(PlayCountKey, 0);
    }

    private void SavePlayCount()
    {
        PlayerPrefs.SetInt(PlayCountKey, playCount);
    }

    public void LoadGameSettings(string filename)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(filename);
        if (jsonFile != null)
        {
            string json = jsonFile.text;
            GameSettings settings = JsonUtility.FromJson<GameSettings>(json);
            if (settings != null)
            {
                ApplyGameSettings(settings);
            }
            else
            {
                Debug.LogError("Failed to parse game settings from JSON.");
            }
        }
        else
        {
            Debug.LogError("Cannot find JSON file: " + filename);
        }
    }

    private void ApplyGameSettings(GameSettings settings)
    {
        if (combatManager == null)
            // Debug.LogError("CombatManager not initialized.");
            return;

        float difficultyMultiplier = 1 + (playCount * 0.1f);  

        GameSpeedtime = settings.GameSpeedtime;
        bossHealth = settings.bossHealth;
        bossAttackDelay = settings.bossAttackDelay / difficultyMultiplier; 
        PotionHpRegenAmount = settings.PotionHpRegenAmount;
        playerStamina = settings.playerStamina;
        playerStaminaRegen = settings.playerStaminaRegen;
        playerStaminaDashCost = settings.playerStaminaDashCost * difficultyMultiplier;
        playerStaminaLightAttackCost = settings.playerStaminaLightAttackCost * difficultyMultiplier;
        playerSpeed = settings.playerSpeed;
        playerDashMultiplier = settings.playerDashMultiplier  * difficultyMultiplier;

        combatManager.bossHighRangeAttack = settings.bossHighRangeAttack * difficultyMultiplier;
        combatManager.bossLowRangeAttack = settings.bossLowRangeAttack * difficultyMultiplier;
        combatManager.bossArmAttack = settings.bossArmAttack * difficultyMultiplier;
    }



    public void ChangeScene(GameScene newScene)
    {
        currentScene = newScene;
        SceneManager.LoadScene(GameSceneToSceneName(newScene));
        currentLevel = 1;
        LoadGameSettings(levelFiles[currentLevel - 1]);
        // resetStats();
    }

    public void RestartGame(GameScene newScene)
    {
        ChangeScene(GameScene.InGameScene);
        resetStats();
    }

    public void ContinueGame(GameScene newScene)
    {
        ChangeScene(GameScene.InGameScene);
        //TODO: LOAD THE FUCKING ITEMS OR WHATEVER IS GOING ON AFTER THE PLAYER WON FFS
        if (newScene == GameScene.InGameScene)
        {
            IncrementPlayCount(); 
            currentLevel = 1;
            
            LoadGameSettings(levelFiles[currentLevel - 1]);
        }
        else
        {
            currentLevel = 1;
            LoadGameSettings(levelFiles[currentLevel - 1]);
        }

        resetStats();
    }

    private void IncrementPlayCount()
    {
        playCount++;
        SavePlayCount();
    }

    private string GameSceneToSceneName(GameScene gameScene)
    {
        switch (gameScene)
        {
            case GameScene.InGameScene:
                return "InGameScene";
            case GameScene.PlayerDeathScene:
                return "DeathScene";
            case GameScene.WinScene:
                return "WinScene";
            case GameScene.MainMenuScene:
                return "MainMenu";
            case GameScene.SettingsScene:
                return "SettingsScene";
            default:
                return "MainMenu";
        }
    }

    public void resetStats()
    {
        playerHealth = 100;
        playerPotions = 3;
        playerStamina = 100;
    }

    public void BossDefeated()
    {
        if (currentLevel >= maxLevel)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            currentLevel++;
            SceneManager.LoadScene("InGameScene");
            LoadGameSettings(levelFiles[currentLevel - 1]);
        }
    }

    [System.Serializable]
    public class GameSettings
    {
        public float GameSpeedtime;
        public float bossHealth;

        public float bossAttackDelay;

        // public float playerHealth;
        // public int playerPotions;
        public int PotionHpRegenAmount;
        public float playerStamina;
        public float playerStaminaRegen;
        public float playerStaminaDashCost;
        public float playerStaminaLightAttackCost;
        public float playerSpeed;
        public float playerDashMultiplier;
        public float bossHighRangeAttack;
        public float bossLowRangeAttack;
        public float bossArmAttack;
    }
}