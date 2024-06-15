using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject Player;

    [Header("Player Stats")]
    [SerializeField] public float lightAttackDamage = 10f;
    [SerializeField] public float heavyAttackDamage = 20f;
    [SerializeField] public float playerDefense = 10f;
    [SerializeField] public float playerCritDamge = 1f;

    [Header("Boss Stats")]
    [SerializeField] public float bossHighRangeAttack;
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
    }

    public void playerLightAttack(){
        //Debug.Log(GameManager.instance.bossHealth+" " + lightAttackDamage);
        GameManager.instance.bossHealth -= lightAttackDamage;
    }


    public void bossHighRangeAttackMethode(){
        Debug.Log( bossHighRangeAttack +" " + GameManager.instance.playerHealth);

        //Debug.Log(GameManager.instance.bossHealth+" " + lightAttackDamage);
        GameManager.instance.playerHealth -= bossHighRangeAttack;
              Debug.Log(  GameManager.instance.playerHealth);
  
    }
      public void bossLowRangeAttackMethode(){
        Debug.Log("low: "+ bossLowRangeAttack +" " + GameManager.instance.playerHealth);

        GameManager.instance.playerHealth -= bossLowRangeAttack;
              Debug.Log(  GameManager.instance.playerHealth);
  
    }

    public void tesy1(){
        Debug.Log("test1");
    }


}
