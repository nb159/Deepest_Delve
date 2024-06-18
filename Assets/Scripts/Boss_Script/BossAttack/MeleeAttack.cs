using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 



    void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.CompareTag("Boss"))
        {
            return;
        }

        if (hitInfo.CompareTag("Player"))
        {
            if (CombatManager.instance != null)
            {
                CombatManager.instance.bossArmAttackMethode();
                
            }
            Debug.Log("player hit arm");
        }


    }


}
