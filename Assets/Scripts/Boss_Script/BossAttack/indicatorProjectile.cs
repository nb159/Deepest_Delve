using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicatorProjectile : MonoBehaviour
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

        //Debug.Log(" indicator hit");
        if (hitInfo.CompareTag("Boss") || hitInfo.CompareTag("Player") || hitInfo.CompareTag("Indicator"))
        {
            return;
        }
    
        Destroy(gameObject);



    }
}
