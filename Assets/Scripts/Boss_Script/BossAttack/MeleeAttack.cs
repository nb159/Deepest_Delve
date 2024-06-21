using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, IBossAttack
{
    // Start is called before the first frame update

    public ParticleSystem groundVFX1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ExecuteAttack(Transform player)
    {


    }



    void OnTriggerEnter(Collider hitInfo)
    {

     if (groundVFX1 != null)
{
  
    ParticleSystem vfxInstance = Instantiate(groundVFX1, transform.position, Quaternion.identity);

    Vector3 adjustedPosition = vfxInstance.transform.position;
    adjustedPosition.y -= 1.9f;
    vfxInstance.transform.position = adjustedPosition;

   
    Debug.Log(vfxInstance);
    
   
    vfxInstance.Play();

    Destroy(vfxInstance.gameObject, vfxInstance.main.duration + vfxInstance.main.startLifetime.constantMax);
}
        else
        {
            Debug.LogError("Particle System (groundVFX1) is not assigned.");
        }

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



