using Godot;
using System;
using System.Collections.Generic;
using System.Threading;

public partial class InventoryManager : Singleton<InventoryManager>
{
    [Export] public int InventorySize = 7;
    
    #region Inventory Events
    public event Action InventoryUpdated;
    #endregion

    public Player Player { get; set; }

    private List<(ItemRes, int, int)> _items = new List<(ItemRes, int, int)>();

    public void SetPlayerReference(Player player)
    {
        Player = player;
    }

    public void AddItem(ItemRes item, int count, int position = -1)
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
                    _items[i] = (_items[i].Item1, existingCount + count, _items[i].Item3);
                    InventoryUpdated?.Invoke();
                    return;
                }
            }
        }

        if (_items.Count < InventorySize)
        {
            if (position == -1)
            {
                position = GetNextAvailablePos();
            }

            _items.Add((item, count, position));
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
                    _items[i] = (_items[i].Item1, existingCount - count, _items[i].Item3);
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

    public (ItemRes, int)? GetItemByPos(int position)
    {
        foreach (var (item, count, pos) in _items)
        {
            if (pos == position)
            {
                return (item, count);
            }
        }

        return null;
    }

    int GetNextAvailablePos()
    {
        for (int i = 0; i < InventorySize; i++)
        {
            if (!_items.Exists(item => item.Item3 == i))
            {
                return i;
            }
        }

        return -1;
    }

    // public List<(ItemRes, int)> GetAllItems()
    // {
    //     return new List<(ItemRes, int)>(_items);
    // }
}
