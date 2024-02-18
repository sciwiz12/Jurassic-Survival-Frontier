using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Text quantityText; // Assign in the inspector
    public Image itemImage; // Assign this in the inspector

    // Update to include item name and quantity in the display
    public void UpdateSlot(string itemName, int quantity)
    {
        // Concatenate item name and quantity for display
        quantityText.text = itemName + (quantity > 1 ? " x" + quantity.ToString() : "");
    }
}
