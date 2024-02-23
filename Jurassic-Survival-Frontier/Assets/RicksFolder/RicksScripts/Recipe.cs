using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    public Item[] inputItems; // Items required for the recipe
    public Item outputItem; // Item produced by the recipe
    public int outputQuantity = 1; // Quantity of the output item produced
    public bool IsMatch(Item itemA, Item itemB)
    {
        // Example logic to check if itemA and itemB match the inputItems of this recipe
        // This is a simplistic approach; you might need more complex logic depending on your game's requirements
        return (inputItems[0] == itemA && inputItems[1] == itemB) || (inputItems[0] == itemB && inputItems[1] == itemA);
    }

}
