using Godot;
using System;

public partial class InventoryUIManager : Singleton<InventoryUIManager>, IMenuInputListener
{
    [Export] public CanvasLayer InventoryLayer { get; private set; }

    public override void _Ready()
    {
        MenuInputs.OpenInventory += OpenInventory;
        MenuInputs.CloseInventory += CloseInventory;
    }

    public override void _ExitTree()
    {
        MenuInputs.CloseInventory -= CloseInventory;
        MenuInputs.OpenInventory -= OpenInventory;
    }

    public void OpenInventory()
    {
        GameStateManager.Instance.GameState = GameState.Inventory;
        InventoryLayer.Visible = true;
    }

    public void CloseInventory()
    {
        GameStateManager.Instance.GameState = GameState.GamePlay;
        InventoryLayer.Visible = false;
    }
}
