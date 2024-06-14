using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    private GameObject Boss;
    private GameObject Player;

    [Header("Player Stats")]
    [SerializeField] private float lightAttackDamage = 10f;
    [SerializeField] private float heavyAttackDamage = 20f;
    [SerializeField] private float playerDefense = 10f;
    [SerializeField] private float playerCritDamge = 1f;

    [Header("Boss Stats")]
    [SerializeField] public float bossHighRangeAttack ;
    [SerializeField] public float bossLowRangeAttack ;
    [SerializeField] public float bossHealing ;
    [SerializeField] public float bossHealingDuration ;

    // Start is called before the first frame update

    private void Start(){
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
        Boss = GameObject.FindWithTag("Boss");
        Player = GameObject.FindWithTag("Player");

    }

    public void playerLightAttack(){
        //Debug.Log(GameManager.instance.bossHealth+" " + lightAttackDamage);
        GameManager.instance.bossHealth -= lightAttackDamage;
    }


    public void bossHighRangeAttackMethode(){
        //Debug.Log(GameManager.instance.bossHealth+" " + lightAttackDamage);
        GameManager.instance.playerHealth -= bossHighRangeAttack;
              Debug.Log(  GameManager.instance.playerHealth);
  
    }
      public void bossLowRangeAttackMethode(){
        //Debug.Log(GameManager.instance.bossHealth+" " + lightAttackDamage);
        GameManager.instance.playerHealth -= bossLowRangeAttack;
              Debug.Log(  GameManager.instance.playerHealth);
  
    }


}
