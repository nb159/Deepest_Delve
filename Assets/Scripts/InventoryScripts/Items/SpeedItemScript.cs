using UnityEngine;
using UnityEngine.UI;

public class SpeedItemScript : MonoBehaviour
{
    public float modifier;
    public string imagePath = "Assets/Sprites/Items/3.png";
    public string itemName = "Speed Buff";
    public Image image;

    // Constructor
    public SpeedItemScript()
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
        //TODO:  CombatManager.instance.playerSpeed *= modifier;
    }
}