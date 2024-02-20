using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Use a dictionary to efficiently manage item quantities
    public Dictionary<Item, int> itemQuantities = new Dictionary<Item, int>();
    public GameObject slotPrefab; // The prefab for inventory slots
    public Transform slotPanel; // The parent object for the slots
    public GameObject inventoryUI; // Reference to the whole inventory UI panel
    public delegate void ItemQuantityChanged(Item item, int newQuantity);
    public event ItemQuantityChanged OnItemQuantityChanged;

    private bool isInventoryVisible = false;

    void Start()
    {
        inventoryUI.SetActive(isInventoryVisible); // Start with the inventory hidden
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryVisible = !isInventoryVisible;
            inventoryUI.SetActive(isInventoryVisible);
        }
    }

    public void AddItem(Item itemToAdd)
    {
        if (itemQuantities.ContainsKey(itemToAdd))
        {
            itemQuantities[itemToAdd] += 1;
        }
        else
        {
            itemQuantities.Add(itemToAdd, 1);
        }
        OnItemQuantityChanged?.Invoke(itemToAdd, itemQuantities[itemToAdd]);
        UpdateInventoryUI();
    }

    public void RemoveItem(Item itemToRemove)
    {
        if (itemQuantities.ContainsKey(itemToRemove) && itemQuantities[itemToRemove] > 1)
        {
            itemQuantities[itemToRemove] -= 1;
            OnItemQuantityChanged?.Invoke(itemToRemove, itemQuantities[itemToRemove]);
        }
        else
        {
            itemQuantities.Remove(itemToRemove);
            OnItemQuantityChanged?.Invoke(itemToRemove, 0);
        }
        UpdateInventoryUI();
    }

    public void CombineItems(Item itemA, Item itemB)
    {
        // Example combination logic
        if (itemA.itemName == "Herb" && itemB.itemName == "Herb")
        {
            // Check if we have a "Medicine" item template or create a new one
            Item medicine = new Item(); // This would be your actual item creation logic
            medicine.itemName = "Medicine";
            medicine.quantity = 1; // Adjust based on your game's logic
            AddItem(medicine);
            Debug.Log("Created Medicine");
        }
    }


    void UpdateInventoryUI()
    {
        // Assuming you have a way to track existing slots, perhaps through a List or Dictionary
        // If not, consider adding a List<InventorySlot> existingSlots = new List<InventorySlot>();

        // First, hide all existing slots to prepare for showing only the ones needed
        foreach (Transform child in slotPanel)
        {
            child.gameObject.SetActive(false);
        }

        int index = 0; // Index to track slot positioning
        int slotsPerRow = 5; // Adjust as needed for your UI layout
        float slotSize = 100f; // Assuming square slots for simplicity
        float slotSpacing = 10f;
        float startX = 50f; // Starting X position
        float startY = -50f; // Starting Y position

        // Iterate through each item in the dictionary to update or create its slot
        foreach (KeyValuePair<Item, int> entry in itemQuantities)
        {
            InventorySlot slotScript;
            if (index < slotPanel.childCount)
            {
                // Reuse an existing slot if available
                slotScript = slotPanel.GetChild(index).GetComponent<InventorySlot>();
                slotScript.gameObject.SetActive(true); // Make sure the slot is visible
            }
            else
            {
                // Create a new slot if there are not enough existing slots
                GameObject slot = Instantiate(slotPrefab, slotPanel);
                slotScript = slot.GetComponent<InventorySlot>();
            }

            // Assign the Item to the slot and update its UI
            if (slotScript != null)
            {
                slotScript.item = entry.Key; // Crucial: Assign the Item to the slot
                slotScript.UpdateSlot(entry.Key.itemName, entry.Value);
                slotScript.itemImage.sprite = entry.Key.icon; // Set the item icon sprite
                slotScript.itemImage.gameObject.SetActive(entry.Key.icon != null); // Ensure it's visible only if there's an icon

                // Calculate and set position for each slot based on its index
                int row = index / slotsPerRow;
                int column = index % slotsPerRow;
                float xPosition = startX + (column * (slotSize + slotSpacing));
                float yPosition = startY - (row * (slotSize + slotSpacing));
                RectTransform slotRectTransform = slotScript.GetComponent<RectTransform>();
                slotRectTransform.anchoredPosition = new Vector2(xPosition, yPosition);
                slotRectTransform.sizeDelta = new Vector2(slotSize, slotSize);
            }

            index++;
        }
        // Ensure that the remaining slots (if any) beyond the current inventory count are made inactive
        for (int i = index; i < slotPanel.childCount; i++)
        {
            slotPanel.GetChild(i).gameObject.SetActive(false);
        }
    }




}
