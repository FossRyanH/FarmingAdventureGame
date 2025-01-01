using Godot;
using System;

public partial class InventoryUIManager : Singleton<InventoryUIManager>, IMenuInputListener
{
    [Export] public PackedScene InventoryLayer { get; private set; }

    CanvasLayer _canvasLayer;
    Control _inventory;

    public Player Player { get; set; }

    public override void _Ready()
    {
        InputManager.Instance.MenuInputs.OpenInventory += OpenInventory;
        InputManager.Instance.MenuInputs.CloseInventory += CloseInventory;
    }

    public override void _ExitTree()
    {
        InputManager.Instance.MenuInputs.CloseInventory -= CloseInventory;
        InputManager.Instance.MenuInputs.OpenInventory -= OpenInventory;
    }

    public void SetPlayerReference(Player player)
    {
        Player = player;

        _canvasLayer = player.GetNodeOrNull<CanvasLayer>("MenuCanvas");

        if (_canvasLayer == null)
        {
            _canvasLayer = new CanvasLayer();
            player.AddChild(_canvasLayer);
        }
    }

    public void OpenInventory()
    {
        if (_inventory == null)
        {
            _inventory = InventoryLayer.Instantiate<Control>();
            _canvasLayer.AddChild(_inventory);
        }

        GameStateManager.Instance.GameState = GameState.Inventory;
        _inventory.Visible = true;
    }

    public void CloseInventory()
    {
        if (_inventory != null)
        {
            GameStateManager.Instance.GameState = GameState.GamePlay;
            _inventory.Visible = false;
        }

    }
}
