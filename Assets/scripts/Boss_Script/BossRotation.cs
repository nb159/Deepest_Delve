using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossRotation : MonoBehaviour
{
    public Transform player;

    void FixedUpdate()
    {
        FacePlayer();
    }

    void FacePlayer()
    {
        if (player != null)
        {
          
            Vector3 direction = (player.position - transform.position).normalized;
           
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}






