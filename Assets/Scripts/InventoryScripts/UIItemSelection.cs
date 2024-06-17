using UnityEngine;

public class UIItemSelection : MonoBehaviour
{
    public InventorySlot inventorySlot; // Assign this in the inspector
    public ItemDatabase itemDatabase; // Assign this in the inspector
    public GameObject itemSelectionUI; // Assign this in the inspector

    public void ShowRandomItems()
    {
        // Get a random set of items
        // List<Item> randomItems = itemDatabase.GetRandomItems(3);

        // Display the random items in the item selection UI
        // This will depend on your game's specific requirements
    }

    public void SelectItem(Item item)
    {
        // Save the selected item into the inventory slot
        inventorySlot.AddItem(item);

        // Hide the item selection UI
        itemSelectionUI.SetActive(false);
    }
}