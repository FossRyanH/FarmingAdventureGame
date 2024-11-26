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

    public virtual void Update(double delta) {  }

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
    }

    public void Interact() 
    {
        GD.Print("Interacting");
    }

    public void UseTool() 
    {
        GD.Print("Using whatever the hell is equipped");
    }
}
