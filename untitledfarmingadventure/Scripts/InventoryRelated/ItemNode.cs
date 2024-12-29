using Godot;
using System;

[GlobalClass]
public partial class ItemNode : Area2D
{
    [Export] public ItemRes ItemData { get; set; }

    [Export] Sprite2D _itemSprite;

    public override void _Ready()
    {
        this.BodyEntered += OnBodyEntered;
        InitializeItem();
    }

    void OnBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            PickUp(player);
        }
    }

    protected void InitializeItem()
    {
        if (ItemData != null)
        {
            _itemSprite.Texture = ItemData.ItemIcon;
        }
    }

    protected void PickUp(Player player)
    {
        if (ItemData == null)
        {
            GD.Print("Cannot pick up item of null data type.");
            return;
        }

        GD.Print($"{player.Name} picked up {ItemData.ItemName}");

        InventoryManager.Instance.AddItem(ItemData, 1);

        QueueFree();
    }
}