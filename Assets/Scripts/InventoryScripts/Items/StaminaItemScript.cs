using UnityEngine;
using UnityEngine.UI;

public class StaminaItemScript : MonoBehaviour
{
    public float modifier;
    public string imagePath = "Assets/Sprites/Items/empty.png";

    public Image image;

    // Constructor
    public StaminaItemScript()
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
        modifier = 2f;
    }

    private void ApplyStats()
    {
        //TODO:  CombatManager.instance.staminaRecovery *= modifier;
    }
}