using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    private List<Item> items;

    private void Awake()
    {
        // Load the items from a JSON file
        var json = File.ReadAllText(Application.dataPath + "/Resources/Items.json");
        items = JsonUtility.FromJson<List<Item>>(json);
    }

    public List<Item> GetRandomItems(int count)
    {
        var randomItems = new List<Item>();
        for (var i = 0; i < count; i++)
        {
            var randomIndex = Random.Range(0, items.Count);
            randomItems.Add(items[randomIndex]);
        }

        return randomItems;
    }
}