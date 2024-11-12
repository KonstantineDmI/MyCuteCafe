using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class ItemEntry
    {
        public string itemName;
        public int amount;
    }

    [SerializeField]
    private List<ItemEntry> startingItems = new List<ItemEntry>();

    private Dictionary<string, int> items = new Dictionary<string, int>();

    private void Start()
    {
        foreach (ItemEntry entry in startingItems)
        {
            AddItem(entry.itemName, entry.amount);
        }
    }

    public void AddItem(string itemName, int amount)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] += amount;
        }
        else
        {
            items[itemName] = amount;
        }
        Debug.Log($"{amount} {itemName}(s) added. Total: {items[itemName]}.");
    }

    public bool RemoveItem(string itemName, int amount)
    {
        if (items.ContainsKey(itemName) && items[itemName] >= amount)
        {
            items[itemName] -= amount;

            if (items[itemName] <= 0)
            {
                items.Remove(itemName);
            }

            Debug.Log($"{amount} {itemName}(s) removed. Remaining: {items[itemName]}.");
            return true;
        }
        else
        {
            Debug.LogWarning($"Not enough {itemName}(s) to remove.");
            return false;
        }
    }
    public Dictionary<string, int> GetItems()
    {
        return items; 
    }

    public int GetItemAmount(string itemName)
    {
        return items.ContainsKey(itemName) ? items[itemName] : 0;
    }

}   

