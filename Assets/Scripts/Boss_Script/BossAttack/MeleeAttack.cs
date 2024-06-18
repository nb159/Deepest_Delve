using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, IBossAttack
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
public void ExecuteAttack(Transform player)
{
   

//   player.position += new Vector3(12, 0, 0);
    Rigidbody playerRb = player.GetComponent<Rigidbody>();
    if (playerRb != null)
    {
        playerRb.AddForce(new Vector3(10, 0, 0) * 10, ForceMode.VelocityChange); 
    }
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

          ExecuteAttack(hitInfo.transform);
            Debug.Log("player hit arm");
        }


    }


}
