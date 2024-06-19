using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ItemDisplaySelector : MonoBehaviour
{
    public PowerUp AssignedItem;
    
    public Sprite itemImage;
    public UnityEngine.UI.Image itemDisplay; // Add this line to reference an Image component

    void Update(){
        itemDisplay.sprite = itemImage;
    }
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
