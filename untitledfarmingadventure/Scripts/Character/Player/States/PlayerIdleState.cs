using Godot;
using System;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.StateMachineNodePlayback.Travel("IdleTree");
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        
        if (Player.InputDir != Vector2.Zero)
        {
            Player.ChangeState(new PlayerMoveState(Player));
        }
    }
}
