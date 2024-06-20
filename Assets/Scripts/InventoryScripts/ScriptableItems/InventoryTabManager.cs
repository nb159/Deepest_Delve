using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line

public class InventoryTabManager : MonoBehaviour
{
    public List<UnityEngine.UI.Image> itemDisplay;

    void Update()
    {
        for (int i = 0; i < itemDisplay.Count; i++)
        {
            
            if(GameManager.instance.playerSelectedBuffs[i] != null)
            {
                itemDisplay[i].sprite = GameManager.instance.playerSelectedBuffs[i].itemImage;
            }
            else
            {
                itemDisplay[i].sprite = null;
                itemDisplay[i].enabled = false;
            }
        }
    }
}
