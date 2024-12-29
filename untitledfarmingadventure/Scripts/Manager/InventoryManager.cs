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

    private List<(ItemRes, int)> _items = new List<(ItemRes, int)>();

    public void SetPlayerReference(Player player)
    {
        Player = player;
    }

    public void AddItem(ItemRes item, int count)
    {
        if (item == null)
        {
            GD.Print("Cannot add item of null type");
            return;
        }

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Item1.itemID == item.itemID)
            {
                var existingCount = _items[i].Item2;
                if (existingCount + count <= item.MaxStack)
                {
                    _items[i] = (_items[i].Item1, existingCount + count);
                    InventoryUpdated?.Invoke();
                    return;
                }
            }
        }

        if (_items.Count < InventorySize)
        {
            _items.Add((item, count));
            InventoryUpdated?.Invoke();
        }
        else
        {
            GD.Print("Inventory is currently Full");
            return;
        }
    }

    public void RemoveItem(ItemRes item, int count)
    {
        for (int i = 0; i <_items.Count; i++)
        {
            if (_items[i].Item1.itemID == item.itemID)
            {
                var existingCount = _items[i].Item2;

                if (existingCount > count)
                {
                    _items[i] = (_items[i].Item1, existingCount - count);
                }
                else
                {
                    _items.RemoveAt(i);
                }

                InventoryUpdated?.Invoke();
                return;
            }
        }
    }
}
