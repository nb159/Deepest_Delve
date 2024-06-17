using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    private GameObject Boss;
    private GameObject Player;

    [Header("Player Stats")]
    [SerializeField] public float lightAttackDamage = 10f;
    [SerializeField] public float heavyAttackDamage = 20f;
    [SerializeField] public float playerDefense = 10f;
    [SerializeField] public float playerCritDamge = 1f;

    [Header("Boss Stats")]
    [SerializeField] public float bossHighRangeAttack;
    [SerializeField] public float bossLowRangeAttack;
        [SerializeField] public float bossArmAttack;
    [SerializeField] public float bossHealing;
    [SerializeField] public float bossHealingDuration;

    // Start is called before the first frame update

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Boss = GameObject.FindWithTag("Boss");
        Player = GameObject.FindWithTag("Player");

    }

    public void playerLightAttack()
    {
        // Debug.Log(GameManager.instance.bossHealth+" " + lightAttackDamage);
        GameManager.instance.bossHealth -= lightAttackDamage;
        if(GameManager.instance.bossHealth <= 0){
            GameManager.instance.nextLevel();
        }
    }



    // public void bossHighRangeAttackMethode(){
    //     //Debug.Log(GameManager.instance.bossHealth+" " + lightAttackDamage);
    //     GameManager.instance.playerHealth -= bossHighRangeAttack;
    //           Debug.Log(  GameManager.instance.playerHealth);

    // }
    //   public void bossLowRangeAttackMethode(){
    //     //Debug.Log(GameManager.instance.bossHealth+" " + lightAttackDamage);
    //     GameManager.instance.playerHealth -= bossLowRangeAttack;
    //           Debug.Log(  GameManager.instance.playerHealth);

    // }

    public void bossHighRangeAttackMethode()
    {
        Debug.Log(bossHighRangeAttack + " " + GameManager.instance.playerHealth);

        // Debug.Log(GameManager.instance.bossHealth+" " + lightAttackDamage);
        GameManager.instance.playerHealth -= bossHighRangeAttack;
        Debug.Log(GameManager.instance.playerHealth);
        
        if (GameManager.instance.playerHealth <= 0)
        {
            GameManager.instance.ChangeScene(GameScene.PlayerDeathScene);
        }

    }
    public void bossLowRangeAttackMethode()
    {
        Debug.Log("low: " + bossLowRangeAttack + " " + GameManager.instance.playerHealth);

        GameManager.instance.playerHealth -= bossLowRangeAttack;
        Debug.Log(GameManager.instance.playerHealth);

    
        if (GameManager.instance.playerHealth <= 0)
        {
            GameManager.instance.ChangeScene(GameScene.PlayerDeathScene);
        }


    }


 public void bossArmAttackMethode()
    {
        Debug.Log("low: " + bossArmAttack + " " + GameManager.instance.playerHealth);

        GameManager.instance.playerHealth -= bossArmAttack;
        Debug.Log(GameManager.instance.playerHealth);

     Debug.Log("testing if armattack methode works");
        if (GameManager.instance.playerHealth <= 0)
        {
            GameManager.instance.ChangeScene(GameScene.PlayerDeathScene);
        }


    }
    public void tesy1()
    {
        Debug.Log("test1");
    }


}
