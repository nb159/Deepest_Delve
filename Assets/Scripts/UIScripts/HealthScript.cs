using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Image HealthBarViz;
    public float Health;
    void Update()
    {
        OnHealthChange();
        
    }

    void Start()
    {
        Health = GameManager.instance.playerHealth;
        HealthBarViz.fillAmount = Health;
    }

    void OnHealthChange()
    {
            Health = GameManager.instance.playerHealth;
            HealthBarViz.fillAmount = 0.01f * Health;
    }
}