using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public Item item; // The item this slot represents
    public Text quantityText; // Assign in the inspector
    public Image itemImage; // Assign this in the inspector
    private Vector2 originalPosition; // To remember the slot's original position
    public GameObject ghostItemPrefab; // Assign a prefab with an Image component in the inspector
    private GameObject currentGhostItem;
    private Transform inventoryWindowPanel; // Reference to the inventory window panel
    private static InventorySlot currentlyDraggingSlot; // Static reference to track which slot is currently being dragged
    public Inventory inventory; // This is one way to get the inventory instance
    public bool isInventorySlot = true; // Set this to false if this slot is in a trading window


    void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        SetParent(isInventorySlot);
    }
    public void SetParent(bool isInventorySlot)
    {
        if (isInventorySlot)
        {
            inventoryWindowPanel = GameObject.FindGameObjectWithTag("InventoryWindow").transform;
            if (inventoryWindowPanel == null)
            {
                Debug.LogError("Inventory window panel not found. Please ensure it is tagged correctly.");
            }
        }
        else
        {
            inventoryWindowPanel = GameObject.FindGameObjectWithTag("TradingWindow").transform;
        }
    }

    public void UpdateSlot(string itemName, int quantity)
    {
        quantityText.text = itemName + (quantity > 1 ? " x" + quantity.ToString() : "");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            originalPosition = transform.position;
            currentlyDraggingSlot = this; // Set the currently dragging slot

            currentGhostItem = Instantiate(ghostItemPrefab, inventoryWindowPanel, false);
            currentGhostItem.transform.position = transform.position;
            Image ghostImage = currentGhostItem.GetComponentInChildren<Image>(true);
            if (ghostImage != null)
            {
                ghostImage.sprite = itemImage.sprite;
                ghostImage.rectTransform.sizeDelta = itemImage.rectTransform.sizeDelta;
            }
            else
            {
                Debug.LogError("GhostItem prefab is missing an Image component.");
                Destroy(currentGhostItem);
            }
        }
        else
        {
            Debug.LogWarning("Attempted to drag an empty slot.");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentGhostItem != null)
        {
            currentGhostItem.transform.position = Input.mousePosition;
        }
    }

    private void CleanupGhostItem()
    {
        if (currentGhostItem != null)
        {
            Destroy(currentGhostItem);
            currentGhostItem = null; // Ensure the reference is cleared
        }
    }

    public void CleanupAllGhostItems()
    {
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("GhostItem");
        foreach (GameObject ghost in ghosts)
        {
            Destroy(ghost);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CleanupGhostItem();
        CleanupAllGhostItems();
        GameObject TradingUIWindow = GameObject.FindGameObjectWithTag("TradingWindow");
        InventorySlot originalSlot = currentlyDraggingSlot; // Slot being dragged from
        // Check if the trading window is active
        if(TradingUIWindow == null)
        {
            Debug.Log("Trading window is not active");
        }
        else
        {
            if (originalSlot != null && originalSlot.item != null)
            {
                bool isBuying = !this.isInventorySlot; // Buying if dragged to a non-inventory slot (NPC to Player)
                // Execute Buy or Sell Logic
                if (isBuying)
                {
                    Debug.Log($"Buying {originalSlot.item.itemName}");
                    // Example Buy Logic
                    inventory.PurchaseItem(originalSlot.item, originalSlot.item.value);
                }
                else
                {
                    Debug.Log($"Selling {originalSlot.item.itemName}");
                    // Example Sell Logic
                    inventory.AddMoney(originalSlot.item.value); // Deduct money from player's inventory
                    inventory.RemoveItem(originalSlot.item); // Add item to player's inventory
                }
            }
        }   
    }

    
    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot originalSlot = currentlyDraggingSlot; // Use the static reference to find the original slot

        if (originalSlot != null && originalSlot.item != null)
        {
            inventory.CombineItems(originalSlot.item, this.item);
        }
        else
        {
            Debug.LogError("Error during dropping: Original slot is null or doesn't contain an item.");
        }
        CleanupGhostItem();
        CleanupAllGhostItems();
    }
    /*public void OnDrop(PointerEventData eventData)
    {
        GameObject TradingUIWindow = GameObject.FindGameObjectWithTag("TradingWindow");
        InventorySlot originalSlot = currentlyDraggingSlot; // Slot being dragged from
        // Check if the trading window is active

        if(TradingUIWindow == null)
        {
            if (originalSlot != null && originalSlot.item != null)
            {
                inventory.CombineItems(originalSlot.item, this.item);
            }
            else
            {
                Debug.LogError("Error during dropping: Original slot is null or doesn't contain an item.");
            }
        }
        else
        {
            
            if (originalSlot != null && originalSlot.item != null)
            {
                bool isBuying = !this.isInventorySlot; // Buying if dragged to a non-inventory slot (NPC to Player)

                // Execute Buy or Sell Logic
                if (isBuying)
                {
                    Debug.Log($"Buying {originalSlot.item.itemName}");
                    // Example Buy Logic
                    inventory.AddItem(originalSlot.item); // Remove item from player's inventory
                    inventory.RemoveMoney(originalSlot.item.value); // Add money to player's inventory
                }
                else
                {
                    Debug.Log($"Selling {originalSlot.item.itemName}");
                    // Example Sell Logic
                    inventory.RemoveItem(originalSlot.item); // Add item to player's inventory
                    inventory.AddMoney(originalSlot.item.value); // Deduct money from player's inventory
                }
            }
        }
        CleanupGhostItem(); // Cleanup any ghost item
        CleanupAllGhostItems(); // Cleanup any other ghost items
    }*/




    // Update the slot with item information
    public void UpdateSlot(Item newItem, int quantity)
    {
        if (newItem == null)
        {
            Debug.LogError("Attempting to update slot with a null item.");
            // Consider also clearing the slot visually if needed
            itemImage.sprite = null; // Hide the item image
            quantityText.text = ""; // Clear the quantity text
            return;
        }

        item = newItem; // Update the item reference
        Debug.Log(item.itemName);
        itemImage.sprite = newItem.icon; // Update the item image
        quantityText.text = newItem.itemName + (quantity > 1 ? " x" + quantity.ToString() : ""); // Update the quantity display
    }


}

