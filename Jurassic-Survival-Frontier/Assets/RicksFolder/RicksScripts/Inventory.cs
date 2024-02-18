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
        Debug.Log($"{itemToAdd.itemName} quantity updated to {itemQuantities[itemToAdd]}");
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

    void UpdateInventoryUI()
    {
        // Clear all existing slots
        foreach (Transform child in slotPanel)
        {
            Destroy(child.gameObject);
        }

        int index = 0; // Index to track slot positioning
        int slotsPerRow = 5; // Adjust as needed for your UI layout
        float slotSize = 100f; // Assuming square slots for simplicity
        float slotSpacing = 10f;
        float startX = 50f; // Starting X position
        float startY = -50f; // Starting Y position

        // Iterate through each item in the dictionary to create its slot
        foreach (KeyValuePair<Item, int> entry in itemQuantities)
        {
            GameObject slot = Instantiate(slotPrefab, slotPanel);
            InventorySlot slotScript = slot.GetComponent<InventorySlot>();
            if (slotScript != null)
            {
                // Updated to use the new UpdateSlot method, which now requires item name
                slotScript.UpdateSlot(entry.Key.itemName, entry.Value);
                slotScript.itemImage.sprite = entry.Key.icon; // Set the item icon sprite
                slotScript.itemImage.gameObject.SetActive(entry.Key.icon != null); // Ensure it's visible only if there's an icon
            }

            // Calculate and set position for each slot based on its index
            int row = index / slotsPerRow;
            int column = index % slotsPerRow;
            float xPosition = startX + (column * (slotSize + slotSpacing));
            float yPosition = startY - (row * (slotSize + slotSpacing)); // Negative because UI coordinates go down
            RectTransform slotRectTransform = slot.GetComponent<RectTransform>();
            slotRectTransform.anchoredPosition = new Vector2(xPosition, yPosition);
            slotRectTransform.sizeDelta = new Vector2(slotSize, slotSize); // Assuming square slots

            index++;
        }
    }


}
