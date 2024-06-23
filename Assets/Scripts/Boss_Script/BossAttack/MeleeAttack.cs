using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, IBossAttack
{
   

 

    void Start()
    {

    }

   
    void Update()
    {

    }


    public void ExecuteAttack(Transform player)
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


        }


    }



}



