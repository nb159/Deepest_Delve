using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Stats")]
    public float GameSpeedtime ;

    [Header("Boss Stats")]
    public float bossHealth ;
    public float bossAttackDelay;

    [Header("Player Stats")]
    public float playerHealth ;
    public int playerPotions;
    public int PotionHpRegenAmount;
    // Stamina
    public float playerStamina ;
    public float playerStaminaRegen ;
    public float playerStaminaDashCost ;
    public float playerStaminaLightAttackCost ;
    // Movement
    public float playerSpeed ;
    public float playerDashMultiplier ;

    public enum GameScene { MainMenuScene, InGameScene, PlayerDeathScene, WinScene }
    public GameScene currentScene;

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
    }

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

        
    }

    void Start()
    {
        ChangeScene(GameScene.MainMenuScene);
    }

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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
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
}
