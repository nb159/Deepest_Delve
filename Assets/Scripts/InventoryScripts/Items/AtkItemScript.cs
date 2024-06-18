using UnityEngine;
using UnityEngine.UI;

public class AtkItemScript : MonoBehaviour
{
    public float modifier;
    public string imagePath = "Assets/Sprites/Items/pngwing.com.png";
    public Image image;

    // Constructor
    public AtkItemScript()
    {
        ApplyStats();
    }

    // Start is called before the first frame update
    private void Start()
    {
        ChangeStats();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void ChangeStats()
    {
        modifier = 1.2f;
    }

    private void ApplyStats()
    {
        CombatManager.instance.lightAttackDamage *= modifier;
    }
}