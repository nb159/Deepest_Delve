using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="PowerUps")]
public class PowerUp : ScriptableObject
{
    public PowerUpType powerUpType;

    [Header("Buff Info")]
    public string itemName;
    public string itemDescribtion;
    public Sprite itemImage;


    [Header("buff attributes")]
    public float modifier;

    
    public void ApplyStats(){
        Debug.Log(powerUpType);
        switch (powerUpType)
        {
            case PowerUpType.Speed:
                GameManager.instance.playerSpeed *= modifier;
                break;
            case PowerUpType.Health:
                GameManager.instance.playerMaxHealth *= modifier;
                break;
            case PowerUpType.Damage:
                // Apply damage boost
                break;
            case PowerUpType.Stamina:
                GameManager.instance.playerStaminaRegen *= modifier;
                break;
            case PowerUpType.Defense:
                CombatManager.instance.playerDefense *= modifier;
                break;
        }
    }
}


public enum PowerUpType
{
    Speed,
    Health,
    Damage,
    Stamina,
    Defense
}
