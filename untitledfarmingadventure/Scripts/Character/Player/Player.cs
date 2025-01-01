using Godot;
using System;

public partial class Player : StateMachine
{
    #region  Input Resources
    [Export] public PlayerInputs PlayerInputs { get; private set; }
    #endregion
    
    #region RequireNodes
    [Export] public AnimationPlayer AnimationPlayer { get; private set; }
    [Export] public AnimationTree AnimationTree { get; private set; }
    [Export] public Area2D HitBox { get; private set; }
    [Export] public RayCast2D DetectionRay { get; private set; }
    #endregion
    
    #region Misc
    public AnimationNodeStateMachinePlayback StateMachineNodePlayback { get; set; }
    public Vector2 InputDir { get; set; }
    public Vector2 LastInputDir { get; set; }
    [Export] public PlayerVariablesRes PlayerVariables { get; set; }
    #endregion

    public override void _Ready()
    {
        AnimationTree.Active = true;
        AnimationTree.Set("parameters/IdleTree/blend_position",Vector2.Down);
        StateMachineNodePlayback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
        
        InitState(new PlayerIdleState(this));

        InventoryManager.Instance.SetPlayerReference(this);
        InventoryUIManager.Instance.SetPlayerReference(this);

        InventoryManager.Instance.InventoryUpdated += OnInventoryUpdate;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        InventoryManager.Instance.InventoryUpdated -= OnInventoryUpdate;
       }

    void OnInventoryUpdate()
    {
        GD.Print("Inventory Updated:");
    }
}
