using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PotionsScript : MonoBehaviour
{
    public Image PotionBarViz;
    public int Potions;
    public float PotionsFloat;
    void Update()
    {
        OnPotionDrink();
        
    }

    void Start()
    {
        Potions = GameManager.instance.playerPotions;
        Debug.Log(Potions);
        PotionsFloat = (float) Potions;
        Debug.Log(PotionsFloat);
        PotionBarViz.fillAmount = PotionsFloat;
    }

    void OnPotionDrink()
    {
        if (!PlayerAnimatorManager.instance.canDrinkPotion)
        { 
            Potions = GameManager.instance.playerPotions;
            PotionBarViz.fillAmount = PotionsFloat;
        }
    }
}