using Godot;
using System;

public class PlayerMoveState : PlayerBaseState
{
    private Vector2 _velocity = Vector2.Zero;
    
    public PlayerMoveState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.StateMachineNodePlayback.Travel("MoveTree");
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        
        if (Player.InputDir == Vector2.Zero)
        {
            Player.ChangeState(new PlayerIdleState(Player));
        }
    }

    public override void PhysicsUpdate(double delta)
    {
        _velocity = Player.InputDir * Player.PlayerVariables.WalkSpeed;
        Player.Velocity = _velocity;

        Player.MoveAndSlide();
    }
}
