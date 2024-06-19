using UnityEngine;
using UnityEngine.UI;

public class HealthItemScript : MonoBehaviour
{
    public float modifier;
    public string imagePath = "Assets/Sprites/Items/Item 3-1.png.png";
    public string itemName = "Health Buff";
    public Image image;

    // Constructor
    public HealthItemScript()
    {
        ChangeStats();
    }

    // Start is called before the first frame update
    private void Start()
    {
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
        //TODO:  CombatManager.instance.playerHealth *= modifier;
    }
}