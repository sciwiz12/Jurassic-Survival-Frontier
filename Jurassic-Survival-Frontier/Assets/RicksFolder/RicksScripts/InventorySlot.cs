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
    public Canvas parentCanvas;

    void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas == null)
        {
            // Optionally, use FindObjectOfType if GetComponentInParent doesn't suit your setup
            parentCanvas = FindObjectOfType<Canvas>();
        }
    }



    // Update to include item name and quantity in the display
    public void UpdateSlot(string itemName, int quantity)
    {
        // Concatenate item name and quantity for display
        quantityText.text = itemName + (quantity > 1 ? " x" + quantity.ToString() : "");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            Debug.Log($"Beginning to drag {item.itemName}");
            originalPosition = transform.position; // Remember the original slot position

            if (ghostItemPrefab != null)
            {
                // Instantiate the ghost item at the slot's position
                currentGhostItem = Instantiate(ghostItemPrefab, parentCanvas.transform, false);// Ensure it's parented to the canvas for correct positioning
                currentGhostItem.transform.position = transform.position;

                // Assign the item's sprite to the ghost item's Image component
                Image ghostImage = currentGhostItem.GetComponentInChildren<Image>(); // Use GetComponentInChildren in case the Image component is not directly on the prefab's root
                if (ghostImage != null)
                {
                    ghostImage.sprite = itemImage.sprite;
                    ghostImage.rectTransform.sizeDelta = itemImage.rectTransform.sizeDelta; // Copy size for consistency
                }
                else
                {
                    Debug.LogError("GhostItem prefab is missing an Image component.");
                    // Consider destroying the ghost item if it cannot correctly represent the item
                    Destroy(currentGhostItem);
                }

                // Optionally, pass the InventorySlot reference to the ghost item
                GhostItem ghostScript = currentGhostItem.GetComponent<GhostItem>();
                if (ghostScript != null)
                {
                    ghostScript.originalSlot = this; // Pass this slot's reference to the ghost item
                }
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
            currentGhostItem.transform.position = Input.mousePosition; // Move the ghost item with the mouse
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag ended");
        // Destroy the ghost item and reset any necessary states
        if (currentGhostItem != null)
        {
            Destroy(currentGhostItem);
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // Attempt to get the GhostItem component from the dragged object
            GhostItem ghostItem = eventData.pointerDrag.GetComponent<GhostItem>();

            // Ensure the ghost item and its original slot are valid
            if (ghostItem != null && ghostItem.originalSlot != null)
            {
                InventorySlot originalSlot = ghostItem.originalSlot;

                // Proceed with the logic using the item from the original slot
                if (this.item == null || this.item.itemName == originalSlot.item.itemName)
                {
                    Debug.Log($"Dropped {originalSlot.item.itemName} onto an empty or compatible slot.");
                    this.item = originalSlot.item; // Move or combine logic placeholder
                    UpdateSlot(this.item.itemName, this.item.quantity); // Update this slot with the new item info

                    // Optionally, clear the original slot if the item was moved
                    // originalSlot.ClearSlot(); // Implement this method to reset or update the original slot as necessary
                }
                else
                {
                    Debug.Log($"Swapping or combining {originalSlot.item.itemName} with {this.item.itemName} is not implemented.");
                    // Implement logic for swapping items or other interactions as needed
                }
            }
            else
            {
                Debug.LogError("The dragged object's original slot or item is null.");
            }
        }
        else
        {
            Debug.LogError("PointerDrag is null.");
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

