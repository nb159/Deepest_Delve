using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PotionsScript : MonoBehaviour
{
    public Image PotionBarViz;
    public double Potions;
    public float PotionsFloat;
    void Update()
    {
        OnPotionDrink();
        
    }

    void Start()
    {
        Potions = GameManager.instance.playerPotions;
        PotionsFloat = (float)Potions;
       // PotionBarViz.fillAmount = PotionsFloat;
    }

    void OnPotionDrink()
    {
        if (PlayerAnimatorManager.instance.canDrinkPotion = false)
        {
            Potions = GameManager.instance.playerPotions;
           // PotionBarViz.fillAmount = PotionsFloat;
        }
    }
}