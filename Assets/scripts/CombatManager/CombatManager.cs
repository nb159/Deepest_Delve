using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    [SerializeField] public GameObject Boss;
    [SerializeField] public GameObject Player;

    [Header("Player Stats")]
    [SerializeField] public float lightAttackDamage ;
    [SerializeField] public float heavyAttackDamage;
    [SerializeField] public float playerDefense;
    [SerializeField] public float playerCritDamge ;


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
