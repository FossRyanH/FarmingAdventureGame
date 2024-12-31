using Godot;
using System;

public partial class InventorySlot : Control
{
    #region Required Nodes
    [Export] Sprite2D _slotTexture;
    [Export] TextureRect _itemTexture;
    [Export] Label _itemCount;
    #endregion

    #region Slot Textures
    [Export] Texture2D _slotTextureReg;
    [Export] Texture2D _slotTexturePressed;
    #endregion

    [Export] public int SlotIndex { get; set; }

    public override void _Ready()
    {
        _slotTexture.Texture = _slotTextureReg;
        _itemCount.Text = "";
        RegisterItemEvents();
    }

    public override void _ExitTree()
    {
        UnregisterItemEvents();
    }

    void SlotBehaviour()
    {
        // If mouse click on slot change texture, otherwise revert to normal texture.
    }

    void RegisterItemEvents()
    {
        // Add item event Listeners here.
        InventoryManager.Instance.InventoryUpdated += UpdateInventorySlot;
    }

    void UnregisterItemEvents()
    {
        // Remove item event listening here.
    }

    void UpdateInventorySlot()
    {
        var itemData = InventoryManager.Instance.GetItemByPos(SlotIndex);

        if (itemData == null)
        {
            _itemTexture.Texture = null;
            _itemCount.Text = "";
            return;
        }

        var (item, count) = itemData.Value;

        _itemTexture.Texture = item.ItemIcon;
        _itemCount.Text = count > 1 ? count.ToString() : "";
    }
}
