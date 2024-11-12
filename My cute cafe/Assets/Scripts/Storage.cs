using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


public class Storage : MonoBehaviour
{
    [System.Serializable]
    public class StoredItem
    {
        public string itemName;
        public int amount;
        public int maxAmount;
    }

    [SerializeField]
    private List<StoredItem> startingStoredItems = new List<StoredItem>();

    private Dictionary<string, int> storedItems = new Dictionary<string, int>();

    public Inventory playerInventory;

    private void Start()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Player inventory not assigned in the inspector!");
            return;
        }

        foreach (StoredItem item in startingStoredItems)
        {
            storedItems[item.itemName] = item.amount;
            Debug.Log($"Initialized {item.amount} of {item.itemName} in storage.");
        }


        StartCoroutine(AutoRestock());
    }

    private IEnumerator AutoRestock()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            foreach (StoredItem item in startingStoredItems)
            {
                if (!storedItems.ContainsKey(item.itemName))
                {
                    storedItems[item.itemName] = 0;
                }
              
                if (storedItems[item.itemName] < item.maxAmount)
                {
                    storedItems[item.itemName]++;
                    Debug.Log($"Restocked 1 {item.itemName}. Total: {storedItems[item.itemName]}");
                }
            }
        }
    }

    public void StoreItem(string itemName, int amount)
    {
        if (playerInventory.RemoveItem(itemName, amount))
        {
            if (storedItems.ContainsKey(itemName))
            {
                storedItems[itemName] += amount;
            }
            else
            {
                storedItems[itemName] = amount;
            }
            Debug.Log($"{amount} {itemName}(s) stored. Total in Storage: {storedItems[itemName]}.");
        }
    }

    public void RetrieveItem(string itemName, int amount)
    {
        if (storedItems.ContainsKey(itemName) && storedItems[itemName] >= amount)
        {
            storedItems[itemName] -= amount;

            if (storedItems[itemName] <= 0)
            {
                storedItems.Remove(itemName);
            }

            playerInventory.AddItem(itemName, amount);
            Debug.Log($"{amount} {itemName}(s) retrieved from storage. Remaining in storage: {storedItems[itemName]}.");
        }
        else
        {
            Debug.LogWarning($"Not enough {itemName}(s) in the storage.");
        }
    }

    private void TransferAllItemsToInventory()
    {
        if (storedItems.Count == 0)
        {
            Debug.Log("No items in storage to transfer.");
            return;
        }

        foreach (var item in storedItems)
        {
            string itemName = item.Key;
            int amount = item.Value;
            playerInventory.AddItem(itemName, amount);
            Debug.Log($"Transferred {amount} of {itemName} to inventory.");
        }

        storedItems.Clear();
        Debug.Log("All items have been transferred to the inventory.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered storage trigger.");
            TransferAllItemsToInventory();
        }
    }

    public int GetStoredAmount(string itemName)
    {
        return storedItems.ContainsKey(itemName) ? storedItems[itemName] : 0;
    }
}
