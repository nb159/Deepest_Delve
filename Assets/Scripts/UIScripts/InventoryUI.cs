using UnityEngine;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour {
    private InputManager inputManager;
	public Transform itemsParent;	// The parent object of all the items
	public GameObject inventoryUI;	// The entire UI

    [SerializeField]
    private bool inventoryInput = false;

	Inventory inventory;	// Our current inventory

	InventorySlot[] slots;	// List of all the slots

	void Start () {
        inputManager = GetComponent<InputManager>();
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;	// Subscribe to the onItemChanged callback
		inventoryUI.SetActive(false);
		// Populate our slots array
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	void Update () {
		// TODO: Refactor this to use the new input system
		if (InputManager.instance.openInventoryInput && !inventoryUI.activeSelf)
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
		}
        else if (!InputManager.instance.openInventoryInput && inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);

        }
	}

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	void UpdateUI ()
	{
		// Loop through all the slots
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)	// If there is an item to add
			{
				slots[i].AddItem(inventory.items[i]);	// Add it
			} else
			{
				// Otherwise clear the slot
				slots[i].ClearSlot();
			}
		}
	}
}