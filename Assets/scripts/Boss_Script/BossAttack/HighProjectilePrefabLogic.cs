using UnityEngine;

public class HighProjectilePrefabLogic : MonoBehaviour
{
    public float lifetime = 10f; 

    void Start()
    {
       
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        
        if (hitInfo.CompareTag("Boss"))
        {
            return;
        }

       
        if (hitInfo.CompareTag("Player"))
        {
            CombatManager.instance.bossHighRangeAttackMethode();
            Debug.Log("Player hit by highrange attack");
        }

     
        Destroy(gameObject);
    }
}
