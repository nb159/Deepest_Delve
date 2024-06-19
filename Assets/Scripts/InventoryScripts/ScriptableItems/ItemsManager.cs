using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public PowerUp[] selectableItems;

    public static bool userSelected = false;

    public GameObject[] itemDisplayObject;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        List<PowerUp> selectedItems = new List<PowerUp>();
        List<int> selectedIndices = new List<int>();
        System.Random random = new System.Random();

        //TODO: potentailly implement a rarity filter for each level
        while (selectedItems.Count < 3)
        {
            int index = random.Next(selectableItems.Length);
    
            if (!selectedIndices.Contains(index))
            {
                selectedIndices.Add(index);
                selectedItems.Add(selectableItems[index]);
            }
        }

        for (int i = 0; i < selectedItems.Count; i++)
        {
            PowerUp item = selectedItems[i];
            itemDisplayObject[i].GetComponent<ItemDisplaySelector>().AssignedItem = item;
            Debug.Log(item);
        }
    }
}
