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
        GD.Print("Idling");
    }

    public override void Update(double delta)
    {
        if (Player.InputDir != Vector2.Zero)
        {
            Player.ChangeState(Player.Move);
        }
    }
}
