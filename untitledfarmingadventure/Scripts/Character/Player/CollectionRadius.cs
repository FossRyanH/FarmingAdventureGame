using Godot;
using System;
using System.Collections.Generic;

public partial class CollectionRadius : Area2D
{
    #region Nodes and related settings
    [Export] Area2D _collectioinRadius;
    [Export] CollisionShape2D _itemDetectionShape;
    CircleShape2D _magnetShape;
    #endregion

    #region Magnet and Item Settings
    [Export] public float MagnetRadius { get; set; } = 10f;
    [Export] float _attractionSpeed = 3f;
    List<ItemNode> _itemsInRange = new List<ItemNode>();
    #endregion

    public override void _Ready()
    {
        _magnetShape = new CircleShape2D();
        _collectioinRadius.AreaEntered += PullToPlayer;
        _itemDetectionShape.Shape = _magnetShape;
        _magnetShape.Radius = MagnetRadius;
    }

    void PullToPlayer(Area2D area)
    {
        if (area is ItemNode item)
        {
            _itemsInRange.Add(item);
        }
    }

    public override void _Process(double delta)
    {
        foreach (var item in _itemsInRange)
        {
            if (item != null && IsInstanceValid(item))
            {
                Vector2 itemPos = item.GlobalPosition;
                Vector2 currentPos = this.GlobalPosition;

                float lerpFactor = (float)delta * _attractionSpeed;

                item.GlobalPosition = itemPos.Lerp(this.GlobalPosition, lerpFactor);
            }
        }
    }
}
