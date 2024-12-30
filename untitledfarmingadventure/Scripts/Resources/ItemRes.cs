using Godot;
using System;

[GlobalClass]
public partial class ItemRes : Resource
{
    [Export] public string ItemName { get; set; }
    [Export] public string ItemDescription { get; set; }
    [Export] public int itemID { get; set; }
    [Export] public int MaxStack { get; set; }
    [Export] public ItemType ItemCategory { get; set; }
    [Export] public ItemType SecondCategory { get; set; }
    [Export] public Texture2D ItemIcon { get; set; }
    [Export] public int ItemCount { get; set; }
}

public enum ItemType { Consumable, Tool,  Weapon, Accessory, Key, Food, Seed, None }
