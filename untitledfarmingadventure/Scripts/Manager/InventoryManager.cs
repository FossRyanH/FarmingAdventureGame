using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryManager : Singleton<InventoryManager>
{
    [Export] public int InventorySize = 10;
    
    #region Inventory Events
    public event Action InventoryUpdated;
    #endregion

    public Player Player { get; set; }

    private Dictionary<int, Dictionary<string, object>> _items = new Dictionary<int, Dictionary<string, object>>();

    public void AddItem(Dictionary<string, object> item)
    {
        string itemID = item["ItemName"].ToString();

        foreach (var slot in _items)
        {
            if (slot.Value["ItemName"].ToString() == itemID)
            {
                slot.Value["itemQuantity"] = (int)slot.Value["itemQuantity"] + (int)item["itemQuantity"];
                InventoryUpdated?.Invoke();
                
                return;
            }
        }
        
        for (int i = 0; i < InventorySize; i++)
        {
            if (!_items.ContainsKey(i))
            {
                _items[i] = item;
                InventoryUpdated?.Invoke();
                
                return;
            }
        }
    }

    public void RemoveItem()
    {
        InventoryUpdated?.Invoke();
    }

    public void IncreaseInventorySize(int additionalSlots)
    {
        InventoryUpdated?.Invoke();
    }

    public void SetPlayerReference(Player player)
    {
        Player = player;
    }
}
