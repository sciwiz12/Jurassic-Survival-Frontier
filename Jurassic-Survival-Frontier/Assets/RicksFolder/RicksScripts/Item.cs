using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int quantity = 1; // Default quantity

    // Add other item properties here
}
