using Godot;
using System;

public partial class InventoryUi : Control
{
    [Export] public GridContainer InventoryContainer { get; set; }
    [Export] public AnimationPlayer AnimPlayer { get; set; }
    [Export] PackedScene _inventorySlot;
}
