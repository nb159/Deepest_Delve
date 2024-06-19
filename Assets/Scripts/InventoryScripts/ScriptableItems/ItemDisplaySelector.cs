using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplaySelector : MonoBehaviour
{
    public PowerUp AssignedItem;

    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (ItemsManager.userSelected) return;
        
        AssignedItem.ApplyStats();
        GameManager.instance.playerSelectedBuffs.Add(AssignedItem);
        ItemsManager.userSelected = true;
        Destroy(gameObject);
    }
}
