using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public Inventory playerInventory; 
    public Text inventoryText; 

    private void Update()
    {
        UpdateInventoryDisplay();
    }

    private void UpdateInventoryDisplay()
    {
        if (playerInventory == null || inventoryText == null) return;

        Dictionary<string, int> items = playerInventory.GetItems();
        inventoryText.text = "Inventory:\n";

        foreach (var item in items)
        {
            inventoryText.text += $"{item.Key} x {item.Value}\n";
        }
    }
}
