


using System.Collections;
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
    public float bossHealth = 100;
    public float GameSpeedtime;

    [SerializeField] public float bossAttackDelay = 1f;

    [Header("Camera Settings")]
    [SerializeField] public int cameraRotationSpeed = 15;

    [Header("Player Stats")]
    [SerializeField] public float playerMaxHealth = 100; //TODO: add this to the JSON
    [SerializeField] public float playerHealth = 100;
    [SerializeField] public int playerPotions = 4;
    [SerializeField] public int PotionHpRegenAmount = 30;
    [SerializeField] public float playerStamina = 100;
    [SerializeField] public float playerStaminaRegen = 1f;
    [SerializeField] public float playerStaminaDashCost = 20f;
    [SerializeField] public float playerStaminaLightAttackCost = 10f;
    [SerializeField] public float playerStaminaComboAttackCost = 10f;
    [SerializeField] public float playerSpeed = 5f;
    [SerializeField] public float playerDashMultiplier = 1.3f; //ideal number is 1.3
    [SerializeField] public float dashTime = 0.6f; //TODO: add this to the JSON
    [SerializeField] public float playerRotationSpeed = 15f;

    public GameScene currentScene;

 [Header("Level Management")]
    public int currentLevel = 1;
    private int maxLevel = 2;
    public string[] levelFiles = { "Level1Settings", "Level2Settings" };




    private void Awake()
    {
        if (instance != null)
        {
            refreshGameManagerBossStats();
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            // LoadGameSettings();
             LoadGameSettings(levelFiles[currentLevel - 1]);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void refreshGameManagerBossStats()
    {
        GameManager.instance.bossHealth = bossHealth;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InGameScene")
        {
            combatManager = FindObjectOfType<CombatManager>();
            if (combatManager == null)
            {
                Debug.LogError("CombatManager not found in the InGameScene!");
            }
            else
            {
                // Debug.Log(  "this should be a proof that there is cmanager" +combatManager.bossArmAttack);
              LoadGameSettings(levelFiles[currentLevel - 1]);
            }
        }
        else
        {
            combatManager = null;
        }
    }

    public void LoadGameSettings(string filename) {
        TextAsset jsonFile = Resources.Load<TextAsset>(filename);
        if (jsonFile != null) {
            string json = jsonFile.text;
            GameSettings settings = JsonUtility.FromJson<GameSettings>(json);
            if (settings != null) {
                ApplyGameSettings(settings);
            } else {
                Debug.LogError("Failed to parse game settings from JSON.");
            }
        } else {
            Debug.LogError("Cannot find JSON file: " + filename);
        }
    }



    private void ApplyGameSettings(GameSettings settings)
    {
        if (combatManager == null)
        {
            // Debug.LogError("CombatManager not initialized.");
            return;
        }

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
    // Debug.Log(settings.bossHighRangeAttack);

    combatManager.bossHighRangeAttack = settings.bossHighRangeAttack;
    combatManager.bossLowRangeAttack = settings.bossLowRangeAttack;
    combatManager.bossArmAttack = settings.bossArmAttack;

     // combatManager.tesy1();
    // bossHealing = settings.playerSpeed;
    // bossHealingDuration = settings.playerSpeed;
    }



    void Start()
    {
        // TODO: figure out what the fuck you want. This will relocate to MainMenuScene from the start
        // ChangeScene(GameScene.MainMenuScene);
    }


    public void ChangeScene(GameScene newScene)
    {
        currentScene = newScene;
        SceneManager.LoadScene(GameSceneToSceneName(newScene));
    }


    // i return here names of scenes cause i want to check on which scene we are to initialize combatmanager
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

    public void BossDefeated() {
        if (currentLevel >= maxLevel) {
            SceneManager.LoadScene("WinScene");
        } else {
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
        public float bossArmAttack;
    }
}
