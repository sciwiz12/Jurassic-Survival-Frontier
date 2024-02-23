using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Dictionary<Item, int> itemQuantities = new Dictionary<Item, int>();
    private Dictionary<Item, InventorySlot> itemSlotMapping = new Dictionary<Item, InventorySlot>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public GameObject inventoryUI;
    public Recipe[] recipes;
    public int money = 0;
    public Text moneyText;

    private bool isInventoryVisible = false;

    void Start()
    {
        moneyText.text = "Money: " + money.ToString();
        inventoryUI.SetActive(isInventoryVisible);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryVisibility();
        }
    }

    public void AddItem(Item itemToAdd)
    {
        if (itemQuantities.ContainsKey(itemToAdd))
        {
            itemQuantities[itemToAdd]++;
        }
        else
        {
            itemQuantities[itemToAdd] = 1;
        }
        UpdateInventoryUI();
    }

    public void RemoveItem(Item itemToRemove, int quantity = 1)
    {
        if (itemQuantities.ContainsKey(itemToRemove))
        {
            itemQuantities[itemToRemove] -= quantity;
            if (itemQuantities[itemToRemove] <= 0)
            {
                itemQuantities.Remove(itemToRemove);
            }
            UpdateInventoryUI();
        }
    }

    public void CombineItems(Item itemA, Item itemB)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.IsMatch(itemA, itemB) && HasSufficientItemsForRecipe(recipe))
            {
                ExecuteCrafting(recipe);
                return;
            }
        }
        Debug.Log("No matching recipe found.");
    }

    private bool HasSufficientItemsForRecipe(Recipe recipe)
    {
        // Check if the inventory has enough of each input item for the recipe
        foreach (var inputItem in recipe.inputItems)
        {
            if (!itemQuantities.ContainsKey(inputItem) || itemQuantities[inputItem] < 1)
            {
                return false;
            }
        }
        return true;
    }

    private void ExecuteCrafting(Recipe recipe)
    {
        // Deduct input items
        foreach (var inputItem in recipe.inputItems)
        {
            RemoveItem(inputItem);
        }

        // Add output item
        AddItem(recipe.outputItem);
        Debug.Log($"Crafted {recipe.outputItem.itemName}");
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = "Money: " + money.ToString();
    }
    public void RemoveMoney(int amount)
    {
        // Ensure the player has enough money to remove
        if (money >= amount)
        {
            money -= amount;
            moneyText.text = "Money: " + money.ToString();
        }
        else
        {
            Debug.Log("Not enough money to remove.");
        }
    }

    private void ToggleInventoryVisibility()
    {
        isInventoryVisible = !isInventoryVisible;
        inventoryUI.SetActive(isInventoryVisible);
    }


    void UpdateInventoryUI()
    {
        // Reset visibility for all slots managed in the dictionary
        foreach (var slot in itemSlotMapping.Values)
        {
            slot.gameObject.SetActive(false);
        }

        // Reassign or create slots based on current inventory
        int index = 0; // Initialize index variable
        int slotsPerRow = 4; // Define the number of slots per row
        float startX = 50f; // Define the starting X position
        float startY = -50f; // Define the starting Y position
        float slotSize = 40f; // Define the size of each slot
        float slotSpacing = 60f; // Define the spacing between slots

        foreach (var entry in itemQuantities)
        {
            InventorySlot slotScript;
            if (itemSlotMapping.TryGetValue(entry.Key, out slotScript))
            {
                // Slot exists, update it
                slotScript.gameObject.SetActive(true);
            }
            else
            {
                // Slot doesn't exist, create and add to dictionary
                GameObject newSlotGO = Instantiate(slotPrefab, slotPanel);
                slotScript = newSlotGO.GetComponent<InventorySlot>();
                itemSlotMapping.Add(entry.Key, slotScript);
            }

            // Update slot properties
            slotScript.item = entry.Key;
            slotScript.UpdateSlot(entry.Key.itemName, entry.Value);
            slotScript.itemImage.sprite = entry.Key.icon;
            slotScript.itemImage.gameObject.SetActive(entry.Key.icon != null);

            // Calculate the position for this slot
            int rowIndex = Mathf.FloorToInt(index / slotsPerRow);
            int columnIndex = index % slotsPerRow;
            float xPosition = startX + (columnIndex * (slotSize + slotSpacing));
            float yPosition = startY - (rowIndex * (slotSize + slotSpacing));
            slotScript.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, yPosition);
            slotScript.GetComponent<RectTransform>().sizeDelta = new Vector2(slotSize, slotSize);

            index++;
        }
    }


}
