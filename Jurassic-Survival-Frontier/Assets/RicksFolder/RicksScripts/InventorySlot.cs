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

    void Awake()
    {
        inventoryWindowPanel = GameObject.FindGameObjectWithTag("InventoryWindow").transform;
        if (inventoryWindowPanel == null)
        {
            Debug.LogError("Inventory window panel not found. Please ensure it is tagged correctly.");
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

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentGhostItem != null)
        {
            Destroy(currentGhostItem);
            if (currentlyDraggingSlot == this)
            {
                currentlyDraggingSlot = null; // Clear the reference if this slot was the one being dragged
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot originalSlot = currentlyDraggingSlot; // Use the static reference to find the original slot

        if (originalSlot != null && originalSlot.item != null)
        {
            // Perform your drop logic here using originalSlot and this slot
            Debug.Log($"Dropped {originalSlot.item.itemName} onto an empty or compatible slot.");
            // Further logic to handle item movement, combination, or trading
        }
        else
        {
            Debug.LogError("Error during dropping: Original slot is null or doesn't contain an item.");
        }
    }





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

