using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public int value;
    public int effectiveness;
    public Sprite icon;
    public int quantity = 1; // Default quantity
    public ItemType type;
    // create a tooltip variable
    [TextArea(15, 20)] public string tooltip;

    // create an enum of item types
    public enum ItemType
    {
        Food,
        Medicine,
        Water,
        Crafting
    }
}
