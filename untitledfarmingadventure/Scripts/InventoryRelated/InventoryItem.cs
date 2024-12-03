using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class InventoryItem : Node2D
{
    #region Item Specifics
    [Export] public ItemType ItemType { get; set; }
    [Export] public string ItemName { get; set; }
    [Export] public Texture2D ItemTexture { get; set; }
    [Export] public string ItemDescription { get; set; }
    #endregion
    
    [Export] private string _itemScenePath = "res://Scenes/InventoryItem.tscn";
    
    #region Inventory Specifics
    private Sprite2D _inventorySprite;
    #endregion

    public override void _Ready()
    {
        _inventorySprite = GetNode<Sprite2D>("InventorySprite");
        
        if (!Engine.IsEditorHint() && ItemTexture != null)
        {
            _inventorySprite.Texture = ItemTexture;
        }
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
        {
            _inventorySprite.Texture = ItemTexture;
        }
    }

    public void PickupItem()
    {
        Dictionary<string, object> item = new Dictionary<string, object>
        {
            { "ItemType", ItemType },
            { "ItemName", ItemName },
            { "ItemDescription", ItemDescription },
            { "itemScenePath", _itemScenePath },
            {"itemQuantity", 1}
        };

        if (InventoryManager.Instance.Player != null)
        {
            InventoryManager.Instance.AddItem(item);
            this.QueueFree();
        }
    }

    void OnBodyEntered(Node body)
    {
        if (body.IsInGroup("Player"))
        {
            PickupItem();
        }
    }
}

public enum ItemType { Consumable, Tool, Food }
