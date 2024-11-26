using Godot;
using System;

public class PlayerBaseState : IState, IPlayerControlsListener
{
    protected Player Player;

    public PlayerBaseState(Player player)
    {
        Player = player;
    }
    
    public virtual void Enter()
    {
        RegisterListerners();
    }

    public virtual void Update(double delta)
    {
        UpdateLookDir();
    }

    public virtual void PhysicsUpdate(double delta) {  }

    public virtual void Exit()
    {
        DeregisterListeners();
    }

    void RegisterListerners()
    {
        Player.PlayerInputs.Movement += Move;
        Player.PlayerInputs.Interact += Interact;
    }

    void DeregisterListeners()
    {
        Player.PlayerInputs.Movement -= Move;
        Player.PlayerInputs.Interact -= Interact;
    }

    public void Move(Vector2 movement)
    {
        Player.InputDir = movement;
        Player.InputDir.Normalized();

        if (Player.InputDir != Vector2.Zero)
        {
            Player.LastInputDir = Player.InputDir;
        }
    }

    public void Interact() 
    {
        GD.Print("Interacting");
    }

    public void UseTool() 
    {
        GD.Print("Using whatever the hell is equipped");
    }

    void UpdateLookDir()
    {
        if (Player.LastInputDir == Vector2.Zero) { return; }

        if (Player.AnimationTree != null)
        {
            Player.AnimationTree.Set("parameters/IdleTree/blend_position", Player.LastInputDir);
            Player.AnimationTree.Set("parameters/MoveTree/blend_position", Player.LastInputDir);
        }
    }
}
