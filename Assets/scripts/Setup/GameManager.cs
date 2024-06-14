using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] public float playerStaminaComboAttackCost = 10f;

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

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
