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
    [SerializeField] private float lightAttackDamage = 10f;
    [SerializeField] private float heavyAttackDamage = 20f;
    [SerializeField] private float playerDefense = 10f;
    [SerializeField] private float playerCritDamge = 1f;


    [Header("Boss Stats")]
    [SerializeField] private float bossProjectileSprayDamage = 20f;
    [SerializeField] private float bossStreamRayDamage = 10f;
    [SerializeField] private float bossHealing = 10f;
    [SerializeField] private float bossHealingDuration = 10f;

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


}
