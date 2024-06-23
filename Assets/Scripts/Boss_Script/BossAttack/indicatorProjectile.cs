using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicatorProjectile : MonoBehaviour
{
   
    void Start()
    {

    }

  
    void Update()
    {

    }



    void OnTriggerEnter(Collider hitInfo)
    {

     
        if (hitInfo.CompareTag("Boss") || hitInfo.CompareTag("Player") || hitInfo.CompareTag("Indicator"))
        {
            return;
        }
    
        Destroy(gameObject);



    }
}
