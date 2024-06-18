using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemScript : MonoBehaviour
{

    public float speed;
    public int damage;
    public float modifier;
    public string ImagePath = "Assets/Sprites/Items/pngwing.com.png";

    // Constructor
    public TestItem()
    {
        this.modifier = 1.2f;
        CombatManager.instance.lightAttackDamage *= modifier;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}
