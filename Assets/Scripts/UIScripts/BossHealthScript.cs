using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthScript : MonoBehaviour
{
    public Image BossHealthViz;
    public float BossHealth;
    void Update()
    {
        OnHealthChange();
        
    }

    void Start()
    {
        BossHealth = GameManager.instance.bossHealth;
        BossHealthViz.fillAmount = BossHealth;
    }

    void OnHealthChange()
    {
            BossHealth = GameManager.instance.bossHealth;
            BossHealthViz.fillAmount = 0.01f * BossHealth;
    }
}