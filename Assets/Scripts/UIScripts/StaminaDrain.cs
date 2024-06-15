using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaDrain : MonoBehaviour
{
    public Image StaminaBarViz;
    public float Stamina;
    void Update()
    {
        OnDrainStamina();
        
    }

    void Start()
    {
        Stamina = GameManager.instance.playerStamina;
        StaminaBarViz.fillAmount = Stamina;
    }

    void OnDrainStamina()
    {
        // if (PlayerAnimatorManager.instance.isDashing = true)
        // {
            Stamina = GameManager.instance.playerStamina;
            StaminaBarViz.fillAmount = 0.01f * Stamina;
       // }
    }
}