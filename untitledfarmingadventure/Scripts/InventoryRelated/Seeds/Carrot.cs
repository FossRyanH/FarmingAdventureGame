using Godot;
using System;

public partial class Carrot : ItemNode
{
    public static event Action<ItemRes> OnCarrotPickedUp;

    protected override void PickUp(Player player)
    {
        base.PickUp(player);

        OnCarrotPickedUp?.Invoke(this.ItemData);
    }
}
