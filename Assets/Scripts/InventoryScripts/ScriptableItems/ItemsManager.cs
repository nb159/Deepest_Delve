using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{

    public static ItemsManager instance;
    public PowerUp[] selectableItems;

    public static bool userSelected = false;

    public GameObject[] itemDisplayObject;

    void Awake()
    {
        gameObject.SetActive(false);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void randomizeItems()
    {
                
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

    public void  togglItemsSelector(bool state){
        gameObject.SetActive(state);
        gameObject.GetComponent<Collider>().enabled = state;
    }
}
